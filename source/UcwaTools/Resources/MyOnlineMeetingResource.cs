using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class MyOnlineMeetingResource
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string ResourceString { get; set; }

        public string accessLevel;
        public List<string> attendees;
        public string automaticLeaderAssignment;
        public string converenceId;
        public string description;
        public string entryExitAnnouncement;
        public string expirationTime;
        public string joinUrl;
        public List<string> leaders;
        public string legacyOnlineMeetingUri;
        public string lobbyBypassForPhoneUsers;
        public string onlineMeetingId;
        public string onlineMeetingRel;
        public string onlineMeetingUri;
        public string organizerUri;
        public string phoneUserAdmission;
        public string subject;
        public MyOnlineMeetingLinks _links;
        public MyOnlineMeetingEmbedded _embedded;

        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";
            try
            {
                ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + resourceUri);
                FillResourceValues(ResourceString);
            }
            catch (Exception ex) { }
            return ResourceString;
        }

        public void FillResourceValues(string resourceString)
        {
            JsonConvert.PopulateObject(resourceString, this);
        }
    }

    public struct MyOnlineMeetingLinks
    {
        Link self;
        Link onlineMeetingExtensions;
    }

    public struct MyOnlineMeetingEmbedded
    {
        List<OnlineMeetingExtensionResource> onlineMeetingExtension;
    }


}
