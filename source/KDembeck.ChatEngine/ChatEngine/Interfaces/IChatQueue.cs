using System.Collections.Generic;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public interface IChatQueue
    {
        bool draining { get; }
        string queueId { get; }
        string queueDisplayName { get; }
        int numberOfWaitingSessions { get; }
        int numberOfActiveSessions { get; }
        int numberOfAbandonedSessions { get; }
        IChatSession currentlyBeingProcessedChatSession { get; }
        IChatSessionQueue waitingChatSessions { get; }
        List<IChatSession> activeChatSessions { get; }
        void queueNewChatSession(IChatSession chatSession);        
        IChatSession getWaitingChatSessionForProcessing();
        void removeChatSession(string conversationId);
        void agentAcceptedChatSessionInvitation(string conversationId);
        void drain();
        

    }
}
