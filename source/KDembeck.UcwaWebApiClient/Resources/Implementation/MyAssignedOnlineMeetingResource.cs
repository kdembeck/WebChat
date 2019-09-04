using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyAssignedOnlineMeetingResource : ResourceBase, IMyAssignedOnlineMeetingResource
    {
        public MyAssignedOnlineMeetingLinks _links { get; set; }
        public MyAssignedOnlineMeetingEmbedded _embedded { get; set; }
        public List<OnlineMeetingExtensionResource> onlineMeetingExtensions { get { return _embedded.onlineMeetingExtension; } }

        public string accessLevel { get; set; }
        public List<string> attendees { get; set; }
        public string automaticLeaderAssignment { get; set; }
        public string conferenceId { get; set; }
        public string description { get; set; }
        public string entryExitAnnouncement { get; set; }
        public string expirationTime { get; set; }
        public string joinUrl { get; set; }
        public List<string> leaders { get; set; }
        public string legacyOnlineMeetingUri { get; set; }
        public string lobbyBypassForPhoneUsers { get; set; }
        public string onlineMeetingId { get; set; }
        public string onlineMeetingRel { get; set; }
        public string onlineMeetingUri { get; set; }
        public string organizerUri { get; set; }
        public string phoneUserAdmission { get; set; }
        public string subject { get; set; }

        public MyAssignedOnlineMeetingResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public MyAssignedOnlineMeetingResource()
        {
            initializeProperties();
        }

        public void initializeProperties()
        {
            _links = new MyAssignedOnlineMeetingLinks();
            _embedded = new MyAssignedOnlineMeetingEmbedded();
            accessLevel = null;
            attendees = new List<string>();
            automaticLeaderAssignment = null;
            conferenceId = null;
            description = null;
            entryExitAnnouncement = null;
            expirationTime = null;
            joinUrl = null;
            leaders = new List<string>();
            legacyOnlineMeetingUri = null;
            lobbyBypassForPhoneUsers = null;
            onlineMeetingId = null;
            onlineMeetingRel = null;
            onlineMeetingUri = null;
            organizerUri = null;
            phoneUserAdmission = null;
            subject = null;
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.onlineMeetingExtension != null && _embedded.onlineMeetingExtension.Count > 0)
                {
                    foreach (OnlineMeetingExtensionResource onlineMeetingExtension in _embedded.onlineMeetingExtension)
                    {
                        onlineMeetingExtension.httpUtility = httpUtility;
                    }
                }
            }
        }

        public async Task<IMyAssignedOnlineMeetingResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            else
            {
                //raise an error
            }
            return this;
        }

        public new async Task<IMyAssignedOnlineMeetingResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task removeOnlineMeeting()
        {
            if (httpUtility != null)
            {
                string deleteResult = await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
            else
            {
                //raise an error
            }
        }

        public async Task<IMyAssignedOnlineMeetingResource> updateOnlineMeeting(string accessLevel = null, List<string> attendees = null, string automaticLeaderAssignment = null, string description = null, string entryExitAnnouncement = null, string expirationTime = null, List<string> leaders = null, string lobbyBypassForPhoneUsers = null, string phoneUserAdmission = null, string subject = null)
        {
            if (httpUtility != null && _links.self != null)
            {
                dynamic myAssignedOnlineMeetingSettings = new ExpandoObject();

                if (accessLevel != null)
                    myAssignedOnlineMeetingSettings.accessLevel = accessLevel;
                if (attendees != null)
                    myAssignedOnlineMeetingSettings.attendees = attendees;
                if (automaticLeaderAssignment != null)
                    myAssignedOnlineMeetingSettings.automaticLeaderAssignment = automaticLeaderAssignment;
                if (description != null)
                    myAssignedOnlineMeetingSettings.description = description;
                if (entryExitAnnouncement != null)
                    myAssignedOnlineMeetingSettings.entryExitAnnouncement = entryExitAnnouncement;
                if (expirationTime != null)
                    myAssignedOnlineMeetingSettings.expirationTime = expirationTime;
                if (leaders != null)
                    myAssignedOnlineMeetingSettings.leaders = leaders;
                if (lobbyBypassForPhoneUsers != null)
                    myAssignedOnlineMeetingSettings.lobbyBypassForPhoneUsers = lobbyBypassForPhoneUsers;
                if (phoneUserAdmission != null)
                    myAssignedOnlineMeetingSettings.phoneUserAdmission = phoneUserAdmission;
                if (subject != null)
                    myAssignedOnlineMeetingSettings.subject = subject;

                string myAssignedOnlineMeetingPutJson = JsonConvert.SerializeObject(myAssignedOnlineMeetingSettings);
                string myAssignedOnlineMeetingString = await httpUtility.httpPutJson(httpUtility.baseUrl + _links.self.href, myAssignedOnlineMeetingPutJson);

                initializeProperties();
                JsonConvert.PopulateObject(myAssignedOnlineMeetingString, this);
            }
            return this;
        }

        public async Task<IOnlineMeetingExtensionsResource> getOnlineMeetingExtensions()
        {
            if (httpUtility != null && _links.onlineMeetingExtensions != null)
            {
                string onlineMeetingExtensionsJson = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.onlineMeetingExtensions.href);
                IOnlineMeetingExtensionsResource onlineMeetingExtensions = new OnlineMeetingExtensionsResource(httpUtility);
                JsonConvert.PopulateObject(onlineMeetingExtensionsJson, onlineMeetingExtensions);
                return onlineMeetingExtensions;
            }
            else
                return null;
        }
    }
}
