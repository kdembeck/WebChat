using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace KDembeck.ChatWcfServiceLibrary
{   
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        string queueNewMessagingSession(string chatUserName, string chatUserEmail, string extendedData, string queueId, string conversationId);

        [OperationContract]
        string queueNewMessagingSession(string chatUserName, string chatUserEmail, string extendedData, string queueId);

        [OperationContract]
        void sendMessageToAgent(string messageText, string conversationId);

        [OperationContract]
        void webUserLeftConversation(string conversationId);

        [OperationContract]
        Dictionary<string, string> getTenantsIdsAndDomainNames();

        [OperationContract]
        Dictionary<string, string> getQueueIdsAndNamesForTenant(string tenantId);

        [OperationContract]
        svcQueueStatus getQueueStatus(string queueId);
    }
}
