using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingPoliciesResource : ResourceBase, IOnlineMeetingPoliciesResource
    {
        public OnlineMeetingPoliciesLinks _links { get; set; }
        public string entryExitAnnouncement { get; set; }
        public string externalUserMeetingRecording { get; set; }
        public string meetingRecording { get; set; }
        public string meetingSize { get; set; }
        public string phoneUserAdmission { get; set; }
        public string voipAudio { get; set; }

        public OnlineMeetingPoliciesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public OnlineMeetingPoliciesResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new OnlineMeetingPoliciesLinks();
            entryExitAnnouncement = null;
            externalUserMeetingRecording = null;
            meetingRecording = null;
            meetingSize = null;
            phoneUserAdmission = null;
            voipAudio = null;
        }

        public async Task<IOnlineMeetingPoliciesResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IOnlineMeetingPoliciesResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
