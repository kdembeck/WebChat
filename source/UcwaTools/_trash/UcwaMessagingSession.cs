using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;

namespace UcwaTools
{
    public delegate void UcwaIcomingConversationMessageReceivedEventHandler(string messageText);
    public delegate void MessageSessionOnConnectedEventHandler(string _threadId, string _operationId);

    public enum UcwaMessagingSessionState { Starting, Inviting, Connected, Closing, Closed};

    internal class UcwaMessagingSession
    {
        public UcwaIcomingConversationMessageReceivedEventHandler OnIncomingConversationMessageReceived;
        public MessageSessionOnConnectedEventHandler OnMessageSessionConnected;
        
        private string _threadId;
        private string _operationId;
                
        private string _recipientSipUri;
        private string _messageSubject;
        private string _startMessageText;
        private string _importance;        

        private EventChannelListener _ucwaEventChannelListener;
        private HttpClient _httpClient;
        private AuthenticationResult _ucwaAuthenticationResult;
        private string _applicationRootUri;

        private string _Uri;
        private string _stopMessagingUri;
        private string _setIsTypingUri;
        private string _typingParticipantsUri;

        private string _applicationsResource;

        UcwaMessagingSessionState _ucwaMessagingSessionState;
        public UcwaMessagingSessionState ucwaMessagingSessionState { get{ return _ucwaMessagingSessionState; } } 
        public string OperationId { get { return _operationId; } }   
        
        public UcwaMessagingSession(EventChannelListener ucwaEventChannelListener, HttpClient httpClient, string applicationsResource, string applicationRootUri, AuthenticationResult ucwaAuthenticationResult)
        {
            try
            {
                _operationId = Guid.NewGuid().ToString();
                _httpClient = httpClient;
                _ucwaAuthenticationResult = ucwaAuthenticationResult;
                _applicationRootUri = applicationRootUri;
                _applicationsResource = applicationsResource;

                //_ucwaEventChannelListener = ucwaEventChannelListener;
                //_ucwaEventChannelListener.OnMessageInvitationReceived += HandleMessageInvitationEventReceived;
                //_ucwaEventChannelListener.OnConversationConnectedEventReceived += HandleConversationConnectedEventRecieved;
                //_ucwaEventChannelListener.OnIncomingConversationEventReceived += HandleIncomingConversationEventReceived;

                _ucwaMessagingSessionState = UcwaMessagingSessionState.Closed;
            }
            catch (Exception ex) { }
        }
        
        public async void startMessaging(string recipientSipUri, string messageSubject, string messageText, string importance = null, string threadId = null)
        {
            try
            {
                //_ucwaMessagingSessionState = UcwaMessagingSessionState.Starting;

                //_recipientSipUri = recipientSipUri;
                //_messageSubject = messageSubject;
                //_startMessageText = messageText;
                //_importance = importance;
                //_threadId = threadId;

                //dynamic applicationsResourceObject = JObject.Parse(_applicationsResource);
                //string startMessagingUri = _applicationRootUri + applicationsResourceObject._embedded.communication._links.startMessaging.href;

                //Dictionary<string, string> jsonData = new Dictionary<string, string>();
                //jsonData.Add("operationId", _operationId);
                //jsonData.Add("to", _recipientSipUri);
                //jsonData.Add("subject", _messageSubject);                
                //if (_threadId != null)
                //    jsonData.Add("threadId", _threadId);
                //if (_importance != null)
                //    jsonData.Add("importance", _importance);
                
                //string jsonPostData = JsonConvert.SerializeObject(jsonData);
                //string postResponse = await HttpHelper.HttpPostAction(_httpClient, startMessagingUri, jsonPostData, _ucwaAuthenticationResult);
            }
            catch (Exception ex) { }
        }
        private void HandleMessageInvitationEventReceived(string messagingInvitationObjectString)
        {
            //THIS IS AN OUTGOING INVITATION EVENT
            try
            {
                dynamic messagingInvitationObject = JObject.Parse(messagingInvitationObjectString);                
                if (_operationId == messagingInvitationObject.operationId.ToString())
                {
                    _ucwaMessagingSessionState = UcwaMessagingSessionState.Inviting;
                    _threadId = messagingInvitationObject.threadId;
                }
            }
            catch (Exception ex) { }
        }

        private async void HandleConversationConnectedEventRecieved(string conversationEventObjectString)
        {
            try
            {
                //dynamic conversationEventOject = JObject.Parse(conversationEventObjectString);
                //if (_threadId == conversationEventOject.threadId.ToString())
                //{
                //    string conversationResourceUri = _applicationRootUri + conversationEventOject._links.self.href;
                //    string conversationResource = await HttpHelper.HttpGetAction(_httpClient, conversationResourceUri, _ucwaAuthenticationResult);
                //    dynamic conversationResourceObject = JObject.Parse(conversationResource);
                //    string messagingResourceUri = _applicationRootUri + conversationResourceObject._links.messaging.href;

                //    string messagingResourceString = await HttpHelper.HttpGetAction(_httpClient, messagingResourceUri, _ucwaAuthenticationResult);

                //    dynamic messagingResourceObject = JObject.Parse(messagingResourceString);
                //    _sendMessageUri = _applicationRootUri + messagingResourceObject._links.sendMessage.href;                    
                //    _setIsTypingUri = _applicationRootUri + messagingResourceObject._links.setIsTyping.href;
                //    _stopMessagingUri = _applicationRootUri + messagingResourceObject._links.stopMessaging.href;
                //    _typingParticipantsUri = _applicationRootUri + messagingResourceObject._links.typingParticipants.href;

                //    //string httpPostRequest = await HttpHelper.HttpPostActionPlainText(_httpClient, _sendMessageUri, _startMessageText, _ucwaAuthenticationResult);
                //    SendMessage(_startMessageText);

                //    _ucwaMessagingSessionState = UcwaMessagingSessionState.Connected;
                //    OnMessageSessionConnected.Invoke(_threadId, _operationId);
                //}
            }
            catch (Exception ex) { }
        }

        private void HandleIncomingConversationEventReceived(string messageText, string opertionId)
        {
            try
            {
                OnIncomingConversationMessageReceived.Invoke(messageText);
            }
            catch (Exception ex) { }
        }

        public async void SendMessage(string messageText)
        {
            //string httpPostRequest = await HttpHelper.HttpPostActionPlainText(_httpClient, _sendMessageUri, messageText, _ucwaAuthenticationResult);
        }
    }
}
