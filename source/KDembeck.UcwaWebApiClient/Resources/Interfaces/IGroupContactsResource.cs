using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IGroupContactsResource : IResourceBase
    {
        GroupContactsLinks _links { get; set; }
        GroupContactsEmbedded _embedded { get; set; }
        List<ContactResource> contacts { get; }
        void initializeResources();
        Task<IGroupContactsResource> Get();
        new Task<IGroupContactsResource> Get(string resourceUrl);
    }

    public class GroupContactsLinks
    {
        public Link self;
    }

    public class GroupContactsEmbedded
    {
        public List<ContactResource> contact;

        public GroupContactsEmbedded()
        {
            contact = new List<ContactResource>();
        }
    }
}
