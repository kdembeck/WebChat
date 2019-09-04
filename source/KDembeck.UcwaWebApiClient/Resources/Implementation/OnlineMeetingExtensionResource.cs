using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingExtensionResource : ResourceBase, IOnlineMeetingExtensionResource
    {
        public OnlineMeetingExtensionLinks _links { get; set; }
        public string id { get; set; }
        public string type { get; set; }

        public OnlineMeetingExtensionResource(IHttpUtility HttpUtility)
        {
            initializeProperties();
            httpUtility = HttpUtility;
        }

        public OnlineMeetingExtensionResource()
        {
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new OnlineMeetingExtensionLinks();
            id = null;
            type = null;
        }

        public new async Task<IOnlineMeetingExtensionResource> Get(string ResourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(ResourceUrl);
            }
            else
            {
                //raise an error
            }
            return this;
        }

        public async Task<IOnlineMeetingExtensionResource> Get()
        {
            if (httpUtility != null && _links.self.href != null)
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
