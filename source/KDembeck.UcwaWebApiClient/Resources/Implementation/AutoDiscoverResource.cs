using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class AutoDiscoverResource : ResourceBase, IAutoDiscoverResource
    {
        public AutoDiscoverLinks _links { get; set; }      

        public AutoDiscoverResource()
        {
            initializeProperties();
        }

        public AutoDiscoverResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new AutoDiscoverLinks();
        }

        public new async Task<IAutoDiscoverResource> Get(string ResourceUrl)
        {
            if (httpUtility != null)
            {
                _links = new AutoDiscoverLinks();
                await base.Get(ResourceUrl);
            }
            return this;
        }

        public async Task<IAutoDiscoverResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
