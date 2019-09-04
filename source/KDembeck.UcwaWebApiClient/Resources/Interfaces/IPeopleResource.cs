using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPeopleResource : IResourceBase
    {
        PeopleLinks _links { get; set; }
        new Task<IPeopleResource> Get(string resourceUrl);
        Task<IPeopleResource> Get();
        Task<IMyContactsAndGroupsSubscriptionResource> getMyContactsAndGroupsSubscription();
        Task<IMyContactsResource> getMyContacts();
        Task<IMyGroupMembershipsResource> getMyGroupMemberships();
        Task<IMyGroupsResource> getMyGroups();
        Task<IMyPrivacyRelationshipsResource> getMyPrivacyRelationships();
        Task<IPresenceSubscriptionMembershipsResource> getPresenceSubscriptionMemberships();
        Task<IPresenceSubscriptionsResource> getPresenceSubscriptions();
        Task<ISearchResource> search(string query = null, string mail = null, int? limit = null);
        Task<ISubscribedContactsResource> getSubscribedContacts();
    }

    public class PeopleLinks
    {
        public Link self;
        public Link myContactsAndGroupsSubscription;
        public Link myContacts;
        public Link myGroupMemberships;
        public Link myGroups;
        public Link myPrivacyRelationships;
        public Link presenceSubscriptionMemberships;
        public Link presenceSubscriptions;
        public Link search;
        public Link subscribedContacts;
    }
}
