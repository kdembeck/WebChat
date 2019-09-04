using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingEligibleValuesResource : ResourceBase, IOnlineMeetingEligibleValuesResource
    {
        public List<string> accessLevels { get; set; }
        public List<string> automaticLeaderAssignments { get; set; }
        public List<string> eligibleOnlineMeetingRels { get; set; }
        public List<string> entryExitAnnouncements { get; set; }
        public List<string> lobbyBypassForPhoneUsersSettings { get; set; }
        public OnlineMeetingEligibleValuesLinks _links { get; set; }

        public OnlineMeetingEligibleValuesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public OnlineMeetingEligibleValuesResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            accessLevels = new List<string>();
            automaticLeaderAssignments = new List<string>();
            eligibleOnlineMeetingRels = new List<string>();
            entryExitAnnouncements = new List<string>();
            lobbyBypassForPhoneUsersSettings = new List<string>();
            _links = new OnlineMeetingEligibleValuesLinks();
        }

        public async Task<IOnlineMeetingEligibleValuesResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            else
            {
                //raise an error
            }
            return this;
        }

        public new async Task<IOnlineMeetingEligibleValuesResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            else
            {
                //raise an error
            }
            return this;
        }
                
        public async Task<IMyAssignedOnlineMeetingResource> getMyAssignedOnlineMeeting()
        {
            if (httpUtility != null && _links.myAssignedOnlineMeeting != null)
            {
                IMyAssignedOnlineMeetingResource myAssignedOnlineMeetingResource = new MyAssignedOnlineMeetingResource(httpUtility);
                await myAssignedOnlineMeetingResource.Get(httpUtility.baseUrl + _links.myAssignedOnlineMeeting.href);
                return myAssignedOnlineMeetingResource;
            }
            else return null;
        }
                
        public async Task<IMyOnlineMeetingsResource> getMyOnlineMeetings()
        {
            if (httpUtility != null && _links.myOnlineMeetings != null)
            {
                IMyOnlineMeetingsResource myOnlineMeetingsResource = new MyOnlineMeetingsResource(httpUtility);
                await myOnlineMeetingsResource.Get(httpUtility.baseUrl + _links.myOnlineMeetings.href);
                return myOnlineMeetingsResource;
            }
            else return null;
        }

    }
}
