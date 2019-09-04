using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyContactsAndGroupsSubscriptionResource : IResourceBase
    {
        SubscriptionState? state { get; set; }
        MyContactsAndGroupsSubscriptionLinks _links { get; set; }
        Task<IMyContactsAndGroupsSubscriptionResource> Get();
        new Task<IMyContactsAndGroupsSubscriptionResource> Get(string resourceUrl);
        Task startOrRefreshSubscriptionToContactsAndGroups(int durationMinutes);
        Task stopSubscriptionToContactsAndGroups();
    }

    public class MyContactsAndGroupsSubscriptionLinks
    {
        public Link self;
        public Link startOrRefreshSubscriptionToContactsAndGroups;
        public Link stopSubscriptionToContactsAndGroups;
    }
}
