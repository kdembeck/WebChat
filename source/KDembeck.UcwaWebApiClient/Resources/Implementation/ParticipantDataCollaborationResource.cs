using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantDataCollaborationResource : ResourceBase, IParticipantDataCollaborationResource
    {
        public ParticipantDataCollaborationLinks _links { get; set; }

        private void initializeProperties()
        {
            _links = new ParticipantDataCollaborationLinks();
        }

        public ParticipantDataCollaborationResource()
        {
            initializeProperties();
        }

        public ParticipantDataCollaborationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IParticipantDataCollaborationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantDataCollaborationResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
