using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IGroupResource : IResourceBase
    {
        string id { get; set; }
        string name { get; set; }
        GroupResourceLinks _links { get; set; }
        Task<IGroupResource> Get();
        new Task<IGroupResource> Get(string resourceUrl);
        Task removeGroup();
        Task updateGroup(string name, string id = null);
        Task<IGroupContactsResource> getGroupContacts();
        Task<IPresenceSubscriptionResource> subscribeToGroupPresence(int duration, string groupId);
        Task<IPresenceSubscriptionResource> subscribeToGroupPresence(int duration);
    }

    public class GroupResourceLinks
    {
        public Link self;
        public Link groupContacts;
        public Link subscribeToGroupPresence;
    }
}
