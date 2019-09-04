using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class DistributionGroupResource : ResourceBase, IDistributionGroupResource
    {
        public string uri { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public DistributionGroupLinks _links { get; set; }
        public DistributionGroupEmbedded _embedded { get; set; }
        public List<ContactResource> contacts { get { return _embedded.contact; } }
        public List<DistributionGroupResource> distributionGroup { get { return _embedded.distributionGroup; } }

        public DistributionGroupResource()
        {
            initializeProperties();
        }

        public DistributionGroupResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }    

        private void initializeProperties()
        {
            uri = null;
            id = null;
            name = null;
            _links = new DistributionGroupLinks();
            _embedded = new DistributionGroupEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.contact != null && _embedded.contact.Count > 0)
                {
                    foreach (ContactResource contactResource in _embedded.contact)
                    {
                        contactResource.httpUtility = httpUtility;
                    }
                }

                if (_embedded.distributionGroup != null && _embedded.distributionGroup.Count > 0)
                {
                    foreach (DistributionGroupResource distGroupResource in _embedded.distributionGroup)
                    {
                        distGroupResource.httpUtility = httpUtility;
                        distGroupResource.initializeResources();
                    }
                }
            }
        }
        
        public async Task<IDistributionGroupResource> Get()
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

        public new async Task<IDistributionGroupResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task addToContactList(string displayName, string smtpAddress)
        {
            if (httpUtility != null && _links.addToContactList != null)
            {
                string addToContactListJson = JsonConvert.SerializeObject(new
                {
                    displayName = displayName,
                    smtpAddress = smtpAddress
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addToContactList.href + "?displayName=" + displayName + "&smtpAddress=" + smtpAddress, addToContactListJson);
            }
        }

        public async Task<IDistributionGroupResource> expandDistributionGroup()
        {
            if (httpUtility != null && _links.expandDistributionGroup != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.expandDistributionGroup.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task removeFromContactList(string groupId)
        {
            if (httpUtility != null && _links.removeFromContactList != null)
            {
                string removeFromContactListJson = JsonConvert.SerializeObject(new
                {
                    groupId = groupId
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.removeFromContactList.href + "?groupId=" + groupId, removeFromContactListJson);
            }
        }

        public async Task<IPresenceSubscriptionResource> subscribeToGroupPresence(int duration, string groupId)
        {
            if (httpUtility != null && _links.subscribeToGroupPresence != null)
            {
                string subscribeToGroupPresenceJson = JsonConvert.SerializeObject(new
                {
                    duration = duration,
                    groupId = groupId
                });
                string presenceSubscriptionResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.subscribeToGroupPresence.href + "?duration=" + duration, subscribeToGroupPresenceJson);
                IPresenceSubscriptionResource presenceSubscriptionResource = new PresenceSubscriptionResource(httpUtility);
                JsonConvert.PopulateObject(presenceSubscriptionResourceString, presenceSubscriptionResource);
                return presenceSubscriptionResource;
            }
            else
                return null;
        }
    }
}
