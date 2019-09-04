using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyPrivacyRelationshipsResource : IResourceBase
    {
        MyPrivacyRelationshipsLinks _links { get; set; }
        MyPrivacyRelationshipsEmbedded _embedded { get; set; }
        List<MyPrivacyRelationshipResource> myPrivacyRelationships { get; }
        Task<IMyPrivacyRelationshipsResource> Get();
        new Task<IMyPrivacyRelationshipsResource> Get(string resourceUrl);
    }

    public class MyPrivacyRelationshipsLinks
    {
        public Link self;
    }

    public class MyPrivacyRelationshipsEmbedded
    {
        public List<MyPrivacyRelationshipResource> myPrivacyRelationship;
        public MyPrivacyRelationshipsEmbedded()
        {
            myPrivacyRelationship = new List<MyPrivacyRelationshipResource>();
        }
    }
}
