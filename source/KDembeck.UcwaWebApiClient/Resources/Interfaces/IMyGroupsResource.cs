using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyGroupsResource : IResourceBase
    {
        MyGroupsLinks _links { get; set; }
        MyGroupsEmbedded _embedded { get; set; }
        List<DistributionGroupResource> distributionGroups { get; }
        List<GroupResource> groups { get; }
        GroupResource defaultGroup { get; }
        GroupResource delegatesGroup { get; }
        GroupResource delegatorsGroup { get; }
        GroupResource myOrganizationGroup { get; }
        GroupResource pinnedGroup { get; }
        Task<IMyGroupsResource> Get();
        new Task<IMyGroupsResource> Get(string resourceUrl);
        Task createNewContactGroup(string displayName);
        Task<IGroupResource> getDefaultGroup();
        Task<IGroupResource> getDelegatesGroup();
        Task<List<IDistributionGroupResource>> getDistributionGroups();
        Task<List<IGroupResource>> getGroups();
        Task<IGroupResource> getMyOrganizationGroup();
        Task<IGroupResource> getPinnedGroup();
    }

    public class MyGroupsLinks
    {
        public Link self;
        public Link defaultGroup;
        public Link delegatesGroup;
        public Link delegatorsGroup;
        public List<Link> distributionGroup;
        public List<Link> group;
        public Link myOrganizationGroup;
        public Link pinnedGroup;

        public MyGroupsLinks()
        {
            distributionGroup = new List<Link>();
            group = new List<Link>();
        }
    }

    public class MyGroupsEmbedded
    {
        public List<DistributionGroupResource> distributionGroup;
        public List<GroupResource> group;
        public GroupResource defaultGroup;
        public GroupResource delegatesGroup;
        public GroupResource delegatorsGroup;
        public GroupResource myOrganizationGroup;
        public GroupResource pinnedGroup;

        public MyGroupsEmbedded()
        {
            distributionGroup = new List<DistributionGroupResource>();
            group = new List<GroupResource>();
        }
    }
}
