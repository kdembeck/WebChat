using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public delegate void OnMessagingInvitationStarted(string eventObjectString);
    public delegate void OnMessagingInvitationCompleted(string eventObjectString);
    public delegate void OnIncomingMessageReceived(string eventObjectString);
    public delegate void OnConversationDeleted(string eventObjectString);
    public delegate void OnOnlineMeetingInvitationCompleted(string eventObjectString);
    public delegate void OnCommunicationConversationUpdated(string eventObjectString);

    public class EventChannelEventHandler
    {
        public OnMessagingInvitationStarted OnMessagingInvitationStartedEvent;
        public OnMessagingInvitationCompleted OnMessagingInvitationCompletedEvent;
        public OnIncomingMessageReceived OnIncomingMessageReceivedEvent;
        public OnConversationDeleted OnConversatedDeletedEvent;
        public OnOnlineMeetingInvitationCompleted OnOnlineMeetingInvitationCompletedEvent;
        public OnCommunicationConversationUpdated OnCommunicationConversationUpdatedEvent;

        private HttpHelper httpHelper;
        private ApplicationResource _applicationResource;
        
        public EventChannelEventHandler(ApplicationResource applicationResource, HttpHelper HttpHelper)
        {
            _applicationResource = applicationResource;
            httpHelper = new HttpHelper();
            httpHelper.ApplicationRootUri = HttpHelper.ApplicationRootUri;
            httpHelper.AuthenticationResult = HttpHelper.AuthenticationResult;
        }
        public void Handle_OnEventChannelListenerEventReceivedEvent(string sender, string eventObjectString)
        {
            try
            {
                dynamic eventObject = JObject.Parse(eventObjectString);

                string eventRel = eventObject.link.rel;
                string eventType = eventObject.type;

                switch (sender)
                {
                    case "me":
                        #region me events
                        switch (eventRel)
                        {
                            case "me":
                                switch (eventType)
                                {
                                    case "updated":
                                        HandleEvent_OnMeUpdated(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "presence":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnMePresenceAdded(eventObjectString);
                                        break;                                   
                                    default:
                                        break;
                                }
                                break;
                            case "location":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnMeLocationAdded(eventObjectString);
                                        break;                                   
                                    default:
                                        break;
                                }
                                break;
                            case "note":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnMeNoteAdded(eventObjectString);
                                        break;                                   
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        #endregion
                        break;
                    case "communication":
                        #region communication events
                        switch (eventRel)
                        {
                            case "communication":
                                switch (eventType)
                                {
                                    case "updated":
                                        HandleEvent_OnCommunicationUpdated(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "messagingInvitation":
                                switch (eventType)
                                {
                                    case "started":
                                        HandleEvent_OnMessagingInvitationStarted(JsonConvert.SerializeObject(eventObject._embedded.messagingInvitation));
                                        break;
                                    case "completed":
                                        HandleEvent_OnMessagingInvitationCompleted(JsonConvert.SerializeObject(eventObject._embedded.messagingInvitation));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "conversation":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnCommunicationConversationAdded(eventObjectString);
                                        break;
                                    case "updated":
                                        HandleEvent_OnCommunicationConversationUpdated(JsonConvert.SerializeObject(eventObject._embedded.conversation));
                                        break;
                                    case "deleted":
                                        HandleEvent_OnCommunicationConversationDeleted(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "onlineMeetingInvitation":
                                switch (eventType)
                                {
                                    case "completed":
                                        HandleEvent_OnOnlineMeetingInvitationCompleted(JsonConvert.SerializeObject(eventObject._embedded.onlineMeetingInvitation));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        #endregion
                        break;
                    case "conversation":
                        #region conversation events
                        switch (eventRel)
                        {
                            case "message":
                                switch (eventType)
                                {
                                    case "completed":
                                        //check for incoming message here
                                        //check for _embedded.message.direction "outgoing" or "incoming"
                                        HandleEvent_OnMessageCompleted(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "messaging":
                                switch (eventType)
                                {
                                    case "updated":
                                        HandleEvent_OnConversationMessagingUpdated(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "localParticipant":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnConversationLocalParticipantAdded(eventObjectString);
                                        break;                                    
                                    case "deleted":
                                        HandleEvent_OnConversationLocalParticipantDeleted(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "participant":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnConversationParticipantAdded(eventObjectString);
                                        break;
                                    case "updated":
                                        break;
                                    case "deleted":
                                        HandleEvent_OnConversationParticipantDeleted(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "participantMessaging":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnConversationParticipantMessagingAdded(eventObjectString);
                                        break;
                                    case "updated":
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "audioVideo":
                                switch (eventType)
                                {   
                                    case "updated":
                                        HandleEvent_OnConversationAudioVideoUpdated(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "applicationSharing":
                                switch (eventType)
                                {   
                                    case "updated":
                                        HandleEvent_OnConversationApplicationSharingUpdated(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "dataCollaboration":
                                switch (eventType)
                                {
                                    case "updated":
                                        HandleEvent_OnConversationDataCollaborationUpdated(eventObjectString);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "onlineMeeting":
                                switch (eventType)
                                {
                                    case "added":
                                        HandleEvent_OnOnlineMeetingAdded(eventObjectString);
                                        break;                                    
                                    default:
                                        break;
                                }
                                break;
                            
                            default:
                                break;
                        }
                        #endregion
                        break;       
                    default:
                        break;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        #region conversation event handlers
        private void HandleEvent_OnConversationMessagingUpdated(string eventObjectString) { }
        private void HandleEvent_OnConversationLocalParticipantAdded(string eventObjectString) { }
        private void HandleEvent_OnConversationLocalParticipantDeleted(string eventObjectString) { }
        private void HandleEvent_OnConversationParticipantAdded(string eventObjectString) { }
        private void HandleEvent_OnConversationParticipantDeleted(string eventObjectString) { }
        private void HandleEvent_OnConversationParticipantMessagingAdded(string eventObjectString) { }
        private void HandleEvent_OnConversationAudioVideoUpdated(string eventObjectString) { }
        private void HandleEvent_OnConversationApplicationSharingUpdated(string eventObjectString) { }
        private void HandleEvent_OnConversationDataCollaborationUpdated(string eventObjectString){ }
        #endregion
        #region communication event handlers
        private void HandleEvent_OnCommunicationUpdated(string eventObjectString)
        {
            try
            {
                dynamic eventObject = Newtonsoft.Json.Linq.JObject.Parse(eventObjectString);
                //string communicationResourceUri = eventObject._embedded.communication._links.self.href;
                string communicationResourceString = JsonConvert.SerializeObject(eventObject._embedded.communication);
                _applicationResource._embedded.communication.FillResourceValues(communicationResourceString);
            }
            catch (Exception ex) { throw ex; }
        }
        private void HandleEvent_OnMessagingInvitationStarted(string eventObjectString)
        {
            //_conversationManager.Handle_OutgoingInvitationStartedEvent(eventObjectString);

            OnMessagingInvitationStartedEvent.Invoke(eventObjectString);
        }
        private void HandleEvent_OnMessagingInvitationCompleted(string eventObjectString)
        {
            try
            {
                //handle adding our new conversation session here since we don't always get the conversation added/updated events
                OnMessagingInvitationCompletedEvent.Invoke(eventObjectString);
                
            }
            catch (Exception ex) { throw ex; }
        }
        private void HandleEvent_OnCommunicationConversationAdded(string eventObjectString) { }
        private void HandleEvent_OnCommunicationConversationUpdated(string eventObjectString)
        {
            OnCommunicationConversationUpdatedEvent.Invoke(eventObjectString);
        }        
        private void HandleEvent_OnCommunicationConversationDeleted(string eventObjectString) {
            OnConversatedDeletedEvent.Invoke(eventObjectString);
        }
        #endregion
        #region me event handlers
        private async void HandleEvent_OnMeUpdated(string eventObjectString)
        {
            try
            {
                //replace this with just an event to be raised. we can then get rid of the application resource and http helper from this class
                await _applicationResource._embedded.me.GetResource(_applicationResource._embedded.me._links.self.href, httpHelper);
            }
            catch (Exception ex) { throw ex; }
        }        
        private void HandleEvent_OnMePresenceAdded(string eventObjectString) { }
        private void HandleEvent_OnMeLocationAdded(string eventObjectString) { }
        private void HandleEvent_OnMeNoteAdded(string eventObjectString) { }
        private void HandleEvent_OnMessageCompleted(string eventObjectString)
        {
            dynamic eventObject = JObject.Parse(eventObjectString);

            if (eventObject._embedded.message.direction == "Incoming")
                OnIncomingMessageReceivedEvent.Invoke(eventObjectString);

        }
        #endregion
        private void HandleEvent_OnOnlineMeetingAdded(string eventObjectString)
        {
            //don't do anything. operation id doesn't come back here so we don't know which meeting this is
        }
        private void HandleEvent_OnOnlineMeetingInvitationCompleted(string eventObjectString)
        {
            //operation id and thread id come back here. use this one. 

            //raise an event and in that handler we'll send out the invite to the agent
            if (OnOnlineMeetingInvitationCompletedEvent != null)
                OnOnlineMeetingInvitationCompletedEvent.Invoke(eventObjectString);

            //eventObject._embedded.onlineMeetingInvitation fill and send the class back
        }
    }
}
