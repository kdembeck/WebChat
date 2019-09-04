using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IAudioVideoResource
    {
        string state { get; set; }
        string supportsReplaces { get; set; }
        VideoSourcesAllowed? videoSourcesAllowed { get; set; }
        AudioVideoLinks _links { get; set; }
        Task<IAudioVideoResource> Get(string resourceUrl);
        Task<IAudioVideoResource> Get();
        Task addAudio(string toUri, string operationId = null, string mediaOffer = null, string sessionContext = null, bool? joinAudioMuted = null);
        Task addAudioVideo(string toUri, string operationId = null, string mediaOffer = null, string sessionContext = null, bool? joinAudioMuted = null, bool? joinVideoMuted = null);
        Task<IAudioVideoPoliciesResource> getAudioVideoPolicies();
        Task<IAudioVideoSessionResource> getAudioVideoSession();
        Task<IConversationResource> getConversation();
        Task escalateAudioVideo(string operationId, string sessionContext, byte[] sdp = null);
        Task escalateAudio(string operationId, string sessionContext, byte[] sdp = null);
        Task replaceWithPhoneAudio(string toUri, string phoneNumber = null, string operationId = null);
        Task reportMediaDiagnostics(ErrorCode? errorCode = null, ErrorSubcode? errorSubcode = null);
        Task stopAudio();
        Task stopAudioVideo();
        Task stopVideo();
        Task videoFreeze();
        Task<IParticipantResource> videoLockedOnParticipant();
    }

    public class AudioVideoLinks
    {
        public Link self;
        public Link addAudio;
        public Link addAudioVideo;
        public Link audioVideoPolicies;
        public Link audioVideoSession;
        public Link conversation;
        public Link escalateAudioVideo;
        public Link escalateAudio;
        public Link replaceWithPhoneAudio;
        public Link reportMediaDiagnostics;
        public Link stopAudio;
        public Link stopAudioVideo;
        public Link stopVideo;
        public Link videoFreeze;
        public Link videoLockedOnParticipant;
    }
}
