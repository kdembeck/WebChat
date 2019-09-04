using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UcwaTools.Utilities;


namespace UcwaTools
{  

    internal class CommunicationController
    {
        private HttpHelper _httpHelper;        

        public CommunicationController(HttpHelper httpHelper)
        {
            _httpHelper = new HttpHelper();
            _httpHelper.ApplicationRootUri = httpHelper.ApplicationRootUri;
            _httpHelper.AuthenticationResult = httpHelper.AuthenticationResult;            
        }

        public async Task<string> StartMessaging(string startMessagingUri, string to, string subject, string importance, string message="", string operationId = "")
        {
            if (operationId == "")
                operationId = Guid.NewGuid().ToString();
            try
            {
                StartMessagingInformation startMessagingInfo = new StartMessagingInformation();
                startMessagingInfo.to = to;
                startMessagingInfo.subject = subject;
                startMessagingInfo.importance = importance;
                startMessagingInfo.operationId = operationId;                

                string jsonPostData = JsonConvert.SerializeObject(startMessagingInfo);
                await _httpHelper.HttpPostAction(_httpHelper.ApplicationRootUri + startMessagingUri, jsonPostData);
            }
            catch (Exception ex) { }
            return operationId;
        }

        public async Task<string> startOnlineMeeting(string startOnlineMeetingUri, string Importance, string Subject, string ThreadId = "", string OperationId = "")
        {
            if (OperationId == "")
                OperationId = Guid.NewGuid().ToString();

            string startOnlineMeetingJson = JsonConvert.SerializeObject(new {
                operationId = OperationId,
                importance = Importance,
                subject = Subject,
                threadId = ThreadId //leave this empty to create a whole new online meeting
            });

            await _httpHelper.HttpPostAction(_httpHelper.ApplicationRootUri + startOnlineMeetingUri, startOnlineMeetingJson);

            return OperationId;
        }
    }

    public struct StartMessagingInformation
    {
        public string operationId;  //DO I NEED THIS HERE? DON'T I GENERATE THIS AND RETURN IT TO THE CLIENT IN MY START MESSAGING METHOD?
        public string to;
        public string subject;
        public string threadId;
        public string importance;        
        public string message;
    }
}
