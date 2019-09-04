using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantDataCollaborationResource : IResourceBase
    {
        ParticipantDataCollaborationLinks _links { get; set; }
        new Task<IParticipantDataCollaborationResource> Get(string resourceUrl);
        Task<IParticipantDataCollaborationResource> Get();
    }

    public class ParticipantDataCollaborationLinks
    {
        public Link self;
        public Link participant;
    }
}
