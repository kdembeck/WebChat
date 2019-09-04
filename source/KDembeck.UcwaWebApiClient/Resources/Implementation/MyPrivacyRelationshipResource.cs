using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyPrivacyRelationshipResource : ResourceBase, IMyPrivacyRelationshipResource
    {
        public PrivacyRelationshipLevel? relationshipLevel { get; set; }
        public MyPrivacyRelationshipLinks _links { get; set; }

        public MyPrivacyRelationshipResource()
        {
            initializeProperties();
        }

        public MyPrivacyRelationshipResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new MyPrivacyRelationshipLinks();
            relationshipLevel = null;
        }

        //Get
        public async Task<IMyPrivacyRelationshipResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IMyPrivacyRelationshipResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        //Put updatePrivacyRelationship
        public async Task updatePrivacyRelationship(PrivacyRelationshipLevel relationshipLevel)
        {
            if (httpUtility != null && _links.self != null)
            {
                string updatePrivacyRelationshipJson = JsonConvert.SerializeObject(new { relationshipLevel = relationshipLevel }, new Newtonsoft.Json.Converters.StringEnumConverter());
                await httpUtility.httpPutJson(httpUtility.baseUrl + _links.self.href, updatePrivacyRelationshipJson);
            }
        }


        public async Task<List<IContactResource>> getContacts()
        {
            if (httpUtility != null && _links.contact.Count > 0)
            {
                List<IContactResource> contactList = new List<IContactResource>();
                foreach (Link contact in _links.contact)
                {
                    IContactResource newContactResource = new ContactResource(httpUtility);
                    await newContactResource.Get(httpUtility.baseUrl + contact.href);
                    contactList.Add(newContactResource);
                }
                return contactList;
            }
            else
                return null;
        }
    }
}
