using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantVideoResource : ResourceBase, IParticipantVideoResource
    {
        public MediaDirectionType? videoDirection { get; set; }
        public bool? videoMuted { get; set; }
        public string videoSourceId { get; set; }
        public ParticipantVideoLinks _links { get; set; }

        private void initializeProperties()
        {
            videoDirection = null;
            videoMuted = null;
            videoSourceId = null;
            _links = new ParticipantVideoLinks();
        }

        public ParticipantVideoResource()
        {
            initializeProperties();
        }

        public ParticipantVideoResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IParticipantVideoResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantVideoResource> Get()
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

        public async Task muteVideo()
        {
            if (httpUtility != null && _links.muteVideo != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.muteVideo.href);
            }
        }

        public async Task unmuteVideo()
        {
            if (httpUtility != null && _links.unmuteVideo != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.unmuteVideo.href);
            }
        }
    }
}
