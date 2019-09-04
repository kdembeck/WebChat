using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class LocationResource : ResourceBase, ILocationResource
    {
        public string location { get; set; }
        public LocationLinks _links { get; set; }

        public LocationResource()
        {
            initializeProperties();
        }

        public LocationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            location = null;
            _links = new LocationLinks();
        }
                
        public async Task<ILocationResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                
            }
            return this;
        }

        public new async Task<ILocationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);

            }
            return this;
        }
                
        public async Task setLocation(string location)
        {
            if (httpUtility != null && _links.self != null)
            {
                string locationJson = JsonConvert.SerializeObject(new
                {
                    location = location
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href, locationJson);
            }
        }
    }
}
