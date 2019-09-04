using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public enum MessageFormat { Html, Plain };
    public enum ModalityType { Audio, Messaging, PanoramicVideo, PhoneAudio, Video , ApplicationSharing };
    public enum AudioPreference { PhoneAudio, VoipAudio };
    public enum PreferredAvailability { Away, BeRightBack, Busy, DoNotDisturb, Offwork, Online };
    public enum NoteType { OutOfOffice, Personal };
    public enum PhoneType { Work, Home, Other, Mobile };
    public enum Availability { Away, BeRightBack, Busy, DoNotDisturb, IdleBusy, IdleOnline, Offline, Online, None };
    public enum SubscriptionState { Connected, Connecting, Disconnected, Disconnecting };
    public enum PrivacyRelationshipLevel { Blocked, Colleagues, External, FriendsAndFamily, Unknown, Workgroup };
    public enum ParticipantRole { Attendee, Leader, Unknown };
    public enum SourceNetwork { Everyone, Federated, PublicCloud, SameEnterprise, Unknown };
    public enum VideoSourcesAllowed { Everyone, OneParticipant, PresentersOnly, Unknown };
    public enum AccessLevel { None, SameEnterprise, Locked, Invited, Everyone };
    public enum AutomaticLeaderAssignment { Disabled, SameEnterprise, Everyone };
    public enum EntryExitAnnouncement { Unsupported, Disabled, Enabled };
    public enum LobbyBypassForPhoneUsers { Enabled, Disabled };
    public enum PhoneUserAdmission { Enabled, Disabled };
    public enum VideoBasedScreenSharing { Enabled, Disabled };
    public enum SimultaneousRingNumberMatch { Enabled, Disabled };
    public enum ConversationHistory { Enabled, Disabled };
    public enum Importance { Normal, Urgent, Emergency };
    public enum MediaDirectionType { SendReceive, SendOnly, ReceiveOnly, Inactive };
    public enum ConnectionState { Connecting, Connected, Disconnected };    
    public enum InvitationDirection { Incoming, Outgoing };
    public enum InvitationState { Connected, Connecting, Failed };
    public enum ErrorCode { BadRequest, Forbidden, NotFound, MethodNotAllowed, NotAcceptable, Conflict, Gone, PreconditionFailed,
        EntityTooLarge, UnsupportedMediaType, PreconditionRequired, TooManyRequests, ServiceFailure, ServiceUnavailable, GatewayTimeout,
        ExchangeServiceFailure, Timeout, LocalFailure, RemoteFailure, Informational };
    public enum ErrorSubcode { None, Timeout, UnsupportedMediaType, DeserializationFailure, AnonymousNotAllowed, InviteesOnly,
        AlreadyExists, AnotherOperationPending, APIVersionNotSupported, NormalizationFailed, ProvisioningDataUnavailable,
        ApplicationNotFound, TooManyApplications, InactiveApplicationExpired, UserAgentNotAllowed, LimitExceeded, OperationNotSupported,
        NoDelegatesConfigured, NoTeamMembersConfigured, MakeMeAvailableRequired, LisServiceUnavailable, NoLocationFound, TooManyContacts,
        MigrationInProgress, TooManyGroups, TooManyOnlineMeetings, ThreadIdAlreadyExists, DoNotDisturb, ConnectedElsewhere, Missed,
        MediaFallback, FederationRequired, Canceled, Declined, Forwarded, Transferred, Replaced, EscalationFailed, InsufficientBandwidth,
        RepliedWithOtherModality, DestinationNotFound, DialoutNotAllowed, Unreachable, MediaEncryptionNotSupported, MediaEncryptionRequired,
        MediaEncryptionMismatch, Unavailable, TooManyParticipants, TooManyLobbyParticipants, Busy, AttendeeNotAllowed, Demoted, MediaFailure,
        InvalidMediaDescription, Removed, TemporarilyUnavailable, ModalityNotSupported, NotAllowed, Ejected, Denied, Ended,
        ParameterValidationFailure, SessionSwitched, DerivedConversation, Redirected, Expired, NotAcceptable, NewContentSharer,
        PhoneNumberConflict, IPv6NotSupported, PGetReplaced, PstnCallFailed, TransferDeclined, TransferTargetDeclined, CallbackUriUnreachable,
        ConversationNotFound, AutoAccepted, TooManyConversations, MediaNegotiationFailure, MediaNegotiationTimeOut, CallTerminated,
        MaxEventCountReached, CallbackChannelError, InvalidExchangeServerVersion, UnprocessableEntity, ExchangeTimeout, CannotRedirect };
}
