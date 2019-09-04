using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantApplicationSharingResource : IResourceBase
    {
        MediaDirectionType? applicationSharingDirection { get; set; }
        string applicationSharingSourceId { get; set; }
        ParticipantApplicationSharingLinks _links { get; set; }
        new Task<IParticipantApplicationSharingResource> Get(string resourceUrl);
        Task<IParticipantApplicationSharingResource> Get();
        Task<IParticipantResource> getParticipant();
    }

    public class ParticipantApplicationSharingLinks
    {
        public Link self;
        public Link participant;
    }
}
