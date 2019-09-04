using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyContactsResource : IResourceBase
    {
        MyContactsLinks _links { get; set; }
        MyContactsEmbedded _embedded { get; set; }
        List<ContactResource> contacts { get; }
        Task<IMyContactsResource> Get();
        new Task<IMyContactsResource> Get(string resourceUrl);
        Task<IContactResource> getContact();
    }

    public class MyContactsLinks
    {
        public Link self;
        public Link contact;
    }

    public class MyContactsEmbedded
    {
        public List<ContactResource> contact;

        public MyContactsEmbedded()
        {
            contact = new List<ContactResource>();
        }
    }
}
