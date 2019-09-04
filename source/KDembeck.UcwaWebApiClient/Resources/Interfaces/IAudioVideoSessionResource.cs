using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IAudioVideoSessionResource
    {
        string remoteEndpoint { get; set; }
        string sessionContext { get; set; }
        string state { get; set; }
        AudioVideoSessionLinks _links { get; set; }
        Task<IAudioVideoSessionResource> Get(string resourceUrl);
        Task<IAudioVideoSessionResource> Get();
        Task<IApplicationSharingResource> getApplicationSharing();
        Task<IAudioVideoResource> getAudioVideo();
        Task<IConversationResource> getConversation();
        Task<IDataCollaborationResource> getDataCollaboration();
        Task publishCallQualityFeedback(string mediaEndpoint = null, string mediaQualityOfExperience = null);
        Task renegotiations(string operationId, byte[] sdp = null);
        Task resumeAudio(string operationId = null, byte[] sdp = null);
        Task resumeAudioVideo(string operationId = null, byte[] sdp = null);
    }

    public class AudioVideoSessionLinks
    {
        public Link self;
        public Link applicationSharing;
        public Link audioVideo;
        public Link conversation;
        public Link dataCollaboration;
        public Link publishCallQualityFeedback;
        public Link renegotiations;
        public Link resumeAudio;
        public Link resumeAudioVideo;
    }
}
