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
    public class PeopleResource : ResourceBase, IPeopleResource
    {
        public PeopleLinks _links { get; set; }

        public PeopleResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }
        public PeopleResource()
        {
            initializeProperties();
        }

        private void initializeProperties()
        {  
            _links = new PeopleLinks();

        }

        public new async Task<IPeopleResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public async Task<IPeopleResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public async Task<IMyContactsAndGroupsSubscriptionResource> getMyContactsAndGroupsSubscription()
        {
            if (httpUtility != null && _links.myContactsAndGroupsSubscription != null)
            {
                IMyContactsAndGroupsSubscriptionResource myContactsAndGroupsSubscriptionResource = new MyContactsAndGroupsSubscriptionResource(httpUtility);
                await myContactsAndGroupsSubscriptionResource.Get(httpUtility.baseUrl + _links.myContactsAndGroupsSubscription.href);
                return myContactsAndGroupsSubscriptionResource;
            }
            else
                return null;
        }

        public async Task<IMyContactsResource> getMyContacts()
        {
            if (httpUtility != null && _links.myContacts != null)
            {
                IMyContactsResource myContactsResource = new MyContactsResource(httpUtility);
                await myContactsResource.Get(httpUtility.baseUrl + _links.myContacts.href);
                return myContactsResource;
            }
            else
                return null;
        }

        public async Task<IMyGroupMembershipsResource> getMyGroupMemberships()
        {
            if (httpUtility != null && _links.myGroupMemberships != null)
            {
                IMyGroupMembershipsResource myGroupMembershipsResource = new MyGroupMembershipsResource(httpUtility);
                await myGroupMembershipsResource.Get(httpUtility.baseUrl + _links.myGroupMemberships.href);
                return myGroupMembershipsResource;
            }
            else
                return null;
        }

        public async Task<IMyGroupsResource> getMyGroups()
        {
            if (httpUtility != null && _links.myGroups != null)
            {
                IMyGroupsResource myGroupsResource = new MyGroupsResource(httpUtility);
                await myGroupsResource.Get(httpUtility.baseUrl + _links.myGroups.href);
                return myGroupsResource;
            }
            else
                return null;
        }

        public async Task<IMyPrivacyRelationshipsResource> getMyPrivacyRelationships()
        {
            if (httpUtility != null && _links.myPrivacyRelationships != null)
            {
                IMyPrivacyRelationshipsResource myPrivacyRelationshipsResource = new MyPrivacyRelationshipsResource(httpUtility);
                await myPrivacyRelationshipsResource.Get(httpUtility.baseUrl + _links.myPrivacyRelationships.href);
                return myPrivacyRelationshipsResource;
            }
            else
                return null;
        }

        public async Task<IPresenceSubscriptionMembershipsResource> getPresenceSubscriptionMemberships()
        {
            if (httpUtility != null && _links.presenceSubscriptionMemberships != null)
            {
                IPresenceSubscriptionMembershipsResource presenceSubscriptionMemberships = new PresenceSubscriptionMembershipsResource(httpUtility);
                await presenceSubscriptionMemberships.Get(httpUtility.baseUrl + _links.presenceSubscriptionMemberships.href);
                return presenceSubscriptionMemberships;
            }
            else
                return null;
        }

        public async Task<IPresenceSubscriptionsResource> getPresenceSubscriptions()
        {
            if (httpUtility != null && _links.presenceSubscriptions != null)
            {
                IPresenceSubscriptionsResource presenceSubscriptionsResource = new PresenceSubscriptionsResource(httpUtility);
                await presenceSubscriptionsResource.Get(httpUtility.baseUrl + _links.presenceSubscriptions.href);
                return presenceSubscriptionsResource;
            }
            else
                return null;
        }

        public async Task<ISearchResource> search(string query = null, string mail = null, int? limit = null)
        {
            if (httpUtility != null && _links.search != null)
            {
                ISearchResource searchResource = new SearchResource(httpUtility);
                string queryParams="";
                if (query != null)
                    queryParams += "?query=" + query;
                if (mail != null)
                {
                    if (queryParams.Length > 0)                    
                        queryParams += "&mail=" + mail;                    
                    else
                        queryParams += "?mail=" + mail;
                }
                if (limit != null)
                {
                    if (queryParams.Length > 0)
                        queryParams += "&limit=" + limit;
                    else
                        queryParams += "?limit=" + limit;
                }
                //await searchResource.search(httpUtility.baseUrl + _links.search.href + queryParams);
                string searchResourceString = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.search.href + queryParams);
                JsonConvert.PopulateObject(searchResourceString, searchResource);
                searchResource.initializeResources();
                return searchResource;
            }
            else
                return null;
        }

        public async Task<ISubscribedContactsResource> getSubscribedContacts()
        {
            if (httpUtility != null && _links.subscribedContacts != null)
            {
                ISubscribedContactsResource subscribedContactsResource = new SubscribedContactsResource(httpUtility);
                await subscribedContactsResource.Get(httpUtility.baseUrl + _links.subscribedContacts.href);
                return subscribedContactsResource;
            }
            else
                return null;
        }
    }
}
