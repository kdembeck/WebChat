using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ContactPrivacyRelationshipResource : ResourceBase, IContactPrivacyRelationshipResource
    {
        public PrivacyRelationshipLevel? relationshipLevel { get; set; }
        public ContactPrivacyRelationshipLinks _links { get; set; }

        public ContactPrivacyRelationshipResource()
        {
            initializeProperties();
        }

        public ContactPrivacyRelationshipResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            relationshipLevel = null;
            _links = new ContactPrivacyRelationshipLinks();
        }

        public async Task<IContactPrivacyRelationshipResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IContactPrivacyRelationshipResource> Get(string resourceUrl)
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
