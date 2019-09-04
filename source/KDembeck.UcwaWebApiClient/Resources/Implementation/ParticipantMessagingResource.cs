using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantMessagingResource : ResourceBase, IParticipantMessagingResource
    {
        public ParticipantMessagingLinks _links { get; set; }

        private void initializeProperties()
        {
            _links = new ParticipantMessagingLinks();
        }

        public ParticipantMessagingResource()
        {
            initializeProperties();
        }

        public ParticipantMessagingResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IParticipantMessagingResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantMessagingResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantResource> getParticipant()
        {
            if (httpUtility != null && _links.participant != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.participant.href);
                return participantResource;
            }
            else
                return null;
        }
    }
}
