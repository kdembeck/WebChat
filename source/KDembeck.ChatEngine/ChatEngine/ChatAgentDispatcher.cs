using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.EventChannel;
using KDembeck.ChatEngine.Data;

namespace KDembeck.ChatEngine
{
    public class ChatAgentDispatcher : IChatAgentDispatcher
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event EventHandler<TenantAgentStatusUpdatedEventArgs> TenantAgentStatusUpdated;
        public event EventHandler<QueueAgentStatusUpdatedEventArgs> QueueAgentStatusUpdated;

        //these should be put into the tenant config table and we should just keep a list of tenant configs in memory that gets refreshed periodically
        private const int MIN_SECONDS_BETWEEN_SESSION_INVITATIONS = 10;
        private const int MIN_SECONDS_AFTER_SESSION_ENDED = 30;
        private const int PRESENCE_SUBSCRIPTION_DURATION = 10;

        public List<IChatAgent> chatAgents { get; private set; }
        
        private string tenantId;
        private IDataUtil dataUtil;
        private IUcwaClient ucwaClient;

        public ChatAgentDispatcher(string TenantId, IDataUtil DataUtil, IUcwaClient UcwaClient)
        {
            tenantId = TenantId;
            dataUtil = DataUtil;
            ucwaClient = UcwaClient;
        }

        public async Task initialize()
        {   
            ucwaClient.events.OnContactPresenceUpdated += Handle_OnContactPresenceUpdated;
            ucwaClient.events.OnPresenceSubscriptionUpdated += Handle_OnPesenceSubscriptionUpdated;

            log.Debug("Agent dispatcher. Getting agent data for tenant with id: " + tenantId);

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
                        if (chatAgent.contactPresence.availability == Availability.None)
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
            }
            else
            {
                log.Warn("No agents were found for tenant with id: " + tenantId);
            }
        }

        public async Task<IChatAgent> findAgentForSession(string queueId)
        {
            var availableAgentGroups = chatAgents.Where(x =>
                x.agentState == AgentState.Available
                && x.queueLevels.ContainsKey(queueId)
                && (DateTime.Now - x.lastSessionEnded).TotalSeconds > MIN_SECONDS_AFTER_SESSION_ENDED
                && (DateTime.Now - x.lastSessionOffered).TotalSeconds > MIN_SECONDS_BETWEEN_SESSION_INVITATIONS)
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
                            sendAgentStatusUpdatedEvent(chatAgent);
                            return chatAgent;
                        }
                        else
                        {
                            if (chatAgent.agentState != AgentState.Unavailable)
                            {
                                chatAgent.agentState = AgentState.Unavailable;
                                sendAgentStatusUpdatedEvent(chatAgent);
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

        public List<IChatAgent> getChatAgentsForQueue(string queueId)
        {
            return chatAgents.Where(x => x.queueLevels.ContainsKey(queueId)).ToList();            
        }

        private async void Handle_OnPesenceSubscriptionUpdated(object sender, UcwaPresenceSubscriptionEventArgs e)
        {
            await e.presenceSubscription.extendPresenceSubscription(PRESENCE_SUBSCRIPTION_DURATION);
        }

        private void Handle_OnContactPresenceUpdated(object sender, UcwaContactPresenceEventArgs e)
        {
            bool updated = false;
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == e.contact.uri).FirstOrDefault();
            if (chatAgent != null)
            {
                chatAgent.contact = e.contact;
                chatAgent.contactPresence = e.contactPresence;

                if (chatAgent.contactPresence.availability != Availability.Online)
                {
                    if (chatAgent.contactPresence.availability == Availability.None)
                    {
                        if (chatAgent.agentState != AgentState.Offline)
                        {
                            chatAgent.agentState = AgentState.Offline;
                            updated = true;
                        }
                    }
                    else if (chatAgent.agentState != AgentState.Unavailable)
                    {
                        chatAgent.agentState = AgentState.Unavailable;
                        updated = true;
                    }
                }
                else
                {   
                    if (chatAgent.agentState != AgentState.InSession && chatAgent.agentState != AgentState.InvitationPending)
                    {
                        if (chatAgent.agentState != AgentState.Available)
                        {
                            chatAgent.agentState = AgentState.Available;
                            updated = true;
                        }
                    }
                }
            }

            if (updated)
            {   
                sendAgentStatusUpdatedEvent(chatAgent);
            }
        }

        private void sendAgentStatusUpdatedEvent(IChatAgent chatAgent)
        {
            log.Debug("Agent: " + chatAgent.displayName + ". Setting agent state to " + chatAgent.agentState.ToString());
            TenantAgentStatusUpdated?.Invoke(this, new TenantAgentStatusUpdatedEventArgs(tenantId, chatAgents));
            foreach(KeyValuePair < string, int > agentQueueLevel in chatAgent.queueLevels)
            {
                string queueId = agentQueueLevel.Key;
                List<IChatAgent> queueChatAgents = chatAgents.Where(x => x.queueLevels.ContainsKey(queueId)).ToList();
                QueueAgentStatusUpdated?.Invoke(this, new QueueAgentStatusUpdatedEventArgs(tenantId, agentQueueLevel.Key, queueChatAgents));                
            }            
        }

        //public List<ChatAgentStatus> getChatAgentStatusList()
        //{
        //    List<ChatAgentStatus> chatAgentStatusList = new List<ChatAgentStatus>();
        //    foreach (ChatAgent chatAgent in chatAgents)
        //    {
        //        ChatAgentStatus chatAgentStatus = new ChatAgentStatus(chatAgent.displayName, chatAgent.sipUri, chatAgent.agentState, chatAgent.lastSessionEnded, chatAgent.lastSessionOffered, null);
        //        foreach (KeyValuePair<string, int> agentQueueLevel in chatAgent.queueLevels)
        //        {
        //            chatAgentStatus.memberQueueIds.Add(agentQueueLevel.Key);
        //        }
        //        chatAgentStatusList.Add(chatAgentStatus);
        //    }
        //    return chatAgentStatusList;
        //}

        //public List<ChatAgentStatus> getChatAgentStatusListForQueue(string queueId)
        //{
        //    List<ChatAgentStatus> chatAgentStatusList = new List<ChatAgentStatus>();
        //    foreach (ChatAgent chatAgent in chatAgents.Where(x => x.queueLevels.ContainsKey(queueId)))
        //    {
        //        ChatAgentStatus chatAgentStatus = new ChatAgentStatus(chatAgent.displayName, chatAgent.sipUri, chatAgent.agentState, chatAgent.lastSessionEnded, chatAgent.lastSessionOffered, null);                
        //        chatAgentStatus.memberQueueIds.Add(queueId);                
        //        chatAgentStatusList.Add(chatAgentStatus);
        //    }
        //    return chatAgentStatusList;
        //}

        

        public void Handle_OnChatSessionParticipantInvitationDeclined(object sender, OutgoingConversationInvitationEventArgs e)
        {   
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == e.invitedParticipantUri).FirstOrDefault();
            if (chatAgent != null)
            {
                log.Debug("Agent: " + chatAgent.displayName + ". Agent declined chat session invitiation.");
                chatAgent.lastSessionOffered = DateTime.Now;
                if (chatAgent.contactPresence.availability == Availability.Online)
                    chatAgent.agentState = AgentState.Available;
                else
                    chatAgent.agentState = AgentState.Unavailable;

                sendAgentStatusUpdatedEvent(chatAgent);
            }
        }

        public void Handle_OnChatSessionParticipantAdded(object sender, ChatSessionParticipantEventArgs e)
        {
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == e.participantSipUri).FirstOrDefault();
            if (chatAgent != null)
            {
                chatAgent.conversationIds.Add(e.conversationId);
                checkForAgentStatusChange(chatAgent);
            }
        }

        public void Handle_OnChatSessionParticipantDeleted(object sender, ChatSessionParticipantEventArgs e)
        {
            IChatAgent chatAgent = chatAgents.Where(x => x.sipUri == e.participantSipUri).FirstOrDefault();
            if (chatAgent != null)
            {
                chatAgent.conversationIds.RemoveAll(x => x == e.conversationId);
                checkForAgentStatusChange(chatAgent);
            }
        }

        private void checkForAgentStatusChange(IChatAgent chatAgent)
        {
            if (chatAgent.conversationIds.Count == 0)
            {   
                if (chatAgent.contactPresence.availability == Availability.Online)
                {
                    if (chatAgent.agentState != AgentState.Available) 
                    {
                        chatAgent.agentState = AgentState.Available;
                        sendAgentStatusUpdatedEvent(chatAgent);
                    }
                }
                else
                {
                    chatAgent.agentState = AgentState.Unavailable;
                    sendAgentStatusUpdatedEvent(chatAgent);
                }
            }
            else
            {   
                if (chatAgent.agentState != AgentState.InSession)
                {
                    chatAgent.agentState = AgentState.InSession;
                    sendAgentStatusUpdatedEvent(chatAgent);
                }
            }
        }

        public async Task Drain()
        {
            //get all our subscriptions and cancel them   

        }
    }

    public class TenantAgentStatusUpdatedEventArgs : EventArgs
    {
        public string tenantId;
        public List<IChatAgent> chatAgents;

        public TenantAgentStatusUpdatedEventArgs(string tenantId, List<IChatAgent> chatAgents)
        {
            this.tenantId = tenantId;
            this.chatAgents = chatAgents;
        }
    }

    public class QueueAgentStatusUpdatedEventArgs : EventArgs
    {
        public string tenantId;
        public string queueId;
        public List<IChatAgent> chatAgents;

        public QueueAgentStatusUpdatedEventArgs(string tenantId, string queueId, List<IChatAgent> chatAgents)
        {
            this.tenantId = tenantId;
            this.chatAgents = chatAgents;
        }
    }
}
