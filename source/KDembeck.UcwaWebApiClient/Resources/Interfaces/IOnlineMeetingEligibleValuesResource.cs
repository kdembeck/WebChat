using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingEligibleValuesResource : IResourceBase
    {
        List<string> accessLevels { get; set; }
        List<string> automaticLeaderAssignments { get; set; }
        List<string> eligibleOnlineMeetingRels { get; set; }
        List<string> entryExitAnnouncements { get; set; }
        List<string> lobbyBypassForPhoneUsersSettings { get; set; }
        OnlineMeetingEligibleValuesLinks _links { get; set; }
        Task<IOnlineMeetingEligibleValuesResource> Get();
        new Task<IOnlineMeetingEligibleValuesResource> Get(string resourceUrl);
        Task<IMyAssignedOnlineMeetingResource> getMyAssignedOnlineMeeting();
        Task<IMyOnlineMeetingsResource> getMyOnlineMeetings();
    }

    public class OnlineMeetingEligibleValuesLinks
    {
        public Link self;
        public Link myAssignedOnlineMeeting;
        public Link myOnlineMeetings;
    }
}
