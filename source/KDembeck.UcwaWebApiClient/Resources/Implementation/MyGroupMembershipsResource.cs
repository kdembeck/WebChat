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
    public class MyGroupMembershipsResource : ResourceBase, IMyGroupMembershipsResource
    {
        public MyGroupMembershipsLinks _links { get; set; }
        public MyGroupMembershipsEmbedded _embedded { get; set; }
        public List<MyGroupMembershipResource> myGroupMembership { get { return _embedded.myGroupMembership; } }

        public MyGroupMembershipsResource()
        {
            initializeProperties();
        }

        public MyGroupMembershipsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new MyGroupMembershipsLinks();
            _embedded = new MyGroupMembershipsEmbedded();
        }

        //Get
        public async Task<IMyGroupMembershipsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IMyGroupMembershipsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IMyGroupMembershipsResource> getByGroupId(string groupId)
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl + "?groupId=" + groupId);
            }
            return this;
        }

        public async Task<IMyGroupMembershipsResource> getByGroupId(string resourceUrl, string groupId)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl + "?groupId=" + groupId);
            }
            return this;
        }

        //Post add contact uri to a group
        public async Task addContactToGroup(string contactUri, string groupId = null)
        {
            if (httpUtility != null && _links.self != null)
            {
                dynamic addContactToGroupSettings = new ExpandoObject();
                addContactToGroupSettings.contactUri = contactUri;
                string queryParams = "?contactUri=" + contactUri;
                if (groupId != null)
                {
                    addContactToGroupSettings.groupId = groupId;
                    queryParams += "&groupId=" + groupId;
                }
                string addContactToGroupJson = JsonConvert.SerializeObject(addContactToGroupSettings);
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href + queryParams, addContactToGroupJson);
            }
        }
    }
}
