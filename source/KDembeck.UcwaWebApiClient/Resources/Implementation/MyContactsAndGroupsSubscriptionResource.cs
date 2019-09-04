using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyContactsAndGroupsSubscriptionResource : ResourceBase, IMyContactsAndGroupsSubscriptionResource
    {
        public SubscriptionState? state { get; set; }
        public MyContactsAndGroupsSubscriptionLinks _links { get; set; }

        public MyContactsAndGroupsSubscriptionResource()
        {
            initializeProperties();
        }

        public MyContactsAndGroupsSubscriptionResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            state = null;
            _links = new MyContactsAndGroupsSubscriptionLinks();
        }
        
        public async Task<IMyContactsAndGroupsSubscriptionResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IMyContactsAndGroupsSubscriptionResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task startOrRefreshSubscriptionToContactsAndGroups(int durationMinutes)
        {
            if (httpUtility != null && _links.startOrRefreshSubscriptionToContactsAndGroups != null)
            {
                if (durationMinutes > 60)
                    durationMinutes = 60;
                else if (durationMinutes < 10)
                    durationMinutes = 10;

                string durationJson = JsonConvert.SerializeObject(new
                {
                    duration = durationMinutes
                });

                string returnJson = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startOrRefreshSubscriptionToContactsAndGroups.href + "?duration=" + durationMinutes.ToString(), durationJson);

                JsonConvert.PopulateObject(returnJson, this);
            }
        }

        public async Task stopSubscriptionToContactsAndGroups()
        {
            if (httpUtility != null && _links.stopSubscriptionToContactsAndGroups != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.stopSubscriptionToContactsAndGroups.href);
            }
        }
    }
}
