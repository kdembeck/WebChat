using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPresenceSubscriptionMembershipResource : IResourceBase
    {
        PresenceSubscriptionMembershipLinks _links { get; set; }
        Task<IPresenceSubscriptionMembershipResource> Get();
        Task<IPresenceSubscriptionMembershipResource> Get(string resourceUrl);
        Task removeContactFromPresenceSubscription();
        Task<IContactResource> getContact();
        Task<IPresenceSubscriptionResource> getPresenceSubscription();
    }

    public class PresenceSubscriptionMembershipLinks
    {
        public Link self;
        public Link contact;
        public Link presenceSubscription;
    }
}
