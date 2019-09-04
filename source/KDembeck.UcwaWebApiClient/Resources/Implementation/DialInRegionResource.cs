using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class DialInRegionResource : ResourceBase, IDialInRegionResource
    {
        public DialInRegionLinks _links { get; set; }
        public List<string> languages { get; set; }
        public string name { get; set; }
        public string number { get; set; }

        public DialInRegionResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public DialInRegionResource()
        {            
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new DialInRegionLinks();
            languages = new List<string>();
            name = null;
            number = null;
        }

        public async Task<IDialInRegionResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            else
            {
                //raise an error
            }
            return this;
        }

        public new async Task<IDialInRegionResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {                
                initializeProperties();
                await base.Get(resourceUrl);
            }
            else
            {
                //raise an error
            }
            return this;
        }
    }
}
