using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ContactPresenceResource : ResourceBase, IContactPresenceResource
    {
        public string activity { get; set; }
        public Availability? availability { get; set; }
        public string deviceType { get; set; }
        public string lastActive { get; set; }
        public ContactPresenceLinks _links { get; set; }

        private void initializeProperties()
        {
            activity = null;
            availability = null;
            deviceType = null;
            lastActive = null;
            _links = new ContactPresenceLinks();
        }

        public ContactPresenceResource()
        {
            initializeProperties();
        }

        public ContactPresenceResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public async Task<IContactPresenceResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IContactPresenceResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
