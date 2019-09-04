using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;
using System.Web;

namespace UcwaTools.AppManagers
{    
    public delegate void OnIncomingMessageReceived(string operationId, string Message); 
    public delegate void OnStartMessagingCompleted(string operationId, string threadId, string conversationId); 
    public delegate void OnConversatedDeleted(string threadId);
    public delegate void OnStartOnlineMeetingCompleted(string operationId, string threadId, string conversationId);

    public class CommunicationManager
    {

        public List<ConversationManager> conversations = new List<ConversationManager>();

        #region private properties
        private HttpHelper httpHelper;
        private CommunicationResource communicationResource;        
        //private List<OnlineMeetingInvitation> onlineMeetingInvitations = new List<OnlineMeetingInvitation>();
        private List<MessagingInvitationResource> messagingInvitations = new List<MessagingInvitationResource>();
        #endregion

        #region Public delegates
        public OnIncomingMessageReceived OnIncomingMessageReceivedEvent;
        public OnStartMessagingCompleted OnMessagingInvitationCompletedEvent;
        public OnConversatedDeleted OnConversationDeletedEvent;
        public OnStartOnlineMeetingCompleted OnStartOnlineMeetingCompletedEvent;
        #endregion

        internal CommunicationManager(HttpHelper HttpHelper)
        {
            httpHelper = HttpHelper;
            //communicationResource = CommunicationResource;                     
        }

        public async Task Update(string communicationResourceUri)
        {
            if (communicationResource == null)
                communicationResource = new CommunicationResource();

            await communicationResource.GetResource(communicationResourceUri, httpHelper);
        }

        #region IM conversation methods
        public async Task<string> startMessaging(string to, string subject, string importance, string message, string operationId = "", string threadId = "")
        {
            if (operationId == "")
                operationId = Guid.NewGuid().ToString();

            string startMessagingJson = JsonConvert.SerializeObject(new
            {
                to = to,
                subject = subject,
                importance = importance,
                operationId = operationId,
                threadId = threadId
            }); 
            
            await httpHelper.HttpPostAction(httpHelper.ApplicationRootUri + communicationResource._links.startMessaging.href, startMessagingJson);

            MessagingInvitationResource messagingInvitation = new MessagingInvitationResource();
            messagingInvitation.operationId = operationId;
            messagingInvitation.to = to;
            messagingInvitation.subject = subject;
            messagingInvitation.importance = importance;
            messagingInvitation.message = message;

            messagingInvitations.Add(messagingInvitation);

            return operationId;




            //ManagedConversation managedConversation = new ManagedConversation();
            //if (communicationController == null)
            //    communicationController = new CommunicationController(httpHelper);
                        
            //managedConversation.InvitationOperationId = await communicationController.StartMessaging(startMessagingHref, recipientSipUri, messageSubject, importance, message);
            //managedConversation.StartMessageText = message;
            
            //conversations.Add(managedConversation);

            //return managedConversation.InvitationOperationId;

            //Now wait for the invitation completed event to begin sending and receiving messages
        }

        //internal void Handle_OnMessagingInvitationStartedEvent(string eventObjectString)
        //{            

        //    MessagingInvitationResource messagingInvitationStarted = new MessagingInvitationResource();
        //    JsonConvert.PopulateObject(eventObjectString, messagingInvitationStarted);

        //    MessagingInvitationResource messagingInvitationSaved = messagingInvitations.Where(x => x.operationId == messagingInvitationStarted.operationId).FirstOrDefault();

        //    messagingInvitationSaved._links = messagingInvitationStarted._links;
        //    messagingInvitationSaved.threadId = messagingInvitationStarted.threadId;
        //    messagingInvitationSaved.state = messagingInvitationStarted.state;
        //}

        //internal async void Handle_OnMessagingInvitationCompletedEvent(string eventObjectString)
        //{
        //    MessagingInvitationResource messagingInvitationCompleted = new MessagingInvitationResource();
        //    JsonConvert.PopulateObject(eventObjectString, messagingInvitationCompleted);

        //    MessagingInvitationResource messagingInvitationSaved = messagingInvitations.Where(x => x.operationId == messagingInvitationCompleted.operationId).FirstOrDefault();

        //    ConversationResource conversationResource = new ConversationResource();            

        //    await conversationResource.GetResource(messagingInvitationCompleted._links.conversation.href, httpHelper);
        //    string conversationId = ParseConversationIdFromMessagingUri(conversationResource._links.messaging.href);
        //    ConversationManager conversationManager = new ConversationManager(conversationResource, httpHelper);
        //    conversationManager.conversationId = conversationId;
        //    await conversationManager.Initialize();

        //    conversations.Add(conversationManager);

        //    await conversationManager.Messaging.sendMessage(messagingInvitationSaved.message);

        //    messagingInvitations.RemoveAll(x => x.operationId == messagingInvitationCompleted.operationId);
            
        //    OnMessagingInvitationCompletedEvent.Invoke(messagingInvitationCompleted.operationId, messagingInvitationCompleted.threadId, conversationId);
        //}

        //internal void Handle_OnIncomingMessageReceivedEvent(string eventObjectString)
        //{
        //    try
        //    {
        //        dynamic eventObject = JObject.Parse(eventObjectString);
        //        string conversationId = ParseConversationIdFromMessagingUri((string)eventObject._embedded.message._links.messaging.href); 
                            
        //        string messageString = (string)eventObject._embedded.message._links.plainMessage.href;
        //        messageString = messageString.Replace("data:text/plain;charset=utf-8,", "");
        //        messageString = HttpUtility.UrlDecode(messageString);      
                         
        //        if (OnIncomingMessageReceivedEvent != null)
        //            OnIncomingMessageReceivedEvent.Invoke(conversationId, messageString);
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        #endregion

        #region Online Meeting methods

        public async Task<string> startOnlineMeeting(string messageSubject, string messageImportance, string messageText, string invitationOperationId = "", string messageThreadId = "")
        {
            //get a client id so we know who to notify when completion occurs

            if (invitationOperationId == "")
                invitationOperationId = Guid.NewGuid().ToString();

            string startOnlineMeetingJson = JsonConvert.SerializeObject(new
            {
                operationId = invitationOperationId,
                importance = messageImportance,
                subject = messageSubject,
                threadId = messageThreadId
            });

            await httpHelper.HttpPostAction(httpHelper.ApplicationRootUri + communicationResource._links.startOnlineMeeting.href, startOnlineMeetingJson);

            return invitationOperationId;
        }

        public async Task joinOnlineMeeting(string onlineMeetingUri="", string operationId="", string importance="", string subject="", string threadId="")
        {
            //communication.joineonlinemeeting
            string joinOnlineMeetingJson = JsonConvert.SerializeObject(new
            {
                onlineMeetingUri = onlineMeetingUri,
                operationId = operationId,
                importance = importance,
                subject = subject
                //threadId = threadId
            });

            await httpHelper.HttpPostAction(httpHelper.ApplicationRootUri + communicationResource._links.joinOnlineMeeting.href + "?onlineMeetingUri=" + onlineMeetingUri, joinOnlineMeetingJson);
        }

        //internal async void Handle_OnOnlineMeetingInvitationCompletedEvent(string eventObjectString)
        //{
        //    //get the conversation resource adn send out the inviet to the agent
        //    UcwaTools.OnlineMeetingInvitation invitation = new UcwaTools.OnlineMeetingInvitation();
        //    JsonConvert.PopulateObject(eventObjectString, invitation);

        //    ConversationResource conversationResource = new ConversationResource();

        //    await conversationResource.GetResource(invitation._links.conversation.href, httpHelper);
        //    string conversationId = ParseConversationIdFromMessagingUri(conversationResource._links.messaging.href);
        //    ConversationManager conversationManager = new ConversationManager(conversationResource, httpHelper);
        //    conversationManager.conversationId = conversationId;
        //    await conversationManager.Initialize();
        //    conversations.Add(conversationManager);

        //    if (OnStartOnlineMeetingCompletedEvent != null)
        //        OnStartOnlineMeetingCompletedEvent.Invoke(invitation.operationId, invitation.threadId, conversationId);

        //    //DELETE THIS
        //    await conversationManager.addParticipant("sip:kdembeck@webchat4.onmicrosoft.com");
        //    await conversationManager.Messaging.addMessaging("test1");
        //    //gotta reload the messaging resource before we can call send message
        //    //await conversationManager.Messaging.sendMessage("test2");
        //    //DELETE THIS
        //}

        //public async Task inviteContactToOnlineMeeting(string participantSipUri, string conversationId)
        //{
        //    //call the addParticipant resource on the conversation resource and send the contact sip uri and operationId
        //    //wait for invitation complete event on the sent operationId
        //    //call conversation.addParticipant
        //}

        #endregion

        //internal void Handle_OnConversationDeletedEvent(string eventObjectString)
        //{
            //find the conversationId in the event object and then raise the event back up to the consuming app so that the web client can react accordingly
        //}

        //internal void Handle_OnCommunicationConversationUpdatedEvent(string eventObjectString)
        //{
        //    ConversationResource conversationResource = new ConversationResource();
        //    JsonConvert.PopulateObject(eventObjectString, conversationResource);

        //    string threadId = conversationResource.threadId;
        //    string conversationId = ParseConversationIdFromMessagingUri(conversationResource._links.messaging.href);

        //    ConversationManager managedConversation = conversations.Where(x => x.conversationId == conversationId).FirstOrDefault();

        //    if (managedConversation != null)
        //    {
        //        managedConversation.Update(conversationResource._links.self.href);
        //    }
        //}
        
        private string ParseConversationIdFromMessagingUri(string MessagingUri)
        {
            string[] strArray = MessagingUri.Split('/');
            return strArray[strArray.Count() - 2];
        }
    }
}
