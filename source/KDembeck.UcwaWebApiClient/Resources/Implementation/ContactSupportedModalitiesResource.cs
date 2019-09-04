using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ContactSupportedModalitiesResource : ResourceBase, IContactSupportedModalitiesResource
    {
        public List<ModalityType> modalities { get; set; }
        public ContactSupportedModalitiesLinks _links { get; set; }

        public ContactSupportedModalitiesResource()
        {
            initializeProperties();
        }

        public ContactSupportedModalitiesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            modalities = new List<ModalityType>();
            _links = new ContactSupportedModalitiesLinks();
        }

        public async Task<IContactSupportedModalitiesResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IContactSupportedModalitiesResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
