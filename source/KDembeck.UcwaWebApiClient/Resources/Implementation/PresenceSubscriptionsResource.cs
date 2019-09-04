using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PresenceSubscriptionsResource : ResourceBase, IPresenceSubscriptionsResource
    {
        public List<PresenceSubscriptionResource> presenceSubscriptions { get; }
        public PresenceSubscriptionsLinks _links { get; set; }
        public PresenceSubscriptionsEmbedded _embedded { get; set; }

        public PresenceSubscriptionsResource()
        {
            initializeProperties();
        }

        public PresenceSubscriptionsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new PresenceSubscriptionsLinks();
            _embedded = new PresenceSubscriptionsEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.presenceSubscription != null && _embedded.presenceSubscription.Count > 0)
                {
                    foreach (PresenceSubscriptionResource presenceSubscriptionResource in _embedded.presenceSubscription)
                    {
                        presenceSubscriptionResource.httpUtility = httpUtility;
                    }
                }
            }
        }
        
        public async Task<IPresenceSubscriptionsResource> Get()
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

        public new async Task<IPresenceSubscriptionsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IPresenceSubscriptionResource> newPresenceSubscription(int duration, List<string> uris)
        {
            if (httpUtility != null && _links.self != null)
            {
                if (duration > 30)
                    duration = 30;
                else if (duration < 10)
                    duration = 10;

                string presenceSubscriptionJson = JsonConvert.SerializeObject(new
                {
                    duration = duration,
                    uris = uris
                });

                string presenceSubscriptionResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href, presenceSubscriptionJson);
                IPresenceSubscriptionResource presenceSubscriptionResource = new PresenceSubscriptionResource(httpUtility);
                JsonConvert.PopulateObject(presenceSubscriptionResourceString, presenceSubscriptionResource);
                return presenceSubscriptionResource;
            }
            else
                return null;
        }
    }
}
