using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantPanoramicVideoResource : IResourceBase
    {
        MediaDirectionType? panoramicVideoDirection { get; set; }
        bool? panoramicVideoMuted { get; set; }
        string panoramicVideoSourceId { get; set; }
        ParticipantPanoramicVideoLinks _links { get; set; }
        new Task<IParticipantPanoramicVideoResource> Get(string resourceUrl);
        Task<IParticipantPanoramicVideoResource> Get();
        Task<IParticipantResource> getParticipant();
    }

    public class ParticipantPanoramicVideoLinks
    {
        public Link self;
        public Link participant;
    }
}
