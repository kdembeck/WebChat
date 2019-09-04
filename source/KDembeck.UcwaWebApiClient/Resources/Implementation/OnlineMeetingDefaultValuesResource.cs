using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingDefaultValuesResource : ResourceBase, IOnlineMeetingDefaultValuesResource
    {
        public OnlineMeetingDefaultValuesLinks _links { get; set; }
        public string accessLevel { get; set; }
        public string automaticLeaderAssignment { get; set; }
        public string defaultOnlineMeetingRel { get; set; }
        public string entryExitAnnouncement { get; set; }
        public string lobbyBypassForPhoneUsers { get; set; }
        public string participantsWarningThreshold { get; set; }

        public OnlineMeetingDefaultValuesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public OnlineMeetingDefaultValuesResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            accessLevel = null;
            automaticLeaderAssignment = null;
            defaultOnlineMeetingRel = null;
            entryExitAnnouncement = null;
            lobbyBypassForPhoneUsers = null;
            participantsWarningThreshold = null;
            _links = new OnlineMeetingDefaultValuesLinks();
        }

        public new async Task<IOnlineMeetingDefaultValuesResource> Get(string resourceUrl)
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

        public async Task<IOnlineMeetingDefaultValuesResource> Get()
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
    }
}
