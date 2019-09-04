using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IContactPrivacyRelationshipResource 
    {
        PrivacyRelationshipLevel? relationshipLevel { get; set; }
        ContactPrivacyRelationshipLinks _links { get; set; }
        Task<IContactPrivacyRelationshipResource> Get();
        Task<IContactPrivacyRelationshipResource> Get(string resourceUrl);
    }

    public class ContactPrivacyRelationshipLinks
    {
        public Link self;
    }
}
