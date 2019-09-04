using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyOnlineMeetingResource : IResourceBase
    {
        MyOnlineMeetingLinks _links { get; set; }
        MyOnlineMeetingEmbedded _embedded { get; set; }
        string accessLevel { get; set; }
        List<string> attendees { get; set; }
        string automaticLeaderAssignment { get; set; }
        string conferenceId { get; set; }
        string description { get; set; }
        string entryExitAnnouncement { get; set; }
        string expirationTime { get; set; }
        string joinUrl { get; set; }
        List<string> leaders { get; set; }
        string legacyOnlineMeetingUri { get; set; }
        string lobbyBypassForPhoneUsers { get; set; }
        string onlineMeetingId { get; set; }
        string onlineMeetingRel { get; set; }
        string onlineMeetingUri { get; set; }
        string organizerUri { get; set; }
        string phoneUserAdmission { get; set; }
        string subject { get; set; }
        List<OnlineMeetingExtensionResource> onlineMeetingExtensions { get; } 
        Task<IMyOnlineMeetingResource> Get();
        new Task<IMyOnlineMeetingResource> Get(string resourceUrl);
        Task Remove(string ResourceUri);
        Task<IMyOnlineMeetingResource> Update(string accessLevel = null, List<string> attendees = null, string automaticLeaderAssignment = null, string description = null, string entryExitAnnouncement = null, string expirationTime = null, List<string> leaders = null, string lobbyBypassForPhoneUsers = null, string phoneUserAdmission = null, string subject = null);
    }

    public class MyOnlineMeetingLinks
    {
        public Link self;
    }

    public class MyOnlineMeetingEmbedded
    {
        public List<OnlineMeetingExtensionResource> onlineMeetingExtensions;

        public MyOnlineMeetingEmbedded()
        {
            onlineMeetingExtensions = new List<OnlineMeetingExtensionResource>();
        }
    }
}
