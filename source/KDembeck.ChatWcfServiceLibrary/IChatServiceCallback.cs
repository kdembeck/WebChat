using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KDembeck.ChatWcfServiceLibrary
{
    public interface IChatServiceCallback
    {
        //our callbacks that we need to define
        //public Action<string, string, string> ChatSessionChatMessageReceivedPlainTextCallback;
        //public Action<string, string, string> ChatSessionChatMessageReceivedHtmlCallback;
        //public Action<string, string> ChatSessionStatusMessageReceivedCallback;
        [OperationContract]
        void ChatSessionChatMessageReceivedPlainTextCallback(string conversationId, string fromDisplayName, string messageText);

        [OperationContract]
        void ChatSessionChatMessageReceivedHtmlCallback(string conversationId, string fromDisplayName, string messageText);

        [OperationContract]
        void ChatSessionStatusMessageReceivedCallback(string conversationId, string messageText);
    }
}
