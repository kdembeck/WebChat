using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMyAssignedOnlineMeetingResource : IResourceBase
    {
        MyAssignedOnlineMeetingLinks _links { get; set; }
        MyAssignedOnlineMeetingEmbedded _embedded { get; set; }
        List<OnlineMeetingExtensionResource> onlineMeetingExtensions { get; }
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
        void initializeResources();
        Task<IMyAssignedOnlineMeetingResource> Get();
        new Task<IMyAssignedOnlineMeetingResource> Get(string resourceUrl);
        Task removeOnlineMeeting();
        Task<IMyAssignedOnlineMeetingResource> updateOnlineMeeting(string accessLevel = null, List<string> attendees = null, string automaticLeaderAssignment = null, string description = null, string entryExitAnnouncement = null, string expirationTime = null, List<string> leaders = null, string lobbyBypassForPhoneUsers = null, string phoneUserAdmission = null, string subject = null);
        Task<IOnlineMeetingExtensionsResource> getOnlineMeetingExtensions();
    }

    public class MyAssignedOnlineMeetingLinks
    {
        public Link self;
        public Link onlineMeetingExtensions;
    }

    public class MyAssignedOnlineMeetingEmbedded
    {
        public List<OnlineMeetingExtensionResource> onlineMeetingExtension;

        public MyAssignedOnlineMeetingEmbedded()
        {
            onlineMeetingExtension = new List<OnlineMeetingExtensionResource>();
        }
    }
}
