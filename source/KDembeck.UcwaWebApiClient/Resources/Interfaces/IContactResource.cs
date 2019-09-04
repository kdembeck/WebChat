using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IContactResource
    {
        string company { get; set; }
        string department { get; set; }
        List<string> emailAddresses { get; set; }
        string homePhoneNumber { get; set; }
        string sourceNetworkIconUrl { get; set; }
        string mobilePhoneNumber { get; set; }
        string name { get; set; }
        string office { get; set; }
        string otherPhoneNumber { get; set; }
        string sourceNetwork { get; set; }
        string title { get; set; }
        string type { get; set; }
        string uri { get; set; }
        string workPhoneNumber { get; set; }
        ContactLinks _links { get; set; }
        Task<IContactResource> Get();
        Task<IContactResource> Get(string resourceUrl);
        Task<ILocationResource> getContactLocation();
        Task<INoteResource> getContactNote();
        Task<Stream> getContactPhoto();
        Task<IContactPresenceResource> getContactPresence();
        Task<IContactPrivacyRelationshipResource> getContactPrivacyRelationship();
        Task<IContactSupportedModalitiesResource> getContactSupportedModalities();
    }

    public class ContactLinks
    {
        public Link self;
        public Link contactLocation;
        public Link contactNote;
        public Link contactPhoto;
        public Link contactPresence;
        public Link contactPrivacyRelationship;
        public Link contactSupportedModalities;
    }
}
