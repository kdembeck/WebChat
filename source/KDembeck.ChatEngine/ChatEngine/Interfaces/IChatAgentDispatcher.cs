using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public interface IChatAgentDispatcher
    {
        event EventHandler<TenantAgentStatusUpdatedEventArgs> TenantAgentStatusUpdated;
        event EventHandler<QueueAgentStatusUpdatedEventArgs> QueueAgentStatusUpdated;
        List<IChatAgent> chatAgents { get; }
        Task initialize();
        Task<IChatAgent> findAgentForSession(string queueId);
        List<IChatAgent> getChatAgentsForQueue(string queueId);
        Task Drain();
        void Handle_OnChatSessionParticipantInvitationDeclined(object sender, OutgoingConversationInvitationEventArgs e);
        void Handle_OnChatSessionParticipantAdded(object sender, ChatSessionParticipantEventArgs e);
        void Handle_OnChatSessionParticipantDeleted(object sender, ChatSessionParticipantEventArgs e);       
    }
}
