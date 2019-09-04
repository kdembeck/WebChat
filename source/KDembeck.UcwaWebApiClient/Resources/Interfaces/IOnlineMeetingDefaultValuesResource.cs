using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingDefaultValuesResource : IResourceBase
    {
        OnlineMeetingDefaultValuesLinks _links { get; set; }
        string accessLevel { get; set; }
        string automaticLeaderAssignment { get; set; }
        string defaultOnlineMeetingRel { get; set; }
        string entryExitAnnouncement { get; set; }
        string lobbyBypassForPhoneUsers { get; set; }
        string participantsWarningThreshold { get; set; }
        new Task<IOnlineMeetingDefaultValuesResource> Get(string resourceUrl);
        Task<IOnlineMeetingDefaultValuesResource> Get();
    }

    public class OnlineMeetingDefaultValuesLinks
    {
        public Link self;
    }
}
