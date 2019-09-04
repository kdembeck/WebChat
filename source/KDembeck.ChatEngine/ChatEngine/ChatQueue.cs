using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using KDembeck.ChatEngine.Data;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.EventChannel;
using KDembeck.UcwaWebApiClient.Resources;

namespace KDembeck.ChatEngine
{
    public class ChatQueue : IChatQueue
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool draining { get; private set; }
        public string queueId { get; private set; }
        public string queueDisplayName { get; private set; }

        private IDataUtil dataUtil;     //might need this to pull queue config values? 

        public int numberOfWaitingSessions
        { get
            {
                if (currentlyBeingProcessedChatSession != null)
                    return waitingChatSessions.Count + 1;
                else
                    return waitingChatSessions.Count;
            }
        }
        public int numberOfActiveSessions { get { return activeChatSessions.Count; } }
        public int numberOfAbandonedSessions { get; private set; }
        public IChatSessionQueue waitingChatSessions { get; private set; }
        public List<IChatSession> activeChatSessions { get; private set; }
        public IChatSession currentlyBeingProcessedChatSession { get; private set; }

        private bool waitingChatSessionsDrained = false;

        public ChatQueue(string queueId, string queueDisplayName, IDataUtil dataUtil)
        {
            this.queueId = queueId;
            this.queueDisplayName = queueDisplayName;
            this.dataUtil = dataUtil;
            activeChatSessions = new List<IChatSession>();
            waitingChatSessions = new ChatSessionQueue();
            waitingChatSessions.ChatSessionQueueDrained += Handle_OnChatSessionQueueDrained;
            draining = false;
        }

        public void queueNewChatSession(IChatSession chatSession)
        {
            if (!draining)
            {
                log.Debug("Queueing new chat coversation with conversation id: " + chatSession.conversationId);
                chatSession.ChatSessionEnded += Handle_OnChatSessionEndedEvent;
                waitingChatSessions.Enqueue(chatSession);
            }
        }

        public IChatSession getWaitingChatSessionForProcessing()
        {
            if (!draining)
            {
                if (currentlyBeingProcessedChatSession == null && waitingChatSessions.Count > 0)
                {
                    currentlyBeingProcessedChatSession = waitingChatSessions.Dequeue();
                    if (currentlyBeingProcessedChatSession != null)
                    {
                        currentlyBeingProcessedChatSession.OutgoingConversationInvitationAccepted += Handle_OnOutgoingConversationInvitationAcceptedEvent;
                    }
                }
                return currentlyBeingProcessedChatSession;
            }
            else
                return null;
        }

        public void removeChatSession(string conversationId)
        {
            if (!draining)
            {
                if (waitingChatSessions.Remove(conversationId) == 0)
                {
                    if (activeChatSessions.RemoveAll(x => x.conversationId == conversationId) == 0)
                    {
                        if (currentlyBeingProcessedChatSession != null && currentlyBeingProcessedChatSession.conversationId == conversationId)
                            currentlyBeingProcessedChatSession = null;
                    }
                }
            }
        }

        public async void drain()
        {
            log.Debug("Draining queue:  " + queueDisplayName + " with queue id: " + queueId);
            draining = true;

            if (currentlyBeingProcessedChatSession != null)
            {
                await currentlyBeingProcessedChatSession.drain();
                currentlyBeingProcessedChatSession = null;
            }

            await waitingChatSessions.Drain();
            waitingChatSessions = null;

            if (activeChatSessions.Count > 0)
            {
                foreach (ChatSession chatSession in activeChatSessions)
                {
                    await chatSession.drain();        
                }
                activeChatSessions.Clear();
            }

            //clear our lists here so that we're not accidentally modifying lists in the session ended event handler
        }

        public void agentAcceptedChatSessionInvitation(string conversationId)
        {
            if (!draining)
            {
                if (currentlyBeingProcessedChatSession.conversationId == conversationId)
                {
                    activeChatSessions.Add(currentlyBeingProcessedChatSession);
                    currentlyBeingProcessedChatSession = null;
                }
            }
        }

        private void Handle_OnOutgoingConversationInvitationAcceptedEvent(object sender, OutgoingConversationInvitationEventArgs e)
        {
            if (!draining)
            {
                //move the processing conversation to active conversations
                if (currentlyBeingProcessedChatSession.conversationId == e.conversationId)
                {
                    currentlyBeingProcessedChatSession.OutgoingConversationInvitationAccepted -= Handle_OnOutgoingConversationInvitationAcceptedEvent;
                    activeChatSessions.Add(currentlyBeingProcessedChatSession);
                    currentlyBeingProcessedChatSession = null;
                }
            }
        }

        private void Handle_OnChatSessionEndedEvent(object sender, ConversationEndedEventArgs e)
        {
            //remove from our lists            
            if (currentlyBeingProcessedChatSession != null && currentlyBeingProcessedChatSession.conversationId == e.conversationId)
            {
                currentlyBeingProcessedChatSession.ChatSessionEnded -= Handle_OnChatSessionEndedEvent;
                currentlyBeingProcessedChatSession.OutgoingConversationInvitationAccepted -= Handle_OnOutgoingConversationInvitationAcceptedEvent;
                currentlyBeingProcessedChatSession = null;
            }
            else
            {
                IChatSession chatSession = activeChatSessions.Where(x => x.conversationId == e.conversationId).FirstOrDefault();
                if (chatSession != null)
                {
                    chatSession.ChatSessionEnded -= Handle_OnChatSessionEndedEvent;
                    activeChatSessions.RemoveAll(x => x.conversationId == e.conversationId);
                }
                else
                {
                    chatSession = waitingChatSessions.Find(e.conversationId);
                    if (chatSession != null)
                    {
                        chatSession.ChatSessionEnded -= Handle_OnChatSessionEndedEvent;
                        waitingChatSessions.Remove(e.conversationId);
                    }
                }
            }
        }

        private void Handle_OnChatSessionQueueDrained(object sender, EventArgs e)
        {
            waitingChatSessionsDrained = true;
            waitingChatSessions.ChatSessionQueueDrained -= Handle_OnChatSessionQueueDrained;
            waitingChatSessions = null;
        }
    }
}
