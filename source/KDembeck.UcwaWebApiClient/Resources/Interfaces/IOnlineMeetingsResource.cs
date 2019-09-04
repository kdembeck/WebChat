using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingsResource : IResourceBase
    {
        OnlineMeetingsLinks _links { get; set; }
        Task<IOnlineMeetingsResource> Get();
        new Task<IOnlineMeetingsResource> Get(string resourceUrl);
        Task<IMyAssignedOnlineMeetingResource> getMyAssignedOnlineMeeting();
        Task<IMyOnlineMeetingsResource> getMyOnlineMeetings();
        Task<IMyOnlineMeetingResource> createNewMyOnlineMeeting(AccessLevel? accessLevel = null, List<string> attendees = null, AutomaticLeaderAssignment? automaticLeaderAssignment = null, string description = null, EntryExitAnnouncement? entryExitAnnouncement = null, DateTime? expirationTime = null, List<string> leaders = null, LobbyBypassForPhoneUsers? lobbyBypassForPhoneUsers = null, PhoneUserAdmission? phoneUserAdmission = null, string subject = null);
        Task<IOnlineMeetingDefaultValuesResource> getOnlineMeetingDefaultValues();
        Task<IOnlineMeetingEligibleValuesResource> getOnlineMeetingEligibleValues();
        Task<IOnlineMeetingInvitationCustomizationResource> getOnlineMeetingInvitationCustomization();
        Task<IOnlineMeetingPoliciesResource> getOnlineMeetingPolicies();
        Task<IPhoneDialInformationResource> getPhoneDialInInformation();
    }

    public class OnlineMeetingsLinks
    {
        public Link self;
        public Link myAssignedOnlineMeeting;
        public Link myOnlineMeetings;
        public Link onlineMeetingDefaultValues;
        public Link onlineMeetingEligibleValues;
        public Link onlineMeetingInvitationCustomization;
        public Link onlineMeetingPolicies;
        public Link phoneDialInformation;
    }
}
