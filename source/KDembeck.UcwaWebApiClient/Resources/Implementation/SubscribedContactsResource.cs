using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class SubscribedContactsResource : ResourceBase, ISubscribedContactsResource
    {
        public SubscribedContactsLinks _links { get; set; }

        public SubscribedContactsResource()
        {
            initializeProperties();
        }

        public SubscribedContactsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new SubscribedContactsLinks();
        }

        public async Task<ISubscribedContactsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<ISubscribedContactsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<List<IContactResource>> getContacts()
        {
            if (httpUtility != null && _links.contact.Count > 0)
            {
                List<IContactResource> contactResources = new List<IContactResource>();
                foreach (Link contactLink in _links.contact)
                {
                    IContactResource newContactResource = new ContactResource(httpUtility);
                    await newContactResource.Get(httpUtility.baseUrl + contactLink.href);
                    contactResources.Add(newContactResource);
                }
                return contactResources;
            }
            else
                return null;
        }
    }
}
