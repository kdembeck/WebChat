using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class GroupContactsResource : ResourceBase, IGroupContactsResource
    {
        public GroupContactsLinks _links { get; set; }
        public GroupContactsEmbedded _embedded { get; set; }
        public List<ContactResource> contacts { get { return _embedded.contact; } }

        public GroupContactsResource()
        {
            initializeProperties();
        }

        public GroupContactsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new GroupContactsLinks();
            _embedded = new GroupContactsEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null && _embedded.contact != null && _embedded.contact.Count > 0)
            {
                foreach (ContactResource contact in _embedded.contact)
                {
                    contact.httpUtility = httpUtility;
                }
            }
        }

        public async Task<IGroupContactsResource> Get()
        {
            if (httpUtility != null && _links.self.href != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public new async Task<IGroupContactsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }
    }
}
