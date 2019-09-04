using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ICommunicationResource : IResourceBase
    {
        SimultaneousRingNumberMatch? simultaneousRingNumberMatch { get; set; }
        VideoBasedScreenSharing? videoBasedScreenSharing { get; set; }
        AudioPreference? audioPreference { get; set; }
        ConversationHistory? conversationHistory { get; set; }
        string lisLocation { get; set; }
        string lisQueryResult { get; set; }
        string phoneNumber { get; set; }
        bool? publishEndpointLocation { get; set; }
        List<MessageFormat> supportedMessageFormats { get; set; }
        List<ModalityType> supportedModalities { get; set; }
        CommunicationLinks _links { get; set; }
        Task<ICommunicationResource> Get(string resourceUrl);
        Task<ICommunicationResource> Get();
        Task<IConversationLogsResource> getConversationLogs();
        Task<List<IConversationResource>> getConversations();
        Task<IOnlineMeetingInvitationResource> joinExistingOnlineMeeting(string onlineMeetingUri, string operationId = null);
        Task<IOnlineMeetingInvitationResource> createAndJoinNewOnlineMeeting(string subject, Importance? importance = null, string operationId = null, string threadId = null);
        Task<IMediaPoliciesResource> getMediaPolicies();
        Task<IMissedItemsResource> getMissedItems();
        Task replayMessage();
        Task startAudio(string toUri, string operationId = null, string subject = null, Importance? importance = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, string mediaOffer = null);
        Task startAudioOnBehalfOfDelegator(string toUri, string subject = null, Importance? importance = null, string delegator = null, string operationId = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, string customContent = null, string mediaOffer = null);
        Task startAudioVideo(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, bool? joinVideoMuted = null, string mediaOffer = null);
        Task startEmergencyCall(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, string mediaOffer = null, string address = null, string building = null, string city = null, string country = null, string state = null, string zip = null);
        Task startMessaging(string operationId, string toUri = null, string subject = null, Importance? importance = null, string message = null, MessageFormat messageFormat = MessageFormat.Plain, string threadId = null, string customContent = null);
        Task startOnlineMeeting(string subject = null, Importance? importance = null, string operationId = null, string threadId = null);
        Task startPhoneAudioOnBehalfOfDelegator(string toUri, string subject = null, Importance? importance = null, string delegator = null, string operationId = null, string threadId = null);
        Task startPhoneAudio(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, string customContent = null);
        Task startVideo(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, bool? joinVideoMuted = null, string sessionContext = null, string mediaOffer = null);
        void Handle_OnCommunicationUpdatedEvent(object sender, EventArgs eventArgs);
    }

    public class CommunicationLinks
    {
        public Link self;
        public Link conversationLogs;
        public Link conversations;
        public Link joinOnlineMeeting;
        public Link mediaPolicies;
        public Link mediaRelayAccessToken;
        public Link missedItems;
        public Link replayMessage;
        public Link startAudio;
        public Link startAudioOnBehalfOfDelegator;
        public Link startAudioVideo;
        public Link startEmergencyCall;
        public Link startMessaging;
        public Link startOnlineMeeting;
        public Link startPhoneAudioOnBehalfOfDelegator;
        public Link startPhoneAudio;
        public Link startVideo;
    }
}
