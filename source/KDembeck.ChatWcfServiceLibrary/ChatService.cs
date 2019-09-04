using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KDembeck.ChatEngine;
using KDembeck.ChatEngine.Dashboard;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KDembeck.ChatWcfServiceLibrary
{   
    public class ChatService : IChatService
    {
        //private IChatEngine chatEngine;
        private IServiceDashboard serviceDashboard;
        private IStatusDashboard statusDashboard;

        public ChatService(IChatEngine chatEngine)
        {
            //this.chatEngine = chatEngine;
            this.serviceDashboard = chatEngine.ServiceDashboard;
            this.statusDashboard = chatEngine.StatusDashboard;
        }

        public string queueNewMessagingSession(string chatUserName, string chatUserEmail, string extendedData, string queueId, string conversationId)
        {
            return serviceDashboard.queueNewMessagingSession(chatUserName, chatUserEmail, extendedData, queueId, conversationId);
        }

        public string queueNewMessagingSession(string chatUserName, string chatUserEmail, string extendedData, string queueId)
        {
            return serviceDashboard.queueNewMessagingSession(chatUserName, chatUserEmail, extendedData, queueId);
        }

        public void sendMessageToAgent(string messageText, string conversationId)
        {
            serviceDashboard.sendMessageToAgent(messageText, conversationId);
        }

        public void webUserLeftConversation(string conversationId)
        {
            serviceDashboard.webUserLeftConversation(conversationId);
        }

        public Dictionary<string, string> getTenantsIdsAndDomainNames()
        {
            return statusDashboard.getTenantsIdsAndDomainNames();
        }

        public Dictionary<string, string> getQueueIdsAndNamesForTenant(string tenantId)
        {
            return statusDashboard.getQueueIdsAndNamesForTenant(tenantId);
        }

        public svcQueueStatus getQueueStatus(string queueId)
        {
            QueueStatus queueStatus = statusDashboard.getQueueStatus(queueId);
            string queueStatusSerialized = JsonConvert.SerializeObject(queueStatus);
            svcQueueStatus returnQueueStatus = new svcQueueStatus();
            JsonConvert.PopulateObject(queueStatusSerialized, returnQueueStatus);
            return returnQueueStatus;
        }
    }
}
