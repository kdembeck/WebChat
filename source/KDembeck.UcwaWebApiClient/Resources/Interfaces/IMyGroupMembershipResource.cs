using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyGroupMembershipResource : IResourceBase
    {
        MyGroupMembershipLinks _links { get; set; }
        Task<IMyGroupMembershipResource> Get();
        new Task<IMyGroupMembershipResource> Get(string resourceUrl);
        Task removeGroupMembership();
        Task<IContactResource> getContact();
        Task<IGroupResource> getDefaultGroup();
        Task<IGroupResource> getGroup();
        Task<IGroupResource> pinnedGroup();
    }

    public class MyGroupMembershipLinks
    {
        public Link self;
        public Link contact;
        public Link defaultGroup;
        public Link group;
        public Link pinnedGroup;
    }
}
