using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPresenceSubscriptionsResource : IResourceBase
    {
        List<PresenceSubscriptionResource> presenceSubscriptions { get; }
        PresenceSubscriptionsLinks _links { get; set; }
        PresenceSubscriptionsEmbedded _embedded { get; set; }        
        Task<IPresenceSubscriptionsResource> Get();
        new Task<IPresenceSubscriptionsResource> Get(string resourceUrl);
        Task<IPresenceSubscriptionResource> newPresenceSubscription(int duration, List<string> uris);
    }

    public class PresenceSubscriptionsLinks
    {
        public Link self;
    }

    public class PresenceSubscriptionsEmbedded
    {
        public List<PresenceSubscriptionResource> presenceSubscription;

        public PresenceSubscriptionsEmbedded()
        {
            presenceSubscription = new List<PresenceSubscriptionResource>();
        }
    }
}
