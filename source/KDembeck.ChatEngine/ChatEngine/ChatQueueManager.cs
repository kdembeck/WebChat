using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using KDembeck.ChatEngine.Data;
using KDembeck.UcwaWebApiClient;

namespace KDembeck.ChatEngine
{
    public class ChatQueueManager : IChatQueueManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event EventHandler<ChatQueueManagerInitializedEventArgs> InitializedEvent;
        public List<IChatQueue> chatQueues { get; private set; }  //stop exposing this to the dashboards and give them methods to call instead. We don't need them traversing the lists while they can be modified

        private IChatCommunicationProxy chatCommunicationProxy;
        private IChatAgentManager chatAgentManager;        
        private IDataUtil dataUtil;
        private IUcwaClient ucwaClient;
        private IInstantMessagingUtility instantMessagingUtility;
        private string tenantId;        

        private Timer processWaitingChatSessionsTimer;

        public ChatQueueManager(string tenantId, IDataUtil dataUtil, IUcwaClient ucwaClient, IChatAgentManager chatAgentManager, IChatCommunicationProxy chatCommunicationManager)
        {   
            this.tenantId = tenantId;
            this.dataUtil = dataUtil;
            this.ucwaClient = ucwaClient;
            this.chatAgentManager = chatAgentManager;
            this.chatCommunicationProxy = chatCommunicationManager;
            chatQueues = new List<IChatQueue>();
            instantMessagingUtility = new InstantMessagingUtility(ucwaClient.application.communication, ucwaClient.events);           
        }

        public void initialize()
        {
            List<QueueInfo> queueInfos = dataUtil.getAllQueuesForTenant(tenantId);
            foreach (QueueInfo queueInfo in queueInfos)
            {
                log.Debug("Chat queue: " + queueInfo.queueName + " with queue id: " + queueInfo.queueId + " found for tenant with id: " + tenantId);
                IChatQueue newChatQueue = new ChatQueue(queueInfo.queueId, queueInfo.queueName, dataUtil);
                chatQueues.Add(newChatQueue);
            }
            processWaitingChatSessionsTimer = new Timer();
            processWaitingChatSessionsTimer.Elapsed += Handle_processWaitingChatSessionsTimer_Elapsed;
            processWaitingChatSessionsTimer.Interval = 1000;
            processWaitingChatSessionsTimer.Start();
            log.Debug("Chat Queue Manager initialized for tenant " + tenantId);
            InitializedEvent?.Invoke(this, new ChatQueueManagerInitializedEventArgs());
        }

        private async Task processWaitingChatSessions()
        {
            //log.Debug("Checking for waiting chat sessions for processing");

            List<IChatSession> waitingChatSessionsForProcessing = new List<IChatSession>();
            foreach (IChatQueue chatQueue in chatQueues)
            {
                IChatSession waitingChatSession = chatQueue.getWaitingChatSessionForProcessing();
                if (waitingChatSession != null)
                {
                    waitingChatSessionsForProcessing.Add(waitingChatSession);
                }
            }

            if (waitingChatSessionsForProcessing.Count > 0)
            {
                //log.Debug("Waiting chat sessions found for processing");
                waitingChatSessionsForProcessing.OrderBy(x => x.queuedTime);
                foreach (IChatSession waitingChatSession in waitingChatSessionsForProcessing)
                {
                    //log.Debug("Checking for available agents for waiting session with conversation id: " + waitingChatSession.conversationId + " in queue with queue id: " + waitingChatSession.queueId);
                    if (chatAgentManager.getAgentCountForQueueWithState(waitingChatSession.queueId, AgentState.Available) > 0)
                    {
                        IChatAgent chatAgent = await chatAgentManager.getNextAvaiableChatAgentForChatQueue(waitingChatSession.queueId);
                        if (chatAgent != null)
                        {   
                            waitingChatSession.OutgoingConversationInvitationStarted += Handle_OnChatSessionOutgoingInvitationStarted;
                            waitingChatSession.ChatSessionEnded += Handle_OnChatSessionEnded;
                            await waitingChatSession.inviteParticipant(chatAgent.sipUri, chatAgent.displayName);
                            return;
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        //log.Debug("No available agents found for waiting session with conversation id: " + waitingChatSession.conversationId + " in queue with queue id: " + waitingChatSession.queueId);
                    }
                }
            }
            else
            {
                //log.Debug("No waiting chat sessions were found for processing");
            }
        }

        public string queueNewChatSession(string webUserName, string webUserEmail, string extendedData, string queueId)
        {
            string conversationId = string.Empty;
            IChatQueue chatQueue = chatQueues.Where(x => x.queueId == queueId).FirstOrDefault();
            if (chatQueue != null)
            {   
                IChatSession chatSession = new ChatSession(webUserName, webUserEmail, extendedData, queueId, chatQueue.queueDisplayName, ucwaClient.application.communication, ucwaClient.events);
                
                conversationId = chatSession.conversationId;
                chatQueue.queueNewChatSession(chatSession);
                chatCommunicationProxy.addSession(chatSession);
                chatSession.initialize();
            }
            return conversationId;
        }

        public void drain()
        {
            //unsubscribe from events              
            processWaitingChatSessionsTimer.Stop();
            foreach (IChatQueue chatQueue in chatQueues)
            {
                chatQueue.drain();
            }
            instantMessagingUtility.drain();
        }

        private async void Handle_processWaitingChatSessionsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            processWaitingChatSessionsTimer.Stop();
            await processWaitingChatSessions();
            processWaitingChatSessionsTimer.Start();
        }

        private void Handle_OnChatSessionEnded(object sender, ConversationEndedEventArgs e)
        {
            //unsubscribe from stuff or just let it go? We shouldn't be causing any memory leaks if we don't unsubscribe here
        }

        private void Handle_OnChatSessionParticipantAdded(object sender, ChatSessionParticipantEventArgs e)
        {   
            chatAgentManager.OnChatSessionParticipantAdded(e.participantSipUri, e.conversationId, e.chatSessionState);
        }

        private void Handle_OnChatSessionParticipantDeleted(object sender, ChatSessionParticipantEventArgs e)
        {   
            chatAgentManager.OnChatSessionParticipantDeleted(e.participantSipUri,e.originallyInvitedParticipantSipUri, e.conversationId);
        }

        private void Handle_OnChatSessionOutgoingInvitationStarted(object sender, OutgoingConversationInvitationEventArgs e)
        {
            IChatSession chatSession = (IChatSession)sender;
            chatSession.OutgoingConversationInvitationStarted -= Handle_OnChatSessionOutgoingInvitationStarted;
            chatSession.OutgoingConversationInvitationDeclined += Handle_OnChatSessionOutgoingInvitationDeclined;
            chatSession.ChatSessionParticipantAdded += Handle_OnChatSessionParticipantAdded;
            chatSession.ChatSessionParticipantDeleted += Handle_OnChatSessionParticipantDeleted;
            chatSession.WebUserLeftConversationEvent += Handle_OnWebUserLeftConversationEvent;
            chatAgentManager.OnChatSessionInvitationStarted(e.invitedParticipantUri);
        }

        private void Handle_OnChatSessionOutgoingInvitationDeclined(object sender, OutgoingConversationInvitationEventArgs e)
        {
            IChatSession chatSession = (IChatSession)sender;         
            chatSession.OutgoingConversationInvitationDeclined -= Handle_OnChatSessionOutgoingInvitationDeclined;
            chatSession.ChatSessionParticipantAdded -= Handle_OnChatSessionParticipantAdded;
            chatSession.ChatSessionParticipantDeleted -= Handle_OnChatSessionParticipantDeleted;
            chatSession.WebUserLeftConversationEvent -= Handle_OnWebUserLeftConversationEvent;
            chatAgentManager.OnChatSessionInvitationDeclined(e.invitedParticipantUri);

            //SEND AN IM TO THE USER THAT AN INVITATION WAS DECLINED AND THAT THEY WILL BE MADE AVAILABLE AGAIN IF THEY DON'T
            //CHANGE THEIR SKYPE STATUS TO SOMETHING OTHER THAN AVAILABLE
            instantMessagingUtility.sendInstantMessageToAgent(e.invitedParticipantUri, "Missed or Declined Chat Invitation", "You missed or declined a chat session invitation. You will be made available for additional invitations shortly. To stop receiving invitations, change your availability status in your Skype client to something other than Available.");

        }

        private void Handle_OnWebUserLeftConversationEvent(object sender, WebUserLeftConversationEventArgs e)
        {
            IChatSession chatSession = (IChatSession)sender;
            chatSession.WebUserLeftConversationEvent -= Handle_OnWebUserLeftConversationEvent;
            chatAgentManager.OnWebUserLeftConversation(e.conversationId);
        }

        private void sendImToAgent(string agentSipUri, string messageText)
        {

        }
    }
}
