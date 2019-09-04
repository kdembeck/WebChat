using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyPrivacyRelationshipResource : IResourceBase
    {
        PrivacyRelationshipLevel? relationshipLevel { get; set; }
        MyPrivacyRelationshipLinks _links { get; set; }
        Task<IMyPrivacyRelationshipResource> Get();
        new Task<IMyPrivacyRelationshipResource> Get(string resourceUrl);
        Task updatePrivacyRelationship(PrivacyRelationshipLevel relationshipLevel);
        Task<List<IContactResource>> getContacts();
    }

    public class MyPrivacyRelationshipLinks
    {
        public Link self;
        public List<Link> contact;
        public MyPrivacyRelationshipLinks()
        {
            contact = new List<Link>();
        }
    }
}
