using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public interface IChatAgentManager
    {
        event EventHandler<ChatAgentManangerInitializedEventArgs> InitializedEvent;

        int getAgentCountForQueueWithState(string queueId, AgentState state);
        List<IChatAgent> getAgentsForQueueWithAgentState(string queueId, AgentState state);
        List<IChatAgent> getAllAgentsForQueue(string queueId);
        List<IChatAgent> getAllAgentsForTenant();
        Task<IChatAgent> getNextAvaiableChatAgentForChatQueue(string queueId);
        Task initialize();
        void drain();
        void OnChatSessionParticipantAdded(string participantSipUri, string conversationId, ChatSessionState chatSessionState);
        void OnChatSessionParticipantDeleted(string participantSipUri, string originallyInvitedParticipantSipUri, string conversationId);
        void OnChatSessionInvitationDeclined(string invitedParticipantUri);
        void OnWebUserLeftConversation(string conversationId);
        void OnChatSessionInvitationStarted(string invitedParticipantUri);
    }

    public class ChatAgentManangerInitializedEventArgs : EventArgs
    {

    }
}
