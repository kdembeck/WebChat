using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyPrivacyRelationshipsResource : ResourceBase, IMyPrivacyRelationshipsResource
    {
        public MyPrivacyRelationshipsLinks _links { get; set; }
        public MyPrivacyRelationshipsEmbedded _embedded { get; set; }
        public List<MyPrivacyRelationshipResource> myPrivacyRelationships { get { return _embedded.myPrivacyRelationship; } }

        public MyPrivacyRelationshipsResource()
        {
            initializeProperties();
        }

        public MyPrivacyRelationshipsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new MyPrivacyRelationshipsLinks();
            _embedded = new MyPrivacyRelationshipsEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null && _embedded.myPrivacyRelationship != null && _embedded.myPrivacyRelationship.Count > 0)
            {
                foreach (MyPrivacyRelationshipResource myPrivacyRelationshipResource in _embedded.myPrivacyRelationship)
                {
                    myPrivacyRelationshipResource.httpUtility = httpUtility;
                }
            }
        }

        public async Task<IMyPrivacyRelationshipsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public new async Task<IMyPrivacyRelationshipsResource> Get(string resourceUrl)
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
