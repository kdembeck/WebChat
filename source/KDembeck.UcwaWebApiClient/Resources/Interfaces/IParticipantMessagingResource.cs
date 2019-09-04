using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantMessagingResource : IResourceBase
    {
        ParticipantMessagingLinks _links { get; set; }

        new Task<IParticipantMessagingResource> Get(string resourceUrl);
        Task<IParticipantMessagingResource> Get();
        Task<IParticipantResource> getParticipant();
    }

    public class ParticipantMessagingLinks
    {
        public Link self;
        public Link participant;
    }
}
