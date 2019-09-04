using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class AudioVideoPoliciesResource : ResourceBase, IAudioVideoPoliciesResource
    {
        public string joinScheduledOnlineMeeting { get; set; }
        public string multiView { get; set; }
        public AudioVideoPoliciesLinks _links { get; set; }

        private void initializeProperties()
        {
            joinScheduledOnlineMeeting = null;
            multiView = null;
            _links = new AudioVideoPoliciesLinks();
        }

        public AudioVideoPoliciesResource()
        {
            initializeProperties();
        }

        public AudioVideoPoliciesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IAudioVideoPoliciesResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IAudioVideoPoliciesResource> Get()
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
