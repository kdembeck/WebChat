using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMediaPoliciesResource : IResourceBase
    {
        string applciationSharingBitRate { get; set; }
        string applicationSharingEncryption { get; set; }
        string audioBitRate { get; set; }
        string audioBypass { get; set; }
        string audioBypassId { get; set; }
        string audioVideoEncryption { get; set; }
        string bandwidthControl { get; set; }
        string externalAudioBypassMode { get; set; }
        string fipsCompliantMedia { get; set; }
        string highPerformanceApplicationSharingInOnlineMeeting { get; set; }
        string internalAudioBypassMode { get; set; }
        int? maximumApplicationSharingPort { get; set; }
        int? maximumAudioPort { get; set; }
        int? maximumVideoPort { get; set; }
        string maximumVideoRateAllowed { get; set; }
        int? minimumApplicationSharingPort { get; set; }
        int? minimumAudioPort { get; set; }
        int? minimumVideoPort { get; set; }
        string multiViewJoin { get; set; }
        string poorDeviceWarnings { get; set; }
        string poorNetworkWarnings { get; set; }
        string portRange { get; set; }
        string qualityOfService { get; set; }
        string totalReceivedVideoBitRateKB { get; set; }
        string video { get; set; }
        string videoBitRate { get; set; }
        MediaPoliciesLinks _links { get; set; }
        new Task<IMediaPoliciesResource> Get(string ResourceUrl);
        Task<IMediaPoliciesResource> Get();
    }

    public class MediaPoliciesLinks
    {
        public Link self;
    }
}
