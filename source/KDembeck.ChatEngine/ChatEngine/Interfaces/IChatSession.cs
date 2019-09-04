using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Resources;

namespace KDembeck.ChatEngine
{
    public interface IChatSession
    {        
        event EventHandler<ConversationEndedEventArgs> ChatSessionEnded;
        event EventHandler<OutgoingConversationInvitationEventArgs> OutgoingConversationInvitationStarted;
        event EventHandler<OutgoingConversationInvitationEventArgs> OutgoingConversationInvitationAccepted;
        event EventHandler<OutgoingConversationInvitationEventArgs> OutgoingConversationInvitationDeclined;
        event EventHandler<ChatSessionParticipantEventArgs> ChatSessionParticipantAdded;
        event EventHandler<ChatSessionParticipantEventArgs> ChatSessionParticipantDeleted;
        event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedPlainText;
        event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedHtml;
        event EventHandler<ConversationStatusMessageReceivedEventArgs> ChatSessionStatusMessageReceived;
        event EventHandler<WebUserLeftConversationEventArgs> WebUserLeftConversationEvent;        

        ChatSessionState sessionState { get; }
        string invitedAgentDisplayName { get; }
        List<string> participantDisplayNames { get; }
        string queueId { get; }
        string queueName { get; }
        string webUserName { get; }
        string webUserEmail { get; }
        string extendedData { get; }
        bool initialized { get; }
        string conversationId { get; }
        string threadId { get; }
        DateTime queuedTime { get; }
        DateTime messagingStartTime { get; }
        DateTime messagingEndTime { get; }
        MessageHistory messageHistory { get; }

        Task initialize();
        Task inviteParticipant(string chatAgentSipUri, string chatAgentDisplayName);
        Task sendChatMessageToAgent(string messageText);
        Task webUserLeftConversation();
        Task drain();
    }
}
