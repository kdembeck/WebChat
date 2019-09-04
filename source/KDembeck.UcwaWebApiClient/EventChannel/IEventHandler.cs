using System;
using KDembeck.UcwaWebApiClient.Utilities;
using KDembeck.UcwaWebApiClient.Resources;

namespace KDembeck.UcwaWebApiClient.EventChannel
{
    public interface IEventHandler
    {
        void Handle_OnEventChannelListenerEventReceived(object sender, EventChannelListenerEventArgs eventChannelListenerArgs);

        event EventHandler<EventChannelListenerEventArgs> OnEventReceived;
        event EventHandler<EventArgs> OnCommunicationUpdated;
        event EventHandler<UcwaClientEventArgs> OnAudioVideoInvitationStarted;
        event EventHandler<UcwaClientEventArgs> OnAudioVideoInvitationUpdated;
        event EventHandler<UcwaClientEventArgs> OnAudioVideoInvitationCompleted;
        event EventHandler<UcwaConversationEventArgs> OnConversationAdded;
        event EventHandler<UcwaConversationEventArgs> OnConversationUpdated;
        event EventHandler<UcwaConversationEventArgs> OnConversationDeleted;
        event EventHandler<UcwaMessagingInvitationEventArgs> OnMessagingInvitationStarted;
        event EventHandler<UcwaMessagingInvitationEventArgs> OnMessagingInvitationUpdated;
        event EventHandler<UcwaMessagingInvitationEventArgs> OnMessagingInvitationCompleted;
        event EventHandler<UcwaClientEventArgs> OnMissedItemsUpdated;
        event EventHandler<UcwaOnlineMeetingInvitationEventArgs> OnOnlineMeetingInvitationStarted;
        event EventHandler<UcwaOnlineMeetingInvitationEventArgs> OnOnlineMeetingInvitationUpdated;
        event EventHandler<UcwaOnlineMeetingInvitationEventArgs> OnOnlineMeetingInvitationCompleted;
        event EventHandler<UcwaParticipantInvitationEventArgs> OnParticipantInvitationStarted;
        event EventHandler<UcwaParticipantInvitationEventArgs> OnParticipantInvitationUpdated;
        event EventHandler<UcwaParticipantInvitationEventArgs> OnParticipantInvitationCompleted;
        event EventHandler<UcwaClientEventArgs> OnPhoneAudioInvitationStarted;
        event EventHandler<UcwaClientEventArgs> OnPhoneAudioInvitationUpdated;
        event EventHandler<UcwaClientEventArgs> OnPhoneAudioInvitationCompleted;

        //Conversation Events
        event EventHandler<UcwaClientEventArgs> OnApplicationSharerAdded;
        event EventHandler<UcwaClientEventArgs> OnApplicationSharerUpdated;
        event EventHandler<UcwaClientEventArgs> OnApplicationSharerDeleted;
        event EventHandler<UcwaClientEventArgs> OnApplicationSharingUpdated;
        event EventHandler<UcwaClientEventArgs> OnAudioVideoUpdated;
        event EventHandler<UcwaClientEventArgs> OnDataCollaborationUpdated;
        event EventHandler<UcwaClientEventArgs> OnDataCollaborationSettingsUpdated;
        event EventHandler<UcwaClientEventArgs> OnLocalParticipantAdded;
        event EventHandler<UcwaClientEventArgs> OnLocalParticipantUpdated;
        event EventHandler<UcwaClientEventArgs> OnLocalParticipantDeleted;
        event EventHandler<UcwaMessageEventArgs> OnMessageStarted;
        event EventHandler<UcwaMessageEventArgs> OnMessageCompleted;
        event EventHandler<UcwaMessagingEventArgs> OnMessagingUpdated;
        event EventHandler<UcwaClientEventArgs> OnOnlineMeetingAdded;
        event EventHandler<UcwaClientEventArgs> OnOnlineMeetingUpdated;
        //event EventHandler<UcwaEventArgs> OnOnlineMeetingDeleted;
        event EventHandler<UcwaParticipantEventArgs> OnParticipantAdded;
        event EventHandler<UcwaParticipantEventArgs> OnParticipantUpdated;
        event EventHandler<UcwaParticipantEventArgs> OnParticipantDeleted;
        event EventHandler<UcwaClientEventArgs> OnParticipantApplicationSharingAdded;
        event EventHandler<UcwaClientEventArgs> OnParticipantApplicationSharingUpdated;
        event EventHandler<UcwaClientEventArgs> OnParticipantApplicationSharingDeleted;
        event EventHandler<UcwaClientEventArgs> OnParticipantAudioAdded;
        event EventHandler<UcwaClientEventArgs> OnParticipantAudioUpdated;
        event EventHandler<UcwaClientEventArgs> OnParticipantAudioDeleted;
        event EventHandler<UcwaClientEventArgs> OnParticipantDataCollaborationAdded;
        event EventHandler<UcwaClientEventArgs> OnParticipantDataCollaborationUpdated;
        event EventHandler<UcwaClientEventArgs> OnParticipantDataCollaborationDeleted;
        event EventHandler<UcwaClientEventArgs> OnParticipantMessagingAdded;
        event EventHandler<UcwaClientEventArgs> OnParticipantMessagingUpdated;
        event EventHandler<UcwaClientEventArgs> OnParticipantMessagingDeleted;
        event EventHandler<UcwaClientEventArgs> OnParticipantPanoramicVideoAdded;
        event EventHandler<UcwaClientEventArgs> OnParticipantPanoramicVideoUpdated;
        event EventHandler<UcwaClientEventArgs> OnParticipantPanoramicVideoDeleted;
        event EventHandler<UcwaClientEventArgs> OnParticipantVideoAdded;
        event EventHandler<UcwaClientEventArgs> OnParticipantVideoUpdated;
        event EventHandler<UcwaClientEventArgs> OnParticipantVideoDeleted;
        event EventHandler<UcwaClientEventArgs> OnPhoneAudioUpdated;
        event EventHandler<UcwaClientEventArgs> OnTypingParticipantAdded;
        event EventHandler<UcwaClientEventArgs> OnTypingParticipantDeleted;
        event EventHandler<UcwaClientEventArgs> OnVideoLockedOnParticipantAdded;
        event EventHandler<UcwaClientEventArgs> OnVideoLockedOnParticipantUpdated;
        event EventHandler<UcwaClientEventArgs> OnVideoLockedOnParticipantDeleted;

        //Me Events
        event EventHandler<EventArgs> OnMeUpdatedEvent;
        event EventHandler<UcwaClientEventArgs> OnLocationAdded;
        event EventHandler<UcwaClientEventArgs> OnLocationUpdated;
        event EventHandler<UcwaClientEventArgs> OnLocationDeleted;
        event EventHandler<UcwaClientEventArgs> OnNoteAdded;
        event EventHandler<UcwaClientEventArgs> OnNoteUpdated;
        event EventHandler<UcwaClientEventArgs> OnNoteDeleted;
        event EventHandler<UcwaClientEventArgs> OnPresenceAdded;
        event EventHandler<UcwaClientEventArgs> OnPresenceUpdated;
        event EventHandler<UcwaClientEventArgs> OnPresenceDeleted;

        //People Events
        event EventHandler<UcwaClientEventArgs> OnContactAdded;
        event EventHandler<UcwaClientEventArgs> OnContactDeleted;
        event EventHandler<UcwaClientEventArgs> OnContactLocationUpdated;
        event EventHandler<UcwaClientEventArgs> OnContactNoteUpdated;
        event EventHandler<UcwaContactPresenceEventArgs> OnContactPresenceUpdated;
        event EventHandler<UcwaClientEventArgs> OnContactPrivacyRelationshipUpdated;
        event EventHandler<UcwaClientEventArgs> OnContactSupportedModalitiesUpdated;
        event EventHandler<UcwaClientEventArgs> OnDefaultGroupAdded;
        event EventHandler<UcwaClientEventArgs> OnDefaultGroupUpdated;
        event EventHandler<UcwaClientEventArgs> OnDefaultGroupDeleted;
        event EventHandler<UcwaClientEventArgs> OnDelegatorsGroupAdded;
        event EventHandler<UcwaClientEventArgs> OnDelegatorsGroupUpdated;
        event EventHandler<UcwaClientEventArgs> OnDelegatorsGroupDeleted;
        event EventHandler<UcwaClientEventArgs> OnDistributionGroupAdded;
        event EventHandler<UcwaClientEventArgs> OnDistributionGroupUpdated;
        event EventHandler<UcwaClientEventArgs> OnDistributionGroupDeleted;
        event EventHandler<UcwaClientEventArgs> OnGroupAdded;
        event EventHandler<UcwaClientEventArgs> OnGroupUpdated;
        event EventHandler<UcwaClientEventArgs> OnGroupDeleted;
        event EventHandler<UcwaClientEventArgs> OnMyContactsAndGroupsSubscriptionUpdated;
        event EventHandler<UcwaClientEventArgs> OnMyOrganizationGroupAdded;
        event EventHandler<UcwaClientEventArgs> OnMyOrganizationGroupUpdated;
        event EventHandler<UcwaClientEventArgs> OnMyOrganizationGroupDeleted;
        event EventHandler<UcwaClientEventArgs> OnPinnedGroupAdded;
        event EventHandler<UcwaClientEventArgs> OnPinnedGroupUpdated;
        event EventHandler<UcwaClientEventArgs> OnPinnedGroupDeleted;
        event EventHandler<UcwaPresenceSubscriptionEventArgs> OnPresenceSubscriptionAdded;
        event EventHandler<UcwaPresenceSubscriptionEventArgs> OnPresenceSubscriptionUpdated;
        event EventHandler<UcwaPresenceSubscriptionEventArgs> OnPresenceSubscriptionDeleted;

        //Policies Events
        event EventHandler<UcwaClientEventArgs> OnPoliciesUpdated;
    }

    public class UcwaClientEventArgs : EventArgs
    {
        public dynamic embeddedObject { get; set; }
        public string rel { get; set; }
        public IHttpUtility httpUtility { get; set; }
        public string href { get; set; }

        public UcwaClientEventArgs(IHttpUtility HttpUtility, string rel, dynamic embeddedObject, string href = null)
        {
            this.httpUtility = HttpUtility;
            this.rel = rel;
            this.embeddedObject = embeddedObject;
            this.href = href;
        }
    }

    public class UcwaConversationEventArgs : EventArgs
    {
        public IConversationResource conversation;
        public string href;

        public UcwaConversationEventArgs(IConversationResource conversationResource, string href)
        {
            conversation = conversationResource;
            this.href = href;
        }
    }

    public class UcwaOnlineMeetingInvitationEventArgs : EventArgs
    {
        public IOnlineMeetingInvitationResource onlineMeetingInvitation;

        public UcwaOnlineMeetingInvitationEventArgs(IOnlineMeetingInvitationResource onlineMeetingInvitationResource)
        {
            onlineMeetingInvitation = onlineMeetingInvitationResource;
        }
    }

    public class UcwaMessagingInvitationEventArgs : EventArgs
    {
        public IMessagingInvitationResource messagingInvitation;

        public UcwaMessagingInvitationEventArgs(IMessagingInvitationResource messagingInvitationResource)
        {
            this.messagingInvitation = messagingInvitationResource;
        }
    }

    public class UcwaMessagingEventArgs : EventArgs
    {
        public IMessagingResource messaging;

        public UcwaMessagingEventArgs(IMessagingResource messagingResource)
        {
            this.messaging = messagingResource;
        }
    }

    public class UcwaParticipantInvitationEventArgs : EventArgs
    {
        public IParticipantInvitationResource participantInvitation;

        public UcwaParticipantInvitationEventArgs(IParticipantInvitationResource participantInvitationResource)
        {
            participantInvitation = participantInvitationResource;
        }
    }

    public class UcwaParticipantEventArgs : EventArgs
    {
        public IParticipantResource participant;
        public string contextId;
        public string participantLinkHref;

        public UcwaParticipantEventArgs(IParticipantResource participantResource, string contextId, string participantLinkHref)
        {
            participant = participantResource;
            this.contextId = contextId;
            this.participantLinkHref = participantLinkHref;
        }
    }

    public class UcwaMessageEventArgs : EventArgs
    {
        public IMessageResource message;

        public UcwaMessageEventArgs(IMessageResource messageResource)
        {
            message = messageResource;
        }
    }

    public class UcwaContactPresenceEventArgs : EventArgs
    {
        public IContactResource contact;
        public IContactPresenceResource contactPresence;

        public UcwaContactPresenceEventArgs(IContactResource contactResource, IContactPresenceResource contactPresenceResource)
        {
            contact = contactResource;
            contactPresence = contactPresenceResource;
        }
    }

    public class UcwaPresenceSubscriptionEventArgs : EventArgs
    {
        public IPresenceSubscriptionResource presenceSubscription;

        public UcwaPresenceSubscriptionEventArgs(IPresenceSubscriptionResource presenceSubscriptionResource)
        {
            presenceSubscription = presenceSubscriptionResource;
        }
    }
}
