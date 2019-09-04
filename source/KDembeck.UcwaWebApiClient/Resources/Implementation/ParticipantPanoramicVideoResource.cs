using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantPanoramicVideoResource : ResourceBase, IParticipantPanoramicVideoResource
    {
        public MediaDirectionType? panoramicVideoDirection { get; set; }
        public bool? panoramicVideoMuted { get; set; }
        public string panoramicVideoSourceId { get; set; }
        public ParticipantPanoramicVideoLinks _links { get; set; }

        private void initializeProperties()
        {
            panoramicVideoDirection = null;
            panoramicVideoMuted = null;
            panoramicVideoSourceId = null;
            _links = new ParticipantPanoramicVideoLinks();
        }

        public ParticipantPanoramicVideoResource()
        {
            initializeProperties();
        }

        public ParticipantPanoramicVideoResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IParticipantPanoramicVideoResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantPanoramicVideoResource> Get()
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
