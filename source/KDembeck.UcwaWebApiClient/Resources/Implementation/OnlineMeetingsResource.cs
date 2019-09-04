using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingsResource : ResourceBase, IOnlineMeetingsResource
    {   
        public OnlineMeetingsLinks _links { get; set; }

        public OnlineMeetingsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public OnlineMeetingsResource() 
        {
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new OnlineMeetingsLinks();
        }

        public async Task<IOnlineMeetingsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public new async Task<IOnlineMeetingsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }
        public async Task<IMyAssignedOnlineMeetingResource> getMyAssignedOnlineMeeting()
        {
            if (httpUtility != null && _links.myAssignedOnlineMeeting != null)
            {
                string myAssignedOnlineMeetingJson = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.myAssignedOnlineMeeting.href);
                IMyAssignedOnlineMeetingResource myAssignedOnlineMeetingResource = new MyAssignedOnlineMeetingResource(httpUtility);
                JsonConvert.PopulateObject(myAssignedOnlineMeetingJson, myAssignedOnlineMeetingResource);
                return myAssignedOnlineMeetingResource;
            }
            else
            {
                //raise an error
                return null;
            }
        }
        
        public async Task<IMyOnlineMeetingsResource> getMyOnlineMeetings()
        {
            if (httpUtility != null && _links.myOnlineMeetings != null)
            {
                //gets all scheduled online meetings
                string myOnlineMeetingsString = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.myOnlineMeetings.href);
                IMyOnlineMeetingsResource myOnlineMeetingsResource = new MyOnlineMeetingsResource(httpUtility);
                JsonConvert.PopulateObject(myOnlineMeetingsString, myOnlineMeetingsResource);
                return myOnlineMeetingsResource;
            }
            else
            {
                //raise an error
                return null;
            }
        }

        public async Task<IMyOnlineMeetingResource> createNewMyOnlineMeeting(AccessLevel? accessLevel = null, List<string> attendees = null, AutomaticLeaderAssignment? automaticLeaderAssignment = null, string description = null, EntryExitAnnouncement? entryExitAnnouncement = null, DateTime? expirationTime = null, List<string> leaders = null, LobbyBypassForPhoneUsers? lobbyBypassForPhoneUsers = null, PhoneUserAdmission? phoneUserAdmission = null, string subject = null)
        {
            if (httpUtility != null && _links.myOnlineMeetings != null)
            {
                //creates a new online meeting
                dynamic myOnlineMeetingSettings = new ExpandoObject();

                if (accessLevel != null)
                    myOnlineMeetingSettings.accessLevel = accessLevel;
                if (attendees != null)
                    myOnlineMeetingSettings.attendees = attendees;
                if (automaticLeaderAssignment != null)
                    myOnlineMeetingSettings.automaticLeaderAssignment = automaticLeaderAssignment;
                if (description != null)
                    myOnlineMeetingSettings.description = description;
                if (entryExitAnnouncement != null)
                    myOnlineMeetingSettings.entryExitAnnouncement = entryExitAnnouncement;
                if (expirationTime != null)
                    myOnlineMeetingSettings.expirationTime = expirationTime;
                if (leaders != null)
                    myOnlineMeetingSettings.leaders = leaders;
                if (lobbyBypassForPhoneUsers != null)
                    myOnlineMeetingSettings.lobbyBypassForPhoneUsers = lobbyBypassForPhoneUsers;
                if (phoneUserAdmission != null)
                    myOnlineMeetingSettings.phoneUserAdmission = phoneUserAdmission;
                if (subject != null)
                    myOnlineMeetingSettings.subject = subject;

                string myOnlineMeetingPostJson = JsonConvert.SerializeObject(myOnlineMeetingSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                string myOnlineMeetingString = await httpUtility.httpPutJson(httpUtility.baseUrl + _links.myOnlineMeetings.href, myOnlineMeetingPostJson);

                IMyOnlineMeetingResource myOnlineMeetingResource = new MyOnlineMeetingResource(httpUtility);
                JsonConvert.PopulateObject(myOnlineMeetingString, myOnlineMeetingResource);
                return myOnlineMeetingResource;
            }
            else
            {
                //raise an error
                return null;
            }
        }
        
        public async Task<IOnlineMeetingDefaultValuesResource> getOnlineMeetingDefaultValues()
        {
            if (httpUtility != null && _links.onlineMeetingDefaultValues != null)
            {
                string onlineMeetingsDefaultValuesJson = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.onlineMeetingDefaultValues.href);
                IOnlineMeetingDefaultValuesResource onlineMeetingDefaultValues = new OnlineMeetingDefaultValuesResource(httpUtility);
                JsonConvert.PopulateObject(onlineMeetingsDefaultValuesJson, onlineMeetingDefaultValues);
                return onlineMeetingDefaultValues;
            }
            else
            {
                //raise an error
                return null;
            }
        }

        public async Task<IOnlineMeetingEligibleValuesResource> getOnlineMeetingEligibleValues()
        {
            if (httpUtility != null && _links.onlineMeetingEligibleValues != null)
            {
                string onlineMeetingsEligibleValuesString = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.onlineMeetingEligibleValues.href);
                IOnlineMeetingEligibleValuesResource onlineMeetingsEligibleValuesResource = new OnlineMeetingEligibleValuesResource(httpUtility);
                JsonConvert.PopulateObject(onlineMeetingsEligibleValuesString, onlineMeetingsEligibleValuesResource);
                return onlineMeetingsEligibleValuesResource;
            }
            else
            {
                //raise an error
                return null;
            }
        }

        public async Task<IOnlineMeetingInvitationCustomizationResource> getOnlineMeetingInvitationCustomization()
        {
            if (httpUtility != null && _links.onlineMeetingInvitationCustomization != null)
            {
                string onlineMeetingInvitationCustomizationString = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.onlineMeetingInvitationCustomization.href);
                IOnlineMeetingInvitationCustomizationResource onlineMeetingInvitationCustomizationResource = new OnlineMeetingInvitationCustomizationResource(httpUtility);
                JsonConvert.PopulateObject(onlineMeetingInvitationCustomizationString, onlineMeetingInvitationCustomizationResource);
                return onlineMeetingInvitationCustomizationResource;
            }
            else
            {
                //raise an error
                return null;
            }
        }

        public async Task<IOnlineMeetingPoliciesResource> getOnlineMeetingPolicies()
        {
            if (httpUtility != null && _links.onlineMeetingPolicies != null)
            {
                string onlineMeetingPoliciesString = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.onlineMeetingPolicies.href);
                IOnlineMeetingPoliciesResource onlineMeetingPoliciesResource = new OnlineMeetingPoliciesResource(httpUtility);
                JsonConvert.PopulateObject(onlineMeetingPoliciesString, onlineMeetingPoliciesResource);
                return onlineMeetingPoliciesResource;
            }
            else
            {
                return null;
            }   
        }

        public async Task<IPhoneDialInformationResource> getPhoneDialInInformation()
        {
            if (httpUtility != null && _links.phoneDialInformation != null)
            {
                string phoneDialInInformationString = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.phoneDialInformation.href);
                IPhoneDialInformationResource phoneDialInformationResource = new PhoneDialInformationResource(httpUtility);
                JsonConvert.PopulateObject(phoneDialInInformationString, phoneDialInformationResource);
                return phoneDialInformationResource;
            }
            else
                return null;
        }
    }
}
