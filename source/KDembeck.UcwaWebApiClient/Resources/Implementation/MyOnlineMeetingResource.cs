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
    public class MyOnlineMeetingResource : ResourceBase, IMyOnlineMeetingResource
    {   
        public MyOnlineMeetingLinks _links { get; set; }
        public MyOnlineMeetingEmbedded _embedded { get; set; }
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
        public List<OnlineMeetingExtensionResource> onlineMeetingExtensions { get { return _embedded.onlineMeetingExtensions; } }

        public MyOnlineMeetingResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public MyOnlineMeetingResource()
        {            
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new MyOnlineMeetingLinks();
            _embedded = new MyOnlineMeetingEmbedded();
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
                if (_embedded.onlineMeetingExtensions != null && _embedded.onlineMeetingExtensions.Count > 0)
                {
                    foreach (OnlineMeetingExtensionResource onlineMeetingExtension in _embedded.onlineMeetingExtensions)
                    {
                        onlineMeetingExtension.httpUtility = httpUtility;
                    }
                }
            }
        }

        public async Task<IMyOnlineMeetingResource> Get()
        {
            if (httpUtility != null && _links.self.href != null)
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

        public new async Task<IMyOnlineMeetingResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
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

        public async Task Remove(string ResourceUri)
        {
            if (httpUtility != null && _links.self != null)
            {
                await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
            else
            {
                //raise an error
            }
        }

        public async Task<IMyOnlineMeetingResource> Update(string accessLevel = null, List<string> attendees = null, string automaticLeaderAssignment = null, string description = null, string entryExitAnnouncement = null, string expirationTime = null, List<string> leaders = null, string lobbyBypassForPhoneUsers = null, string phoneUserAdmission = null, string subject = null)
        {
            if (httpUtility != null && _links.self != null)
            {
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

                string myOnlineMeetingPutJson = JsonConvert.SerializeObject(myOnlineMeetingSettings);
                string myOnlineMeetingString = await httpUtility.httpPutJson(httpUtility.baseUrl + _links.self.href, myOnlineMeetingPutJson);

                initializeProperties();
                JsonConvert.PopulateObject(myOnlineMeetingString, this);
            }
            return this;
        }
    }
}
