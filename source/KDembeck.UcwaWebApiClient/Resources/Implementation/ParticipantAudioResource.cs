using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantAudioResource : ResourceBase, IParticipantAudioResource
    {
        public MediaDirectionType? audioDirection { get; set; }
        public bool? audioMuted { get; set; }
        public string audioSourceId { get; set; }
        public ParticipantAudioLinks _links { get; set; }

        private void initializeProperties()
        {
            audioDirection = null;
            audioMuted = null;
            audioSourceId = null;
            _links = new ParticipantAudioLinks();
        }

        public ParticipantAudioResource()
        {
            initializeProperties();
        }

        public ParticipantAudioResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IParticipantAudioResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantAudioResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task muteAudio()
        {
            if (httpUtility != null && _links.muteAudio != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.muteAudio.href);
            }
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

        public async Task unmuteAudio()
        {
            if (httpUtility != null && _links.unmuteAudio != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.unmuteAudio.href);
            }
        }
    }
}
