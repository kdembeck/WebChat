using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.ChatEngine.Dashboard;

namespace KDembeck.ChatEngine
{
    //This class exists just to make it easy to pass messages back and forth between chat sessions and the service dashboard 
    //without having to search for chat sessions within different queues. A reference for each chat session gets passed
    //in when the session is queued. 
    public class ChatCommunicationProxy : IChatCommunicationProxy
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedPlainText;
        public event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedHtml;
        public event EventHandler<ConversationStatusMessageReceivedEventArgs> ChatSessionStatusMessageReceived;
        public event EventHandler<ConversationEndedEventArgs> ChatSessionEnded;

        private List<IChatSession> chatSessions;

        public ChatCommunicationProxy()
        {            
            chatSessions = new List<IChatSession>();
        }

        public void addSession(IChatSession chatSession)
        {
            chatSessions.Add(chatSession);
            chatSession.ChatSessionStatusMessageReceived += Handle_OnChatSessionStatusMessageReceivedEvent;
            chatSession.ChatSessionChatMessageReceivedHtml += Handle_OnChatSessionChatMessageReceivedHtmlEvent;
            chatSession.ChatSessionChatMessageReceivedPlainText += Handle_OnChatSessionChatMessageReceivedPlainTextEvent;
            chatSession.ChatSessionEnded += Handle_OnChatSessionEnded;
        }

        public void sendMessageToAgent(string messageText, string conversationId)
        {
            IChatSession chatSession = chatSessions.Where(x => x.conversationId == conversationId).FirstOrDefault();
            if (chatSession != null)
            {
                chatSession.sendChatMessageToAgent(messageText);
            }
        }

        public void webUserLeftConversation(string conversationId)
        {
            IChatSession chatSession = chatSessions.Where(x => x.conversationId == conversationId).FirstOrDefault();
            if (chatSession != null)
            {
                chatSession.webUserLeftConversation();
            }
        }

        public void drain()
        {
            foreach (IChatSession chatSession in chatSessions)
            {   
                chatSession.ChatSessionStatusMessageReceived -= Handle_OnChatSessionStatusMessageReceivedEvent;
                chatSession.ChatSessionChatMessageReceivedHtml -= Handle_OnChatSessionChatMessageReceivedHtmlEvent;
                chatSession.ChatSessionChatMessageReceivedPlainText -= Handle_OnChatSessionChatMessageReceivedPlainTextEvent;
                chatSession.ChatSessionEnded -= Handle_OnChatSessionEnded;
            }
            chatSessions.Clear();
        }

        private void Handle_OnChatSessionEnded(object sender, ConversationEndedEventArgs e)
        {   
            ChatSessionEnded?.Invoke(this, e);
            IChatSession chatSession = chatSessions.Where(x => x.conversationId == e.conversationId).FirstOrDefault();
            if (chatSession != null)
            {
                chatSession.ChatSessionStatusMessageReceived -= Handle_OnChatSessionStatusMessageReceivedEvent;
                chatSession.ChatSessionChatMessageReceivedHtml -= Handle_OnChatSessionChatMessageReceivedHtmlEvent;
                chatSession.ChatSessionChatMessageReceivedPlainText -= Handle_OnChatSessionChatMessageReceivedPlainTextEvent;
                chatSessions.Remove(chatSession);
            }
        }

        private void Handle_OnChatSessionChatMessageReceivedPlainTextEvent(object sender, ConversationMessageReceivedEventArgs e)
        {   
            ChatSessionChatMessageReceivedPlainText?.Invoke(this, e);
        }

        private void Handle_OnChatSessionChatMessageReceivedHtmlEvent(object sender, ConversationMessageReceivedEventArgs e)
        {
            ChatSessionChatMessageReceivedHtml?.Invoke(this, e);
        }

        private void Handle_OnChatSessionStatusMessageReceivedEvent(object sender, ConversationStatusMessageReceivedEventArgs e)
        {   
            ChatSessionStatusMessageReceived?.Invoke(this, e);
        }
    }
}
