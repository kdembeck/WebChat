using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ISubscribedContactsResource : IResourceBase
    {
        SubscribedContactsLinks _links { get; set; }
        Task<ISubscribedContactsResource> Get();
        new Task<ISubscribedContactsResource> Get(string resourceUrl);
        Task<List<IContactResource>> getContacts();
    }

    public class SubscribedContactsLinks
    {
        public Link self;
        public List<Link> contact;

        public SubscribedContactsLinks()
        {
            contact = new List<Link>();
        }
    }
}
