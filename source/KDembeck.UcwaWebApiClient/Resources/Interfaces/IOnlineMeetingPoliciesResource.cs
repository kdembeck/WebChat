using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingPoliciesResource : IResourceBase
    {
        OnlineMeetingPoliciesLinks _links { get; set; }
        string entryExitAnnouncement { get; set; }
        string externalUserMeetingRecording { get; set; }
        string meetingRecording { get; set; }
        string meetingSize { get; set; }
        string phoneUserAdmission { get; set; }
        string voipAudio { get; set; }
        Task<IOnlineMeetingPoliciesResource> Get();
        new Task<IOnlineMeetingPoliciesResource> Get(string resourceUrl);
    }

    public class OnlineMeetingPoliciesLinks
    {
        public Link self;
    }
}
