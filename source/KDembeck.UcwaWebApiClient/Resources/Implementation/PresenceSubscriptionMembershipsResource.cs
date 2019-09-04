using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PresenceSubscriptionMembershipsResource : ResourceBase, IPresenceSubscriptionMembershipsResource
    {
        public PresenceSubscriptionMembershipsLinks _links { get; set; }
        public PresenceSubscriptionMembershipsEmbedded _embedded { get; set; }
        public List<PresenceSubscriptionMembershipResource> presenceSubscriptionMemberships { get { return _embedded.presenceSubscriptionMembership; } }

        public PresenceSubscriptionMembershipsResource()
        {
            initializeProperties();
        }

        public PresenceSubscriptionMembershipsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new PresenceSubscriptionMembershipsLinks();
            _embedded = new PresenceSubscriptionMembershipsEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.presenceSubscriptionMembership != null && _embedded.presenceSubscriptionMembership.Count > 0)
                {
                    foreach (PresenceSubscriptionMembershipResource presenceSubscriptionMembershipResource in _embedded.presenceSubscriptionMembership)
                    {
                        presenceSubscriptionMembershipResource.httpUtility = httpUtility;
                    }
                }
            }
        }
        
        public async Task<IPresenceSubscriptionMembershipsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public new async Task<IPresenceSubscriptionMembershipsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }
                
        public async Task<IPresenceSubscriptionMembershipsResource> subscribeToContacts(List<string> contactUris)
        {
            if (httpUtility != null && _links.self != null)
            {
                string contactUrisString = JsonConvert.SerializeObject(new
                {
                    contactUris = contactUris
                });
                string presenceSubscriptionMembershipsResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href, contactUrisString);
                JsonConvert.PopulateObject(presenceSubscriptionMembershipsResourceString, this);
            }
            return this;
        }
    }
}
