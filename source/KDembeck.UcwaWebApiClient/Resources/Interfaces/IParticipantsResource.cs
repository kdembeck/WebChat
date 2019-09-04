using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantsResource : IResourceBase
    {
        ParticipantsLinks _links { get; set; }

        new Task<IParticipantsResource> Get(string resourceUrl);
        Task<IParticipantsResource> Get();
    }

    public class ParticipantsLinks
    {
        public Link self;
        public List<ParticipantLink> participant;
    }

    public class ParticipantLink
    {
        public string href;
        public string title;
    }
}
