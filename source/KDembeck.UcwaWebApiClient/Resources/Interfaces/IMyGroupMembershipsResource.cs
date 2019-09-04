using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyGroupMembershipsResource : IResourceBase
    {
        MyGroupMembershipsLinks _links { get; set; }
        MyGroupMembershipsEmbedded _embedded { get; set; }
        List<MyGroupMembershipResource> myGroupMembership { get; }
        Task<IMyGroupMembershipsResource> Get();
        new Task<IMyGroupMembershipsResource> Get(string resourceUrl);
        Task<IMyGroupMembershipsResource> getByGroupId(string groupId);
        Task<IMyGroupMembershipsResource> getByGroupId(string resourceUrl, string groupId);
        Task addContactToGroup(string contactUri, string groupId = null);
    }

    public class MyGroupMembershipsLinks
    {
        public Link self;
        public Link removeContactFromAllGroups;
    }

    public class MyGroupMembershipsEmbedded
    {
        public List<MyGroupMembershipResource> myGroupMembership;
        public MyGroupMembershipsEmbedded()
        {
            myGroupMembership = new List<MyGroupMembershipResource>();
        }
    }
}
