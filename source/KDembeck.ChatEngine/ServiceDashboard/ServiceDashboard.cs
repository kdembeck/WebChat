using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.ChatEngine.Data;
using KDembeck.ChatEngine;

namespace KDembeck.ChatEngine.Dashboard
{
    public interface IServiceDashboard
    {
        event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedPlainTextEvent;
        event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedHtmlEvent;
        event EventHandler<ConversationStatusMessageReceivedEventArgs> ChatSessionStatusMessageReceivedEvent;
        event EventHandler<ConversationEndedEventArgs> ChatSessionEndedEvent;

        string queueNewMessagingSession(string chatUserName, string chatUserEmail, string extendedData, string queueId, string conversationId = null);
        void sendMessageToAgent(string messageText, string conversationId);
        void webUserLeftConversation(string conversationId);
    }

    public class ServiceDashboard : IServiceDashboard
    {
        private List<IChatTenant> tenantChatList;
        private List<TenantConversation> tenantConversations;   
        private List<TenantQueue> tenantQueues;

        public event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedPlainTextEvent;
        public event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedHtmlEvent;
        public event EventHandler<ConversationStatusMessageReceivedEventArgs> ChatSessionStatusMessageReceivedEvent;
        public event EventHandler<ConversationEndedEventArgs> ChatSessionEndedEvent;

        internal ServiceDashboard(List<IChatTenant> tenantChatList)
        {
            this.tenantChatList = tenantChatList;
            tenantQueues = new List<TenantQueue>();
            tenantConversations = new List<TenantConversation>();
            foreach (IChatTenant chatTenant in tenantChatList)
            {
                chatTenant.chatCommunicationProxy.ChatSessionEnded += Handle_OnChatSessionEnded;
                chatTenant.chatCommunicationProxy.ChatSessionChatMessageReceivedHtml += Handle_OnConversationChatMessageReceivedHtml;
                chatTenant.chatCommunicationProxy.ChatSessionChatMessageReceivedPlainText += Handle_OnConversationChatMessageReceivedPlainText;
                chatTenant.chatCommunicationProxy.ChatSessionStatusMessageReceived += Handle_OnChatSessionStatusMessageRecevied;
            }
        }

        public void getTenantQueues()
        {
            tenantQueues.Clear();
            foreach (IChatTenant chatTenant in tenantChatList)
            {
                foreach (IChatQueue chatQueue in chatTenant.chatQueueManager.chatQueues)
                {
                    tenantQueues.Add(new TenantQueue(chatQueue.queueId, chatTenant.tenantId));
                }
            }
        }

        public string queueNewMessagingSession(string chatUserName, string chatUserEmail, string extendedData, string queueId, string conversationId=null)
        {   
            getTenantQueues();            
            TenantQueue tenantQueue = tenantQueues.Where(q => q.queueId == queueId).FirstOrDefault();
            if (tenantQueue != null)
            {   
                IChatTenant chatTenant = tenantChatList.Where(x => x.tenantId == tenantQueue.tenantId).FirstOrDefault();
                if (chatTenant != null)
                {
                    string newConversationId = chatTenant.chatQueueManager.queueNewChatSession(chatUserName, chatUserEmail, extendedData, queueId); 
                    tenantConversations.Add(new TenantConversation(newConversationId, tenantQueue.queueId, tenantQueue.tenantId));
                    return newConversationId;
                }                
            }
            return string.Empty;
        }

        public void sendMessageToAgent(string messageText, string conversationId)
        {
            string tenantId;
            TenantConversation tenantConversation = tenantConversations.Where(x => x.conversationId == conversationId).FirstOrDefault();
            if (tenantConversation != null)
            {
                tenantId = tenantConversation.tenantId;
                IChatTenant chatTenant = tenantChatList.Where(x => x.tenantId == tenantId).FirstOrDefault();
                if (chatTenant != null)
                {
                    chatTenant.chatCommunicationProxy.sendMessageToAgent(messageText, conversationId);
                }
            }
        }

        public void webUserLeftConversation(string conversationId)
        {
            string tenantId;
            TenantConversation tenantConversation = tenantConversations.Where(x => x.conversationId == conversationId).FirstOrDefault();
            if (tenantConversation != null)
            {
                tenantId = tenantConversation.tenantId;
                IChatTenant chatTenant = tenantChatList.Where(x => x.tenantId == tenantId).FirstOrDefault();
                if (chatTenant != null)
                {
                    chatTenant.chatCommunicationProxy.webUserLeftConversation(conversationId);
                }
            }
        }

        private void Handle_OnConversationChatMessageReceivedPlainText(object sender, ConversationMessageReceivedEventArgs e)
        {   
            ChatSessionChatMessageReceivedPlainTextEvent?.Invoke(sender, e);
        }

        private void Handle_OnConversationChatMessageReceivedHtml(object sender, ConversationMessageReceivedEventArgs e)
        {   
            ChatSessionChatMessageReceivedHtmlEvent?.Invoke(sender, e);
        }
        
        private void Handle_OnChatSessionStatusMessageRecevied(object sender, ConversationStatusMessageReceivedEventArgs e)
        {   
            ChatSessionStatusMessageReceivedEvent?.Invoke(sender, e);
        }

        private void Handle_OnChatSessionEnded(object sender, ConversationEndedEventArgs e)
        {
            tenantConversations.RemoveAll(x => x.conversationId == e.conversationId);
            ChatSessionEndedEvent?.Invoke(sender, e);
        }

        private void Handle_OnChatTenantInitialized(object sender, ChatTenantInitializedEventArgs e)
        {
            
        }

        private class TenantConversation
        {
            public string tenantId;
            public string queueId;
            public string conversationId;

            public TenantConversation(string conversationId, string queueId, string tenantId)
            {
                this.conversationId = conversationId;
                this.queueId = queueId;
                this.tenantId = tenantId;
            }
        }
    }

    internal class TenantQueue
    {
        public string tenantId;
        public string queueId;

        public TenantQueue(string queueId, string tenantId)
        {
            this.queueId = queueId;
            this.tenantId = tenantId;
        }
    }

    internal class TenantAgent
    {
        public string tenantId;
        public string agentSipUri;        

        public TenantAgent(string agentSipUri, string tenantId)
        {
            this.tenantId = tenantId;
            this.agentSipUri = agentSipUri;            
        }
    }

    internal class QueueAgent
    {      
        public string queueId;
        public string agentSipUri;

        public QueueAgent(string agentSipUri, string queueId)
        {
            this.queueId = queueId;
            this.agentSipUri = agentSipUri;
        }
    }
    
}
