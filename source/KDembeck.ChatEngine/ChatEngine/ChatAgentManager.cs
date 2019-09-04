using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using KDembeck.ChatEngine.Data;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace KDembeck.ChatEngine
{
    public class ChatAgentManager : IChatAgentManager
    {
        private const int MIN_SECONDS_BETWEEN_SESSION_INVITATIONS = 10;
        private const int MIN_SECONDS_UNAVAILABLE_AFTER_INVITATION_DECLINED = 30;
        private const int MIN_SECONDS_AFTER_SESSION_ENDED = 30;
        private const int PRESENCE_SUBSCRIPTION_DURATION = 10;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Timer agentStateCheckTimer;
        private IUcwaClient ucwaClient;
        private IDataUtil dataUtil;
        private string tenantId;
        private List<IChatAgent> chatAgents;

        public event EventHandler<ChatAgentManangerInitializedEventArgs> InitializedEvent;
        
        public ChatAgentManager(string tenantId, IDataUtil dataUtil, IUcwaClient ucwaClient)
        {
            this.tenantId = tenantId;
            this.dataUtil = dataUtil;
            this.ucwaClient = ucwaClient;
            chatAgents = new List<IChatAgent>();
            agentStateCheckTimer = new Timer();
            agentStateCheckTimer.Interval = 1000;
            agentStateCheckTimer.Elapsed += Handle_OnAgentStateCheckTimerElapsed;
        }
        
        public async Task initialize()
        {
            ucwaClient.events.OnContactPresenceUpdated += Handle_OnContactPresenceUpdated;
            ucwaClient.events.OnPresenceSubscriptionUpdated += Handle_OnPesenceSubscriptionUpdated;

            log.Debug("ChatAgentManager. Getting agent data for tenant with id: " + tenantId);

            List<TenantAgentInfo> tenantAgents = dataUtil.getAllAgentsForTenant(tenantId);

            List<string> contactsUriList = new List<string>();
            chatAgents = new List<IChatAgent>();
            if (tenantAgents.Count > 0)
            {
                foreach (TenantAgentInfo tenantAgent in tenantAgents)
                {
                    log.Debug("Agent found: " + tenantAgent.displayName);
                    IChatAgent chatAgent = new ChatAgent();
                    chatAgent.displayName = tenantAgent.displayName;
                    chatAgent.lastSessionEnded = tenantAgent.lastSessionEnded;
                    chatAgent.lastSessionOffered = tenantAgent.lastSessionOffered;
                    chatAgent.sipUri = tenantAgent.sipUri;

                    if (tenantAgent.queueLevels.Count > 0)
                    {
                        log.Debug("Agent: " + tenantAgent.displayName + ". " + tenantAgent.queueLevels.Count + " total queues found for agent.");
                        foreach (QueueLevel queueLevel in tenantAgent.queueLevels)
                        {
                            chatAgent.queueLevels.Add(queueLevel.queueId, queueLevel.level);
                        }
                    }
                    else
                    {
                        log.Warn("No queues found for agent: " + tenantAgent.displayName);
                    }

                    ISearchResource searchResults = await ucwaClient.application.people.search(chatAgent.sipUri);
                    List<ContactResource> searchContactResources = searchResults.contacts;

                    if (searchContactResources.Count > 0)
                    {
                        log.Debug("Agent: " + chatAgent.sipUri + ". Skype contact found for agent. Getting availability.");
                        contactsUriList.Add(tenantAgent.sipUri);
                        chatAgent.contact = searchContactResources[0];
                        chatAgent.contactPresence = await searchContactResources[0].getContactPresence();
                        if (chatAgent.contactPresence.availability == Availability.Online)
                        {
                            log.Debug("Agent: " + chatAgent.sipUri + ". Skype availability: " + chatAgent.contactPresence.availability + ". Setting agent state to Available.");
                            chatAgent.agentState = AgentState.Available;
                        }
                        else if (chatAgent.contactPresence.availability == Availability.None)
                        {
                            log.Debug("Agent: " + chatAgent.sipUri + ". Skype availability: " + chatAgent.contactPresence.availability + ". Setting agent state to Offline.");
                            chatAgent.agentState = AgentState.Offline;
                        }
                        else
                        {
                            log.Debug("Agent: " + chatAgent.sipUri + ". Skype availability: " + chatAgent.contactPresence.availability + ". Setting agent state to Unavailable.");
                            chatAgent.agentState = AgentState.Unavailable;
                        }

                        log.Debug("Agent: " + chatAgent.sipUri + ". Subscribing to Skype contact presence.");
                        IPresenceSubscriptionsResource presenceSubscriptions = await ucwaClient.application.people.getPresenceSubscriptions();
                        await presenceSubscriptions.newPresenceSubscription(PRESENCE_SUBSCRIPTION_DURATION, contactsUriList);

                        chatAgents.Add(chatAgent);
                        log.Debug("Agent: " + chatAgent.sipUri + ". Agent successfully added to agents list.");

                    }
                    else
                    {
                        log.Warn("Agent: " + chatAgent.sipUri + ". Skype contact not found for agent.");
                        chatAgent.agentState = AgentState.Unavailable;
                    }
                }
                agentStateCheckTimer.Start();
                log.Debug("Agent manager initialized for tenant with id " + tenantId);
                InitializedEvent?.Invoke(this, new ChatAgentManangerInitializedEventArgs());
            }
            else
            {
                log.Warn("No agents were found for tenant with id: " + tenantId);
            }
        }

        public int getAgentCountForQueueWithState(string queueId, AgentState state)
        {
            return chatAgents.Where(x => x.queueLevels.ContainsKey(queueId) && x.agentState == state).ToList().Count();
        }

        public List<IChatAgent> getAgentsForQueueWithAgentState(string queueId, AgentState state)
        {
            return chatAgents.Where(x => x.queueLevels.ContainsKey(queueId) && x.agentState == state).ToList();
        }

        public List<IChatAgent> getAllAgentsForQueue(string queueId)
        {
            return chatAgents.Where(x => x.queueLevels.ContainsKey(queueId)).ToList();
        }

        public List<IChatAgent> getAllAgentsForTenant()
        {
            return chatAgents;
        }

        public async Task<IChatAgent> getNextAvaiableChatAgentForChatQueue(string queueId)
        {
            log.Debug("Searching for available agent for queue with id: " + queueId);

            var availableAgentGroups = chatAgents.Where(x =>
                x.agentState == AgentState.Available
                && x.queueLevels.ContainsKey(queueId)
                //&& (DateTime.Now - x.lastSessionEnded).TotalSeconds > MIN_SECONDS_AFTER_SESSION_ENDED
                /*&& (DateTime.Now - x.lastSessionOffered).TotalSeconds > MIN_SECONDS_BETWEEN_SESSION_INVITATIONS*/)
                .OrderBy(x => x.lastSessionEnded)
                .GroupBy(x => x.queueLevels[queueId]);

            if (availableAgentGroups.Count() > 0)
            {
                foreach (IGrouping<int, IChatAgent> agentGroup in availableAgentGroups)
                {
                    foreach (IChatAgent chatAgent in agentGroup)
                    {
                        await chatAgent.contactPresence.Get();
                        if (chatAgent.contactPresence.availability == Availability.Online && chatAgent.agentState == AgentState.Available)
                        {
                            chatAgent.agentState = AgentState.InvitationPending;
                            log.Debug("Agent found for queue with id: " + queueId + ". Agent: " + chatAgent.sipUri);
                            return chatAgent;
                        }
                        else
                        {
                            if (chatAgent.agentState != AgentState.Unavailable)
                            {
                                chatAgent.agentState = AgentState.Unavailable;                                
                            }
                        }
                    }
                }
            }
            else
            {
                log.Debug("No available agents were found for queue with id: " + queueId);
            }
            return null;
        }

        public void drain()
        {
            if (ucwaClient != null)
            {
                ucwaClient.events.OnContactPresenceUpdated -= Handle_OnContactPresenceUpdated;
                ucwaClient.events.OnPresenceSubscriptionUpdated -= Handle_OnPesenceSubscriptionUpdated;
            }
        }

        private void Handle_OnContactPresenceUpdated(object sender, UcwaContactPresenceEventArgs e)
        {   
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == e.contact.uri).FirstOrDefault();
            if (chatAgent != null)
            {
                chatAgent.contact = e.contact;
                chatAgent.contactPresence = e.contactPresence;

                if (chatAgent.contactPresence.availability != Availability.Online)
                {
                    if (chatAgent.contactPresence.availability == Availability.None)
                    {
                        chatAgent.agentState = AgentState.Offline;                            
                    }
                    else if (chatAgent.agentState != AgentState.InSession)
                    {
                        chatAgent.agentState = AgentState.Unavailable;                        
                    }
                }
                else
                {
                    if (chatAgent.agentState != AgentState.InSession && chatAgent.agentState != AgentState.InvitationPending && chatAgent.agentState != AgentState.PostSession && chatAgent.agentState != AgentState.UnavailableInvitationDeclined)
                    {
                        if (chatAgent.agentState != AgentState.Available)
                        {
                            chatAgent.agentState = AgentState.Available;                            
                        }
                    }
                }
            }
        }

        private async void Handle_OnPesenceSubscriptionUpdated(object sender, UcwaPresenceSubscriptionEventArgs e)
        {
            await e.presenceSubscription.extendPresenceSubscription(PRESENCE_SUBSCRIPTION_DURATION);
        }

        private void Handle_OnAgentStateCheckTimerElapsed(object sender, ElapsedEventArgs e)
        {
            agentStateCheckTimer.Stop();
            List<IChatAgent> chatAgentsForCheck = chatAgents.Where(x => x.agentState == AgentState.UnavailableInvitationDeclined).ToList();
            if (chatAgentsForCheck.Count() > 0)
            {
                foreach (IChatAgent chatAgent in chatAgentsForCheck)
                {
                    TimeSpan timeUnavailable = DateTime.Now - chatAgent.lastSessionOffered;
                    if (timeUnavailable.Seconds > MIN_SECONDS_UNAVAILABLE_AFTER_INVITATION_DECLINED)
                    {
                        if (chatAgent.contactPresence.availability == Availability.Online)
                        {
                            //should we query presence manually here just to make sure? 
                            chatAgent.agentState = AgentState.Available;
                        }
                        else if (chatAgent.contactPresence.availability == Availability.None)
                        {
                            chatAgent.agentState = AgentState.Offline;
                        }
                        else
                        {
                            chatAgent.agentState = AgentState.Unavailable;
                        }
                    }
                }
            }
            agentStateCheckTimer.Start();
        }

        public void OnChatSessionParticipantAdded(string participantSipUri, string conversationId, ChatSessionState chatSessionState)
        {   
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == participantSipUri).FirstOrDefault();
            if (chatAgent != null)
            {
                chatAgent.conversationIds.Add(conversationId);

                if (chatSessionState == ChatSessionState.PostSession)
                    chatAgent.agentState = AgentState.PostSession;
                else
                    chatAgent.agentState = AgentState.InSession;
            }
        }

        public void OnChatSessionParticipantDeleted(string participantSipUri, string originallyInvitedParticipantSipUri, string conversationId)
        {
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == participantSipUri).FirstOrDefault();
            if (chatAgent != null)
            {
                if (originallyInvitedParticipantSipUri == participantSipUri)
                {
                    chatAgent.lastSessionEnded = DateTime.Now;
                }

                chatAgent.conversationIds.RemoveAll(x => x == conversationId);
                if (chatAgent.conversationIds.Count() == 0)
                {
                    if (chatAgent.contactPresence.availability == Availability.Online)
                    {
                        chatAgent.agentState = AgentState.Available;
                    }
                    else if (chatAgent.contactPresence.availability == Availability.None)
                    {
                        chatAgent.agentState = AgentState.Offline;
                    }
                    else
                    {
                        chatAgent.agentState = AgentState.Unavailable;
                    }
                }
            }
        }

        public void OnChatSessionInvitationDeclined(string invitedParticipantUri)
        {
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == invitedParticipantUri).FirstOrDefault();
            if (chatAgent != null)
            {
                chatAgent.agentState = AgentState.UnavailableInvitationDeclined;
            }
        }

        public void OnWebUserLeftConversation(string conversationId)
        {
            //mark any agents that are in session for this conversation id as post session
            List<IChatAgent> chatAgentsPostSession = chatAgents.Where(x => x.conversationIds.Contains(conversationId)).ToList();
            foreach (IChatAgent chatAgentPostSession in chatAgentsPostSession)
            {
                chatAgentPostSession.agentState = AgentState.PostSession;
            }
        }

        public void OnChatSessionInvitationStarted(string invitedParticipantUri)
        {
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == invitedParticipantUri).FirstOrDefault();
            if (chatAgent != null)
            {
                chatAgent.lastSessionOffered = DateTime.Now;
                chatAgent.agentState = AgentState.InvitationPending;
            }
        }
    }
}
