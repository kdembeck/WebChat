using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public interface IChatCommunicationProxy
    {
        event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedPlainText;
        event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedHtml;
        event EventHandler<ConversationStatusMessageReceivedEventArgs> ChatSessionStatusMessageReceived;
        event EventHandler<ConversationEndedEventArgs> ChatSessionEnded;

        void webUserLeftConversation(string conversationId);
        void sendMessageToAgent(string messageText, string conversationId);
        void addSession(IChatSession chatSession);
        void drain();
    }
}
