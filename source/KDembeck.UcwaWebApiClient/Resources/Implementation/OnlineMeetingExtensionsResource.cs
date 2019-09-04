using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingExtensionsResource : ResourceBase, IOnlineMeetingExtensionsResource
    {
        public OnlineMeetingExtensionsLinks _links { get; set; }
        public OnlineMeetingExtensionsEmbedded _embedded { get; set; }

        public OnlineMeetingExtensionsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public OnlineMeetingExtensionsResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new OnlineMeetingExtensionsLinks();
            _embedded = new OnlineMeetingExtensionsEmbedded();
        }

        public new async Task<IOnlineMeetingExtensionsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            else
            {
                //raise an error
            }
            return this;
        }

        public async Task<IOnlineMeetingExtensionsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            else
            {
                //raise an error
            }
            return this;
        }
    }
}
