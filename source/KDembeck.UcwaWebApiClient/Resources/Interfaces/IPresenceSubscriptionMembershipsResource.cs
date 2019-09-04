using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPresenceSubscriptionMembershipsResource : IResourceBase
    {
        PresenceSubscriptionMembershipsLinks _links { get; set; }
        PresenceSubscriptionMembershipsEmbedded _embedded { get; set; }
        List<PresenceSubscriptionMembershipResource> presenceSubscriptionMemberships { get; }
        Task<IPresenceSubscriptionMembershipsResource> Get();
        new Task<IPresenceSubscriptionMembershipsResource> Get(string resourceUrl);
        Task<IPresenceSubscriptionMembershipsResource> subscribeToContacts(List<string> contactUris);
    }

    public class PresenceSubscriptionMembershipsLinks
    {
        public Link self;
    }

    public class PresenceSubscriptionMembershipsEmbedded
    {
        public List<PresenceSubscriptionMembershipResource> presenceSubscriptionMembership;

        public PresenceSubscriptionMembershipsEmbedded()
        {
            presenceSubscriptionMembership = new List<PresenceSubscriptionMembershipResource>();
        }
    }
}
