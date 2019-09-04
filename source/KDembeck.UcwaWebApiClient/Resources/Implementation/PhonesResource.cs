using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PhonesResource : ResourceBase, IPhonesResource
    {
        public List<PhoneResource> phone { get { return _embedded.phone; } }
        public PhonesLinks _links { get; set; }
        public PhonesEmbedded _embedded { get; set; }

        public PhonesResource()
        {
            initializeProperties();
        }

        public PhonesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {   
            _links = new PhonesLinks();
            _embedded = new PhonesEmbedded();
        }

        //Get
        public async Task<IPhonesResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IPhonesResource> Get(string resourceUrl)
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
