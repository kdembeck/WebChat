using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyGroupMembershipResource : ResourceBase, IMyGroupMembershipResource
    {
        public MyGroupMembershipLinks _links { get; set; }

        public MyGroupMembershipResource()
        {
            initializeProperties();
        }

        public MyGroupMembershipResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new MyGroupMembershipLinks();
        }

        public async Task<IMyGroupMembershipResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IMyGroupMembershipResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        //Delete 
        public async Task removeGroupMembership()
        {
            if (httpUtility != null && _links.self != null)
            {
                await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
        }

        public async Task<IContactResource> getContact()
        {
            if (httpUtility != null && _links.contact != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.contact.href);
                return contactResource;
            }
            else
                return null;
        }

        public async Task<IGroupResource> getDefaultGroup()
        {
            if (httpUtility != null && _links.defaultGroup != null)
            {
                IGroupResource defaultGroupResource = new GroupResource(httpUtility);
                await defaultGroupResource.Get(httpUtility.baseUrl + _links.defaultGroup.href);
                return defaultGroupResource;
            }
            else
                return null;
        }

        public async Task<IGroupResource> getGroup()
        {
            if (httpUtility != null && _links.group != null)
            {
                IGroupResource groupResource = new GroupResource(httpUtility);
                await groupResource.Get(httpUtility.baseUrl + _links.group.href);
                return groupResource;
            }
            else
                return null;
        }

        public async Task<IGroupResource> pinnedGroup()
        {
            if (httpUtility != null && _links.pinnedGroup != null)
            {
                IGroupResource pinnedGroupResource = new GroupResource(httpUtility);
                await pinnedGroupResource.Get(httpUtility.baseUrl + _links.pinnedGroup.href);
                return pinnedGroupResource;
            }
            else
                return null;
        }
    }
}
