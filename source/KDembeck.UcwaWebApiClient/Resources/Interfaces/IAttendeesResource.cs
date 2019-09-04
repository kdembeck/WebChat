using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IAttendeesResource
    {
        AttendeesLinks _links { get; }
        Task<IAttendeesResource> Get(string resourceUrl);
        Task<IAttendeesResource> Get();
        Task<List<IParticipantResource>> getParticipants();
    }

    public class AttendeesLinks
    {
        public Link self;
        public List<Link> participant;
    }
}
