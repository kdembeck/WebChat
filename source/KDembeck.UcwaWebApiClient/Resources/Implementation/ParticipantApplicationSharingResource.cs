using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantApplicationSharingResource : ResourceBase, IParticipantApplicationSharingResource
    {
        public MediaDirectionType? applicationSharingDirection { get; set; }
        public string applicationSharingSourceId { get; set; }
        public ParticipantApplicationSharingLinks _links { get; set; }

        private void initializeProperties()
        {
            applicationSharingDirection = null;
            applicationSharingSourceId = null;
            _links = new ParticipantApplicationSharingLinks();
        }

        public ParticipantApplicationSharingResource()
        {
            initializeProperties();
        }

        public ParticipantApplicationSharingResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IParticipantApplicationSharingResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantApplicationSharingResource> Get()
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
