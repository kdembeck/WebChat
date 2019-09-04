using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyOnlineMeetingsResource : IResourceBase
    {
        MyOnlineMeetingsLinks _links { get; set; }
        MyOnlineMeetingsEmbedded _embedded { get; set; }
        List<MyAssignedOnlineMeetingResource> myAssignedOnlineMeetings { get; }
        List<MyOnlineMeetingResource> myOnlineMeeting { get; }
        new Task<IMyOnlineMeetingsResource> Get(string resourceUrl);
        Task<IMyOnlineMeetingsResource> Get();
    }

    public class MyOnlineMeetingsLinks
    {
        public Link self;
    }

    public class MyOnlineMeetingsEmbedded
    {
        public List<MyAssignedOnlineMeetingResource> myAssignedOnlineMeeting;
        public List<MyOnlineMeetingResource> myOnlineMeeting;

        public MyOnlineMeetingsEmbedded()
        {
            myAssignedOnlineMeeting = new List<MyAssignedOnlineMeetingResource>();
            myOnlineMeeting = new List<MyOnlineMeetingResource>();
        }
    }
}
