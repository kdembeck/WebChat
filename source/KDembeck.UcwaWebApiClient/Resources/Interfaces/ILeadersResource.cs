using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ILeadersResource : IResourceBase
    {
        LeadersLinks _links { get; set; }
        new Task<ILeadersResource> Get(string resourceUrl);
        Task<ILeadersResource> Get();
        Task<IParticipantResource> getParticipant();
    }

    public class LeadersLinks
    {
        public Link self;
        public Link participant;
    }
}
