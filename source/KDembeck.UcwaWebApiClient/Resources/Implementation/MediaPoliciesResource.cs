using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MediaPoliciesResource : ResourceBase, IMediaPoliciesResource
    {
        public string applciationSharingBitRate { get; set; }
        public string applicationSharingEncryption { get; set; }
        public string audioBitRate { get; set; }
        public string audioBypass { get; set; }
        public string audioBypassId { get; set; }
        public string audioVideoEncryption { get; set; }
        public string bandwidthControl { get; set; }
        public string externalAudioBypassMode { get; set; }
        public string fipsCompliantMedia { get; set; }
        public string highPerformanceApplicationSharingInOnlineMeeting { get; set; }
        public string internalAudioBypassMode { get; set; }
        public int? maximumApplicationSharingPort { get; set; }
        public int? maximumAudioPort { get; set; }
        public int? maximumVideoPort { get; set; }
        public string maximumVideoRateAllowed { get; set; }
        public int? minimumApplicationSharingPort { get; set; }
        public int? minimumAudioPort { get; set; }
        public int? minimumVideoPort { get; set; }
        public string multiViewJoin { get; set; }
        public string poorDeviceWarnings { get; set; }
        public string poorNetworkWarnings { get; set; }
        public string portRange { get; set; }
        public string qualityOfService { get; set; }
        public string totalReceivedVideoBitRateKB { get; set; }
        public string video { get; set; }
        public string videoBitRate { get; set; }

        public MediaPoliciesLinks _links { get; set; }

        private void initializeProperties()
        {
            _links = new MediaPoliciesLinks();
        }

        public MediaPoliciesResource()
        { }

        public MediaPoliciesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
        }

        public new async Task<IMediaPoliciesResource> Get(string ResourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(ResourceUrl);
            }
            return this;
        }

        public async Task<IMediaPoliciesResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
