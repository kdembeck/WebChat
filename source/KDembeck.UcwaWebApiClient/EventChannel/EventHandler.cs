using System;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;
using KDembeck.UcwaWebApiClient.Resources;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace KDembeck.UcwaWebApiClient.EventChannel
{
    public class EventHandler : IEventHandler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EventHandler(IHttpUtility httpUtility)
        {
            this.httpUtility = httpUtility;
        }

        private IHttpUtility httpUtility;
        public event EventHandler<EventChannelListenerEventArgs> OnEventReceived;
        public event EventHandler<EventArgs> OnCommunicationUpdated;
        public event EventHandler<UcwaClientEventArgs> OnAudioVideoInvitationStarted;
        public event EventHandler<UcwaClientEventArgs> OnAudioVideoInvitationUpdated;
        public event EventHandler<UcwaClientEventArgs> OnAudioVideoInvitationCompleted;
        public event EventHandler<UcwaConversationEventArgs> OnConversationAdded;
        public event EventHandler<UcwaConversationEventArgs> OnConversationUpdated;
        public event EventHandler<UcwaConversationEventArgs> OnConversationDeleted;
        public event EventHandler<UcwaMessagingInvitationEventArgs> OnMessagingInvitationStarted;
        public event EventHandler<UcwaMessagingInvitationEventArgs> OnMessagingInvitationUpdated;
        public event EventHandler<UcwaMessagingInvitationEventArgs> OnMessagingInvitationCompleted;
        public event EventHandler<UcwaClientEventArgs> OnMissedItemsUpdated;
        public event EventHandler<UcwaOnlineMeetingInvitationEventArgs> OnOnlineMeetingInvitationStarted;
        public event EventHandler<UcwaOnlineMeetingInvitationEventArgs> OnOnlineMeetingInvitationUpdated;
        public event EventHandler<UcwaOnlineMeetingInvitationEventArgs> OnOnlineMeetingInvitationCompleted;
        public event EventHandler<UcwaParticipantInvitationEventArgs> OnParticipantInvitationStarted;
        public event EventHandler<UcwaParticipantInvitationEventArgs> OnParticipantInvitationUpdated;
        public event EventHandler<UcwaParticipantInvitationEventArgs> OnParticipantInvitationCompleted;
        public event EventHandler<UcwaClientEventArgs> OnPhoneAudioInvitationStarted;
        public event EventHandler<UcwaClientEventArgs> OnPhoneAudioInvitationUpdated;
        public event EventHandler<UcwaClientEventArgs> OnPhoneAudioInvitationCompleted;

        //Conversation Events
        public event EventHandler<UcwaClientEventArgs> OnApplicationSharerAdded;
        public event EventHandler<UcwaClientEventArgs> OnApplicationSharerUpdated;
        public event EventHandler<UcwaClientEventArgs> OnApplicationSharerDeleted;
        public event EventHandler<UcwaClientEventArgs> OnApplicationSharingUpdated;
        public event EventHandler<UcwaClientEventArgs> OnAudioVideoUpdated;
        public event EventHandler<UcwaClientEventArgs> OnDataCollaborationUpdated;
        public event EventHandler<UcwaClientEventArgs> OnDataCollaborationSettingsUpdated;
        public event EventHandler<UcwaClientEventArgs> OnLocalParticipantAdded;
        public event EventHandler<UcwaClientEventArgs> OnLocalParticipantUpdated;
        public event EventHandler<UcwaClientEventArgs> OnLocalParticipantDeleted;
        public event EventHandler<UcwaMessageEventArgs> OnMessageStarted;
        public event EventHandler<UcwaMessageEventArgs> OnMessageCompleted;
        public event EventHandler<UcwaMessagingEventArgs> OnMessagingUpdated;
        public event EventHandler<UcwaClientEventArgs> OnOnlineMeetingAdded;
        public event EventHandler<UcwaClientEventArgs> OnOnlineMeetingUpdated;
        //public event EventHandler<UcwaEventArgs> OnOnlineMeetingDeleted;
        public event EventHandler<UcwaParticipantEventArgs> OnParticipantAdded;
        public event EventHandler<UcwaParticipantEventArgs> OnParticipantUpdated;
        public event EventHandler<UcwaParticipantEventArgs> OnParticipantDeleted;
        public event EventHandler<UcwaClientEventArgs> OnParticipantApplicationSharingAdded;
        public event EventHandler<UcwaClientEventArgs> OnParticipantApplicationSharingUpdated;
        public event EventHandler<UcwaClientEventArgs> OnParticipantApplicationSharingDeleted;
        public event EventHandler<UcwaClientEventArgs> OnParticipantAudioAdded;
        public event EventHandler<UcwaClientEventArgs> OnParticipantAudioUpdated;
        public event EventHandler<UcwaClientEventArgs> OnParticipantAudioDeleted;
        public event EventHandler<UcwaClientEventArgs> OnParticipantDataCollaborationAdded;
        public event EventHandler<UcwaClientEventArgs> OnParticipantDataCollaborationUpdated;
        public event EventHandler<UcwaClientEventArgs> OnParticipantDataCollaborationDeleted;
        public event EventHandler<UcwaClientEventArgs> OnParticipantMessagingAdded;
        public event EventHandler<UcwaClientEventArgs> OnParticipantMessagingUpdated;
        public event EventHandler<UcwaClientEventArgs> OnParticipantMessagingDeleted;
        public event EventHandler<UcwaClientEventArgs> OnParticipantPanoramicVideoAdded;
        public event EventHandler<UcwaClientEventArgs> OnParticipantPanoramicVideoUpdated;
        public event EventHandler<UcwaClientEventArgs> OnParticipantPanoramicVideoDeleted;
        public event EventHandler<UcwaClientEventArgs> OnParticipantVideoAdded;
        public event EventHandler<UcwaClientEventArgs> OnParticipantVideoUpdated;
        public event EventHandler<UcwaClientEventArgs> OnParticipantVideoDeleted;
        public event EventHandler<UcwaClientEventArgs> OnPhoneAudioUpdated;
        public event EventHandler<UcwaClientEventArgs> OnTypingParticipantAdded;
        public event EventHandler<UcwaClientEventArgs> OnTypingParticipantDeleted;
        public event EventHandler<UcwaClientEventArgs> OnVideoLockedOnParticipantAdded;
        public event EventHandler<UcwaClientEventArgs> OnVideoLockedOnParticipantUpdated;
        public event EventHandler<UcwaClientEventArgs> OnVideoLockedOnParticipantDeleted;

        //Me Events
        public event EventHandler<EventArgs> OnMeUpdatedEvent;
        public event EventHandler<UcwaClientEventArgs> OnLocationAdded;
        public event EventHandler<UcwaClientEventArgs> OnLocationUpdated;
        public event EventHandler<UcwaClientEventArgs> OnLocationDeleted;
        public event EventHandler<UcwaClientEventArgs> OnNoteAdded;
        public event EventHandler<UcwaClientEventArgs> OnNoteUpdated;
        public event EventHandler<UcwaClientEventArgs> OnNoteDeleted;
        public event EventHandler<UcwaClientEventArgs> OnPresenceAdded;
        public event EventHandler<UcwaClientEventArgs> OnPresenceUpdated;
        public event EventHandler<UcwaClientEventArgs> OnPresenceDeleted;

        //People Events
        public event EventHandler<UcwaClientEventArgs> OnContactAdded;
        public event EventHandler<UcwaClientEventArgs> OnContactDeleted;
        public event EventHandler<UcwaClientEventArgs> OnContactLocationUpdated;
        public event EventHandler<UcwaClientEventArgs> OnContactNoteUpdated;
        public event EventHandler<UcwaContactPresenceEventArgs> OnContactPresenceUpdated;
        public event EventHandler<UcwaClientEventArgs> OnContactPrivacyRelationshipUpdated;
        public event EventHandler<UcwaClientEventArgs> OnContactSupportedModalitiesUpdated;
        public event EventHandler<UcwaClientEventArgs> OnDefaultGroupAdded;
        public event EventHandler<UcwaClientEventArgs> OnDefaultGroupUpdated;
        public event EventHandler<UcwaClientEventArgs> OnDefaultGroupDeleted;
        public event EventHandler<UcwaClientEventArgs> OnDelegatorsGroupAdded;
        public event EventHandler<UcwaClientEventArgs> OnDelegatorsGroupUpdated;
        public event EventHandler<UcwaClientEventArgs> OnDelegatorsGroupDeleted;
        public event EventHandler<UcwaClientEventArgs> OnDistributionGroupAdded;
        public event EventHandler<UcwaClientEventArgs> OnDistributionGroupUpdated;
        public event EventHandler<UcwaClientEventArgs> OnDistributionGroupDeleted;
        public event EventHandler<UcwaClientEventArgs> OnGroupAdded;
        public event EventHandler<UcwaClientEventArgs> OnGroupUpdated;
        public event EventHandler<UcwaClientEventArgs> OnGroupDeleted;
        public event EventHandler<UcwaClientEventArgs> OnMyContactsAndGroupsSubscriptionUpdated;
        public event EventHandler<UcwaClientEventArgs> OnMyOrganizationGroupAdded;
        public event EventHandler<UcwaClientEventArgs> OnMyOrganizationGroupUpdated;
        public event EventHandler<UcwaClientEventArgs> OnMyOrganizationGroupDeleted;
        public event EventHandler<UcwaClientEventArgs> OnPinnedGroupAdded;
        public event EventHandler<UcwaClientEventArgs> OnPinnedGroupUpdated;
        public event EventHandler<UcwaClientEventArgs> OnPinnedGroupDeleted;
        public event EventHandler<UcwaPresenceSubscriptionEventArgs> OnPresenceSubscriptionAdded;
        public event EventHandler<UcwaPresenceSubscriptionEventArgs> OnPresenceSubscriptionUpdated;
        public event EventHandler<UcwaPresenceSubscriptionEventArgs> OnPresenceSubscriptionDeleted;

        //Policies Events
        public event EventHandler<UcwaClientEventArgs> OnPoliciesUpdated;

        public async void Handle_OnEventChannelListenerEventReceived(object sender, EventChannelListenerEventArgs eventChannelListenerArgs)
        {
            OnEventReceived?.Invoke(this, eventChannelListenerArgs);

            dynamic eventObject = eventChannelListenerArgs.eventResource;

            string eventSender = eventChannelListenerArgs.sender.rel;
            string eventRel = eventObject.link.rel;
            string eventType = eventObject.type;

            log.Debug("UCWA Event received. sender=" + eventSender + "; rel=" + eventRel + "; type=" + eventType + "; embedded=" + JsonConvert.SerializeObject(eventObject._embedded));

            JObject jObject;

            switch (eventSender)
            {
                case "communication":
                    #region communication events
                    switch (eventRel)
                    {
                        case "audioVideoInvitation":
                            switch (eventType)
                            {
                                case "completed":
                                    OnAudioVideoInvitationCompleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "started":
                                    OnAudioVideoInvitationStarted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnAudioVideoInvitationUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "communication":
                            switch (eventType)
                            {
                                case "updated":
                                    OnCommunicationUpdated?.Invoke(this, new EventArgs());
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "conversation":
                            IConversationResource conversationResource;
                            string href = eventObject.link.href;
                            switch (eventType)
                            {   
                                case "added":                                    
                                    jObject = eventObject._embedded.conversation;
                                    conversationResource = jObject.ToObject<ConversationResource>();                                    
                                    conversationResource.httpUtility = httpUtility;
                                    OnConversationAdded?.Invoke(this, new UcwaConversationEventArgs(conversationResource, href));
                                    break;
                                case "updated":
                                    jObject = eventObject._embedded.conversation;
                                    conversationResource = jObject.ToObject<ConversationResource>();
                                    conversationResource.httpUtility = httpUtility;
                                    OnConversationUpdated?.Invoke(this, new UcwaConversationEventArgs(conversationResource, href));
                                    break;
                                case "deleted":
                                    OnConversationDeleted?.Invoke(this, new UcwaConversationEventArgs(null, href));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "messagingInvitation":
                            IMessagingInvitationResource messagingInvitationResource;
                            jObject = eventObject._embedded.messagingInvitation;
                            messagingInvitationResource = jObject.ToObject<MessagingInvitationResource>();
                            messagingInvitationResource.httpUtility = httpUtility;
                            switch (eventType)
                            {
                                case "started":                                    
                                    OnMessagingInvitationStarted?.Invoke(this, new UcwaMessagingInvitationEventArgs(messagingInvitationResource));
                                    break;
                                case "completed":
                                    OnMessagingInvitationCompleted?.Invoke(this, new UcwaMessagingInvitationEventArgs(messagingInvitationResource));
                                    break;
                                case "updated":
                                    OnMessagingInvitationUpdated?.Invoke(this, new UcwaMessagingInvitationEventArgs(messagingInvitationResource));
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case "missedItems":
                            switch (eventType)
                            {
                                case "updated":
                                    OnMissedItemsUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded.missedItems));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "onlineMeetingInvitation":
                            IOnlineMeetingInvitationResource onlineMeetingInvitation;
                            switch (eventType)
                            {
                                case "started":
                                    jObject = eventObject._embedded.onlineMeetingInvitation;
                                    onlineMeetingInvitation = jObject.ToObject<OnlineMeetingInvitationResource>();
                                    onlineMeetingInvitation.httpUtility = httpUtility;
                                    OnOnlineMeetingInvitationStarted?.Invoke(this, new UcwaOnlineMeetingInvitationEventArgs(onlineMeetingInvitation));
                                    break;
                                case "completed":
                                    jObject = eventObject._embedded.onlineMeetingInvitation;
                                    onlineMeetingInvitation = jObject.ToObject<OnlineMeetingInvitationResource>();
                                    onlineMeetingInvitation.httpUtility = httpUtility;
                                    OnOnlineMeetingInvitationCompleted?.Invoke(this, new UcwaOnlineMeetingInvitationEventArgs(onlineMeetingInvitation));
                                    break;
                                case "updated":
                                    jObject = eventObject._embedded.onlineMeetingInvitation;
                                    onlineMeetingInvitation = jObject.ToObject<OnlineMeetingInvitationResource>();
                                    onlineMeetingInvitation.httpUtility = httpUtility;
                                    OnOnlineMeetingInvitationUpdated?.Invoke(this, new UcwaOnlineMeetingInvitationEventArgs(onlineMeetingInvitation));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "participantInvitation":
                            IParticipantInvitationResource participantInvitationResource;
                            jObject = eventObject._embedded.participantInvitation;
                            participantInvitationResource = jObject.ToObject<ParticipantInvitationResource>();
                            participantInvitationResource.httpUtility = httpUtility;
                            switch (eventType)
                            {   
                                case "completed":                                    
                                    OnParticipantInvitationCompleted?.Invoke(this, new UcwaParticipantInvitationEventArgs(participantInvitationResource));
                                    break;
                                case "started":
                                    OnParticipantInvitationStarted?.Invoke(this, new UcwaParticipantInvitationEventArgs(participantInvitationResource));
                                    break;
                                case "updated":
                                    OnParticipantInvitationUpdated?.Invoke(this, new UcwaParticipantInvitationEventArgs(participantInvitationResource));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "phoneAudioInvitation":
                            switch (eventType)
                            {
                                case "completed":
                                    OnPhoneAudioInvitationCompleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "started":
                                    OnPhoneAudioInvitationStarted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnPhoneAudioInvitationStarted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
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
                        case "applicationSharer":                            
                            switch (eventType)
                            {
                                case "added":
                                    OnApplicationSharerAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnApplicationSharerUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnApplicationSharerDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded)); 
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case "applicationSharing":
                            switch (eventType)
                            {
                                case "updated":
                                    OnApplicationSharingUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "audioVideo":
                            switch (eventType)
                            {
                                case "updated":
                                    OnAudioVideoUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "dataCollaboration":
                            switch (eventType)
                            {
                                case "updated":
                                    OnDataCollaborationUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "dataCollaborationSettings":
                            switch (eventType)
                            {
                                case "updated":
                                    OnDataCollaborationSettingsUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "localParticipant":
                            switch (eventType)
                            {
                                case "added":
                                    OnLocalParticipantAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnLocalParticipantDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnLocalParticipantUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "message":
                            IMessageResource messageResource;
                            jObject = eventObject._embedded.message;
                            messageResource = jObject.ToObject<MessageResource>();
                            messageResource.httpUtility = httpUtility;
                            switch (eventType)
                            {
                                case "completed":                                                                        
                                    OnMessageCompleted?.Invoke(this, new UcwaMessageEventArgs(messageResource));
                                    break;
                                case "started":
                                    OnMessageStarted?.Invoke(this, new UcwaMessageEventArgs(messageResource));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "messaging":
                            IMessagingResource messagingResource;
                            jObject = eventObject._embedded.messaging;
                            messagingResource = jObject.ToObject<MessagingResource>();
                            messagingResource.httpUtility = httpUtility;
                            switch (eventType)
                            {
                                case "updated":                                    
                                    OnMessagingUpdated?.Invoke(this, new UcwaMessagingEventArgs(messagingResource));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "onlineMeeting":
                            switch (eventType)
                            {
                                case "added":
                                    OnOnlineMeetingAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnOnlineMeetingUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                //case "deleted":
                                //    break;
                                default:
                                    break;
                            }
                            break;
                        case "participant":
                            string contextId = eventObject.context;
                            string participantLinkHref = eventObject.link.href;
                            IParticipantResource participantResource = new ParticipantResource(httpUtility);
                            await participantResource.Get(httpUtility.baseUrl + participantLinkHref);
                            //ConversationResource conversationResource = await participantResource.getConversation();                     
                            switch (eventType)
                            {
                                case "added":
                                    //this triggers off of typing participants event
                                    if (eventObject.@in != null)// == "typingParticipants")
                                    {
                                        string participantAddedRel = eventObject.@in.rel;
                                        switch (participantAddedRel)
                                        {
                                            case "typingParticipants":
                                                
                                                break;
                                            case "leaders":
                                                break;
                                            default:
                                                break;  
                                        }
                                    }
                                    else
                                    {
                                        OnParticipantAdded?.Invoke(this, new UcwaParticipantEventArgs(participantResource, contextId, participantLinkHref));
                                    }
                                    break;
                                case "updated":
                                    OnParticipantUpdated?.Invoke(this, new UcwaParticipantEventArgs(participantResource, contextId, participantLinkHref));
                                    break;
                                case "deleted":
                                    //this triggers off of typing participants event         
                                    if (eventObject.@in != null)// == "typingParticipants")
                                    {
                                        string participantAddedRel = eventObject.@in.rel;
                                        switch (participantAddedRel)
                                        {
                                            case "typingParticipants":
                                                break;
                                            case "leaders":
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        //{{
                                        //  "link": {
                                        //    "rel": "participant",
                                        //    "href": "/ucwa/oauth/v1/applications/103265589125/communication/conversations/340c6e88-85e6-45ca-8ac3-46e32af7cf3d/participants/usera@webchat5.onmicrosoft.com",
                                        //    "title": "User A"
                                        //  },                                          
                                        //  "type": "deleted"
                                        //}}
                                            
                                        OnParticipantDeleted?.Invoke(this, new UcwaParticipantEventArgs(participantResource, contextId, participantLinkHref));
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "participantApplicationSharing":
                            switch (eventType)
                            {
                                case "added":
                                    OnParticipantApplicationSharingAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnParticipantApplicationSharingDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnParticipantApplicationSharingUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "participantAudio":
                            switch (eventType)
                            {
                                case "added":
                                    OnParticipantAudioAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnParticipantAudioDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnParticipantAudioUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "participantDataCollaboration":
                            switch (eventType)
                            {
                                case "added":
                                    OnParticipantDataCollaborationAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnParticipantDataCollaborationDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnParticipantDataCollaborationUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "participantMessaging":
                            switch (eventType)
                            {
                                case "added":
                                    OnParticipantMessagingAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnParticipantMessagingDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnParticipantMessagingUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "participantPanoramicVideo":
                            switch (eventType)
                            {
                                case "added":
                                    OnParticipantPanoramicVideoAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnParticipantPanoramicVideoDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnParticipantPanoramicVideoUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "participantVideo":
                            switch (eventType)
                            {
                                case "added":
                                    OnParticipantVideoAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnParticipantVideoDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnParticipantVideoUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "phoneAudio":
                            switch (eventType)
                            {
                                case "updated":
                                    OnPhoneAudioUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "videoLockedOnParticipant":
                            switch (eventType)
                            {
                                case "added":
                                    OnVideoLockedOnParticipantAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnVideoLockedOnParticipantUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnVideoLockedOnParticipantDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
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
                case "me":
                    #region me events
                    switch (eventRel)
                    {
                        case "location":
                            switch (eventType)
                            {
                                case "added":
                                    OnLocationAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnLocationDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnLocationUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "me":
                            switch (eventType)
                            {
                                case "updated":
                                    OnMeUpdatedEvent?.Invoke(this, new EventArgs());
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "note":
                            switch (eventType)
                            {
                                case "added":
                                    OnNoteAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnNoteDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnNoteUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "presence":
                            switch (eventType)
                            {
                                case "added":
                                    OnPresenceAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnPresenceDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnPresenceUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
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
                case "people":
                    #region people events
                    switch (eventRel)
                    {
                        case "contact":
                            switch (eventType)
                            {
                                case "added":
                                    OnContactAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnContactDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                //case "updated":
                                //    break;
                                default:
                                    break;
                            }
                            break;
                        case "contactLocation":
                            switch (eventType)
                            {
                                case "updated":
                                    OnContactLocationUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "contactNote":
                            switch (eventType)
                            {
                                case "updated":
                                    OnContactNoteUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "contactPresence":
                            IContactResource contactResource = new ContactResource(httpUtility);
                            await contactResource.Get(httpUtility.baseUrl + eventObject.@in.href);
                            IContactPresenceResource contactPresenceResource = new ContactPresenceResource(httpUtility);
                            await contactPresenceResource.Get(httpUtility.baseUrl + eventObject.link.href);
                            switch (eventType)
                            {
                                case "updated":
                                    OnContactPresenceUpdated?.Invoke(this, new UcwaContactPresenceEventArgs(contactResource, contactPresenceResource));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "contactPrivacyRelationship":
                            switch (eventType)
                            {
                                case "updated":
                                    OnContactPrivacyRelationshipUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "contactSupportedModalities":
                            switch (eventType)
                            {
                                case "updated":
                                    OnContactSupportedModalitiesUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "defaultGroup":
                            switch (eventType)
                            {
                                case "added":
                                    OnDefaultGroupAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnDefaultGroupDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnDefaultGroupUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "delegatorsGroup":
                            switch (eventType)
                            {
                                case "added":
                                    OnDelegatorsGroupAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnDelegatorsGroupDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnDelegatorsGroupUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "distributionGroup":
                            switch (eventType)
                            {
                                case "added":
                                    OnDistributionGroupAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnDistributionGroupDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnDistributionGroupUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "group":
                            switch (eventType)
                            {
                                case "added":
                                    OnGroupAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnGroupDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnGroupDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "myContactsAndGroupsSubscription":
                            switch (eventType)
                            {
                                case "updated":
                                    OnMyContactsAndGroupsSubscriptionUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "myOrganizationGroup":
                            switch (eventType)
                            {
                                case "added":
                                    OnMyOrganizationGroupAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnMyOrganizationGroupDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnMyOrganizationGroupUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "pinnedGroup":
                            switch (eventType)
                            {
                                case "added":
                                    OnPinnedGroupAdded?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "deleted":
                                    OnPinnedGroupDeleted?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                case "updated":
                                    OnPinnedGroupUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "presenceSubscription":
                            IPresenceSubscriptionResource presenceSubscriptionResource = new PresenceSubscriptionResource(httpUtility);
                            await presenceSubscriptionResource.Get(httpUtility.baseUrl + eventObject.link.href);
                            switch (eventType)
                            {
                                case "added":
                                    OnPresenceSubscriptionAdded?.Invoke(this, new UcwaPresenceSubscriptionEventArgs(presenceSubscriptionResource));
                                    break;
                                case "deleted":
                                    OnPresenceSubscriptionDeleted?.Invoke(this, new UcwaPresenceSubscriptionEventArgs(presenceSubscriptionResource));
                                    break;
                                case "updated":
                                    OnPresenceSubscriptionUpdated?.Invoke(this, new UcwaPresenceSubscriptionEventArgs(presenceSubscriptionResource));
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
                case "policies":
                    #region policies events
                    switch (eventRel)
                    {
                        case "policies":
                            switch (eventType)
                            {
                                case "updated":
                                    OnPoliciesUpdated?.Invoke(this, new UcwaClientEventArgs(httpUtility, eventRel, eventObject._embedded));
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
            }
        }
    }
}
