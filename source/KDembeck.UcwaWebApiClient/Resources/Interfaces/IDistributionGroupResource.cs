using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IDistributionGroupResource : IResourceBase
    {
        string uri { get; set; }
        string id { get; set; }
        string name { get; set; }
        DistributionGroupLinks _links { get; set; }
        DistributionGroupEmbedded _embedded { get; set; }
        List<ContactResource> contacts { get; }
        List<DistributionGroupResource> distributionGroup { get; }
        void initializeResources();
        Task<IDistributionGroupResource> Get();
        new Task<IDistributionGroupResource> Get(string resourceUrl);
        Task addToContactList(string displayName, string smtpAddress);
        Task<IDistributionGroupResource> expandDistributionGroup();
        Task removeFromContactList(string groupId);
        Task<IPresenceSubscriptionResource> subscribeToGroupPresence(int duration, string groupId);
    }

    public class DistributionGroupLinks
    {
        public Link self;
        public Link addToContactList;
        public Link expandDistributionGroup;
        public Link removeFromContactList;
        public Link subscribeToGroupPresence;
    }

    public class DistributionGroupEmbedded
    {
        public List<ContactResource> contact;
        public List<DistributionGroupResource> distributionGroup;

        public DistributionGroupEmbedded()
        {
            contact = new List<ContactResource>();
            distributionGroup = new List<DistributionGroupResource>();
        }
    }
}
