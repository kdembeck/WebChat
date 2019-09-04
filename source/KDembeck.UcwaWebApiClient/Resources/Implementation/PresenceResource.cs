using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PresenceResource : ResourceBase, IPresenceResource
    {
        public string activity { get; set; }
        public Availability? availability { get; set; }
        public PresenceLinks _links { get; set; }

        public PresenceResource()
        {
            initializeProperties();
        }

        public PresenceResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            activity = null;
            availability = null;
            _links = new PresenceLinks();
        }
        
        public async Task<IPresenceResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IPresenceResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
                
        public async Task setAvailability(PreferredAvailability? availability = null)
        {
            if (httpUtility != null && _links.self != null)
            {
                if (availability != null)
                {
                    string availabilityJson = JsonConvert.SerializeObject(new
                    {
                        availability = availability
                    }, new Newtonsoft.Json.Converters.StringEnumConverter());

                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href, availabilityJson);

                }
                else
                {
                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href);
                }
            }
        }
    }
}
