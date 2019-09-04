using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPresenceSubscriptionResource : IResourceBase
    {
        string id { get; set; }
        PresenceSubscriptionLinks _links { get; set; }
        Task<IPresenceSubscriptionResource> Get();
        new Task<IPresenceSubscriptionResource> Get(string resourceUrl);
        Task<IPresenceSubscriptionResource> extendPresenceSubscription(int duration);
        Task deletePresenceSubscription();
        Task<IPresenceSubscriptionMembershipsResource> addToPresencesSubscription(List<string> contactUris);
        Task<IPresenceSubscriptionMembershipsResource> getMemberships();
    }

    public class PresenceSubscriptionLinks
    {
        public Link self;
        public Link addToPresenceSubscription;
        public Link memberships;
    }
}
