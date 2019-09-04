using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyContactsResource : ResourceBase, IMyContactsResource
    {
        public MyContactsLinks _links { get; set; }
        public MyContactsEmbedded _embedded { get; set; }
        public List<ContactResource> contacts { get { return _embedded.contact; } }

        public MyContactsResource()
        {
            initializeProperties();
        }

        public MyContactsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new MyContactsLinks();
            _embedded = new MyContactsEmbedded();
        }

        private void initializeResources()
        {
            if (httpUtility != null && _embedded.contact != null && _embedded.contact.Count > 0)
            {
                foreach (ContactResource contactResource in _embedded.contact)
                {
                    contactResource.httpUtility = httpUtility;                    
                }
            }
        }

        public async Task<IMyContactsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IMyContactsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IContactResource> getContact()
        {
            if (httpUtility != null && _links.contact != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.contact.href);
                return contactResource;
            }
            else return null;
        }
    }
}
