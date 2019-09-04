using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class GroupResource : ResourceBase, IGroupResource
    {
        public string id { get; set; }
        public string name { get; set; }
        public GroupResourceLinks _links { get; set; }

        public GroupResource()
        {
            initializeProperties();
        }

        public GroupResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            id = null;
            name = null;
            _links = new GroupResourceLinks();
        }

        //Get
        public async Task<IGroupResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IGroupResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        //Delete removeGroup
        public async Task removeGroup()
        {
            if (httpUtility != null && _links.self != null)
            {
                await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
        }

        //Put updateGroup
        public async Task updateGroup(string name, string id = null)
        {
            if (httpUtility != null && _links.self != null)
            {
                dynamic updateGroupSettings = new ExpandoObject();
                updateGroupSettings.name = name;
                if (id != null)
                    updateGroupSettings.id = id;
                string updateGroupJson = JsonConvert.SerializeObject(updateGroupSettings);
                await httpUtility.httpPutJson(httpUtility.baseUrl + _links.self.href, updateGroupJson);
            }
        }

        public async Task<IGroupContactsResource> getGroupContacts()
        {
            if (httpUtility != null && _links.groupContacts != null)
            {
                IGroupContactsResource groupContactsResource = new GroupContactsResource(httpUtility);
                await groupContactsResource.Get(httpUtility.baseUrl + _links.groupContacts.href);
                return groupContactsResource;
            }
            else
                return null;
        }

        public async Task<IPresenceSubscriptionResource> subscribeToGroupPresence(int duration, string groupId)
        {
            if (httpUtility != null && _links.subscribeToGroupPresence != null)
            {
                if (duration > 30)
                    duration = 30;
                else if (duration < 10)
                    duration = 10;

                string subscribeToGroupPresenceJson = JsonConvert.SerializeObject(new
                {
                    duration = duration,
                    groupId = groupId
                });
                string presenceSubscriptionString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.subscribeToGroupPresence.href + "?duration=" + duration, subscribeToGroupPresenceJson);
                IPresenceSubscriptionResource presenceSubscriptionResource = new PresenceSubscriptionResource(httpUtility);
                JsonConvert.PopulateObject(presenceSubscriptionString, presenceSubscriptionResource);
                return presenceSubscriptionResource;
            }
            return null;
        }

        public async Task<IPresenceSubscriptionResource> subscribeToGroupPresence(int duration)
        {
            if (httpUtility != null && _links.subscribeToGroupPresence != null)
            {
                if (duration > 30)
                    duration = 30;
                else if (duration < 10)
                    duration = 10;

                string subscribeToGroupPresenceJson = JsonConvert.SerializeObject(new
                {
                    duration = duration
                });
                string presenceSubscriptionString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.subscribeToGroupPresence.href + "?duration=" + duration, subscribeToGroupPresenceJson);
                IPresenceSubscriptionResource presenceSubscriptionResource = new PresenceSubscriptionResource(httpUtility);
                JsonConvert.PopulateObject(presenceSubscriptionString, presenceSubscriptionResource);
                return presenceSubscriptionResource;
            }
            return null;
        }
    }
}

