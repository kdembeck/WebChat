using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingResource : IResourceBase
    {
        string accessLevel { get; set; }
        string attendees { get; set; }
        string automaticLeaderAssignment { get; set; }
        string conferenceId { get; set; }
        string description { get; set; }
        string disclaimerBody { get; set; }
        string disclaimerTitle { get; set; }
        string entryExitAnnouncement { get; set; }
        string expirationTime { get; set; }
        string hostingNetwork { get; set; }
        string joinUrl { get; set; }
        string largeMeeting { get; set; }
        List<string> leaders { get; set; }
        string lobbyBypassForPhoneUsers { get; set; }
        string onlineMeetingId { get; set; }
        string onlineMeetingRel { get; set; }
        string onlineMeetingUri { get; set; }
        string organizerName { get; set; }
        string organizerUri { get; set; }
        string phoneUserAdmission { get; set; }
        string subject { get; set; }
        OnlineMeetingLinks _links { get; set; }
        OnlineMeetingEmbedded _embedded { get; set; }
        List<OnlineMeetingExtensionsResource> onlineMeetingExtension { get; }
        new Task<IOnlineMeetingResource> Get(string resourceUrl);
        Task<IOnlineMeetingResource> Get();
        Task<IConversationResource> conversation();
        Task<IContactResource> organizer();
        Task<IPhoneDialInformationResource> phoneDianInInformation();
        Task<IOnlineMeetingExtensionsResource> onlineMeetingExtensions();
    }

    public class OnlineMeetingLinks
    {
        public Link self;
        public Link conversation;
        public Link organizer;
        public Link phoneDialInInformation;
        public Link onlineMeetingExtensions;
    }

    public class OnlineMeetingEmbedded
    {
        public List<OnlineMeetingExtensionsResource> onlineMeetingExtension;

        public OnlineMeetingEmbedded()
        {
            onlineMeetingExtension = new List<OnlineMeetingExtensionsResource>();
        }
    }
}
