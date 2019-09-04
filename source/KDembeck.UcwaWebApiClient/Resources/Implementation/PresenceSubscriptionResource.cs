using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PresenceSubscriptionResource : ResourceBase, IPresenceSubscriptionResource
    {
        public string id { get; set; }
        public PresenceSubscriptionLinks _links { get; set; }

        public PresenceSubscriptionResource()
        {
            initializeProperties();
        }

        public PresenceSubscriptionResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            id = null;
            _links = new PresenceSubscriptionLinks();            
        }
                
        public async Task<IPresenceSubscriptionResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public new async Task<IPresenceSubscriptionResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }
                
        public async Task<IPresenceSubscriptionResource> extendPresenceSubscription(int duration)
        {
            if (httpUtility != null && _links.self != null)
            {
                if (duration > 30)
                    duration = 30;
                else if (duration < 10)
                    duration = 10;

                string presenceSubscriptionJson = JsonConvert.SerializeObject(new
                {
                    duration = duration
                });

                string presenceSubscriptionResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href + "?duration=" + duration.ToString(), presenceSubscriptionJson);
                JsonConvert.PopulateObject(presenceSubscriptionResourceString, this);                
            }
            return this;
        }
                
        public async Task deletePresenceSubscription()
        {
            if (httpUtility != null && _links.self != null)
            {
                await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
        }

        public async Task<IPresenceSubscriptionMembershipsResource> addToPresencesSubscription(List<string> contactUris)
        {
            if (httpUtility != null && _links.addToPresenceSubscription != null)
            {
                string contactUrisJson = JsonConvert.SerializeObject(new
                {
                    contactUris = contactUris
                });
                string presenceSubscriptionMembershiptsResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addToPresenceSubscription.href, contactUrisJson);
                IPresenceSubscriptionMembershipsResource presenceSubscriptionMembershipsResource = new PresenceSubscriptionMembershipsResource(httpUtility);
                JsonConvert.PopulateObject(presenceSubscriptionMembershiptsResourceString, presenceSubscriptionMembershipsResource);
                return presenceSubscriptionMembershipsResource;
            }
            else
                return null;
        }

        public async Task<IPresenceSubscriptionMembershipsResource> getMemberships()
        {
            if (httpUtility != null & _links.memberships != null)
            {
                IPresenceSubscriptionMembershipsResource membershipsResource = new PresenceSubscriptionMembershipsResource(httpUtility);
                await membershipsResource.Get(httpUtility.baseUrl + _links.memberships.href);
                return membershipsResource;
            }
            else
                return null;
        }
    }
}
