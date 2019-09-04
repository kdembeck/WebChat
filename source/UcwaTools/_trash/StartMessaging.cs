//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
//using UcwaTools.EventChannel;

//namespace UcwaTools
//{   
//    public delegate void OnConversationConnectedEventHandler(string operationId, string threadId, string conversation);

//    public class StartMessagingResource : IResource
//    {
//        public OnConversationConnectedEventHandler OnConversationConnected;

//        public string ResourceString { get; set; }
//        public string rel { get; set; }
//        public string href { get; set; }

//        public string operationId;
//        public string to;
//        public string importance;
//        public string subject;
//        public string threadId;
//        public string message;
        
//        private Dictionary<string, MessagingInvitationResource> _messagingInvitations;
//        private Dictionary<string, string> _messagingThreadIdsAndOperatingIds;
        
//        private HttpHelper _httpHelper;
//        private EventChannelListener _eventChannelListener; 

//        public StartMessagingResource(HttpHelper httpHelper, EventChannelListener eventChannelListener)
//        {
//            _httpHelper = httpHelper;
//            _eventChannelListener = eventChannelListener;
//            _eventChannelListener.OnOutgoingMessageInvitationReceived += HandleOutgoingInvitationEvent;
//            _eventChannelListener.OnConversationConnectedEventReceived += HandleConversationConnectedEvent;

//            _messagingInvitations = new Dictionary<string, MessagingInvitationResource>();
//            _messagingThreadIdsAndOperatingIds = new Dictionary<string, string>();
//        }

//        public async Task<string> GetResource(string resourceUri)
//        {
//            ResourceString = "";
//            try
//            {
//                href = resourceUri;
//                ResourceString = await _httpHelper.HttpGetAction(_httpHelper._applicationRootUri + resourceUri);
//                FillResourceValues();
//            }
//            catch (Exception ex) { }
//            return ResourceString;
//        }

//        public void FillResourceValues(string resourceString="")
//        {
//            if (resourceString != "")
//                ResourceString = resourceString;

//            try
//            {
//                dynamic resourceObject = JObject.Parse(ResourceString);

//                if (resourceObject.rel != null)
//                    rel = resourceObject.rel;
//                if (resourceObject.href != null)
//                    href = resourceObject.href;

//                operationId = resourceObject.operationId;
//                to = resourceObject.to;
//                importance = resourceObject.importance;
//                subject = resourceObject.subject;
//                threadId = resourceObject.threadId;
//                message = resourceObject.message;
//            }
//            catch (Exception ex) { }
//        }

//        public override string ToString()
//        {
//            if (ResourceString != null)
//                return ResourceString;
//            else
//                return "";
//        }

//        public async void Begin(StartMessagingInformation startMessagingInfo)
//        {
//            try
//            {
//                string jsonPostData = JsonConvert.SerializeObject(startMessagingInfo);
//                string postResponse = await _httpHelper.HttpPostAction(_httpHelper._applicationRootUri + href, jsonPostData);
//            }
//            catch (Exception ex) { }
//        }

//        private void HandleOutgoingInvitationEvent(string _messagingInvitationObjectString)
//        {
//            dynamic resourceObject = JObject.Parse(_messagingInvitationObjectString);

//            if (!_messagingInvitations.ContainsKey(resourceObject.operationId))
//            {
//                try
//                {   
//                    _messagingInvitations.Add(resourceObject.operationId, new MessagingInvitationResource(_httpHelper, _eventChannelListener));
//                    _messagingInvitations[resourceObject.operationId].FillResourceValues(_messagingInvitationObjectString);
//                    _messagingThreadIdsAndOperatingIds.Add(resourceObject.threadId, resourceObject.operationId);
//                }
//                catch (Exception ex) { }
//            }
//        }

//        private void HandleConversationConnectedEvent(string conversationResourceString)
//        {
//            try
//            {
//                dynamic resourceObject = JObject.Parse(conversationResourceString);

//                if (_messagingInvitations.ContainsKey(resourceObject.threadId))
//                {
//                    OnConversationConnected.Invoke(_messagingThreadIdsAndOperatingIds[resourceObject.threadId], resourceObject.threadId, conversationResourceString);

//                    _messagingInvitations.Remove(_messagingThreadIdsAndOperatingIds[resourceObject.threadId]);
//                    _messagingThreadIdsAndOperatingIds.Remove(resourceObject.threadId);
//                }
//            }
//            catch (Exception ex) { }
//        }
//    }

     
//}
