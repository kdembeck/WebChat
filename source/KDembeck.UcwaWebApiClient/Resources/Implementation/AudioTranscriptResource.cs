using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class AudioTranscriptResource : ResourceBase, IAudioTranscriptResource
    {
        public string duration { get; set; }
        public string status { get; set; }
        public AudioTranscriptLinks _links { get; set; }

        private void initializeProperties()
        {
            duration = null;
            status = null;
            _links = new AudioTranscriptLinks();
        }

        public AudioTranscriptResource()
        {
            initializeProperties();
        }

        public AudioTranscriptResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IAudioTranscriptResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IAudioTranscriptResource> Get()
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
