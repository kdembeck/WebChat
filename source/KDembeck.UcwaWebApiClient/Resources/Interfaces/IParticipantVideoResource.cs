using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantVideoResource : IResourceBase
    {
        MediaDirectionType? videoDirection { get; set; }
        bool? videoMuted { get; set; }
        string videoSourceId { get; set; }
        ParticipantVideoLinks _links { get; set; }
        new Task<IParticipantVideoResource> Get(string resourceUrl);
        Task<IParticipantVideoResource> Get();
        Task<IParticipantResource> getParticipant();
        Task muteVideo();
        Task unmuteVideo();
    }

    public class ParticipantVideoLinks
    {
        public Link self;
        public Link muteVideo;
        public Link participant;
        public Link unmuteVideo;
    }
}
