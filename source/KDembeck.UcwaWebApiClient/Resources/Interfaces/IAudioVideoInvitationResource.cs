using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IAudioVideoInvitationResource
    {
        string address { get; set; }
        string bandwidthControlId { get; set; }
        string building { get; set; }
        string city { get; set; }
        string country { get; set; }
        string customContent { get; set; }
        string delegator { get; set; }
        string direction { get; set; }
        string importance { get; set; }
        bool? joinAudioMuted { get; set; }
        bool? joinVideoMuted { get; set; }
        string locationState { get; set; }
        string operationId { get; set; }
        string privateLine { get; set; }
        string mediaOffer { get; set; }
        string sessionContext { get; set; }
        string state { get; set; }
        string subject { get; set; }
        string threadId { get; set; }
        string to { get; set; }
        string zip { get; set; }
        AudioVideoInvitationLinks _links { get; set; }
        AudioVideoInvitationEmbedded _embedded { get; set; }

        Task<IAudioVideoInvitationResource> Get(string resourceUrl);

        Task<IAudioVideoInvitationResource> Get();

        Task<IContactResource> getAcceptedByContact();

        Task acceptWithAnswer(string sessionContext, string processedOfferId = null, byte[] spd = null);

        Task acceptWithPhoneAudio();

        Task<IAudioVideoResource> getAudioVideo();
        Task cancel();

        Task<IConversationResource> getConversation();

        Task decline();

        Task<IContactResource> getDelegator();

        Task<IAudioVideoResource> getDerivedAudioVideo();

        Task<IConversationResource> getDerivedConversation();

        Task<IContactResource> getForwardedBy();

        Task<IParticipantResource> getFrom();

        Task<IContactResource> getOnBehalfOf();

        Task getReplacesAudioVideo();

        Task reportMediaDiagnostics(ErrorCode? errorCode = null, ErrorSubcode? errorSubcode = null);
        Task sendProvisionalAnswer(string sessionContext, string processOfferId = null, byte[] sdp = null);

        Task<IContactResource> getTo();

        Task<IContactResource> getTrasnferredBy();
    }

    public class AudioVideoInvitationLinks
    {
        public Link self;
        public Link acceptedByContact;
        public Link acceptWithAnswer;
        public Link acceptWithPhoneAudio;
        public Link audioVideo;
        public Link cancel;
        public Link conversation;
        public Link decline;
        public Link delegator;
        public Link derivedAudioVideo;
        public Link derivedConversation;
        public Link forwardedBy;
        public Link from;
        public Link onBehalfOf;
        public Link replacesAudioVideo;
        public Link reportMediaDiagnostics;
        public Link sendProvisionalAnswer;
        public Link to;
        public Link transferredBy;
    }

    public class AudioVideoInvitationEmbedded
    {
        public List<ParticipantResource> acceptedByParticipant;
        public ParticipantResource from;
        //public StartEmergencyCallParametersResource startEmergencyCallInput;
    }
}
