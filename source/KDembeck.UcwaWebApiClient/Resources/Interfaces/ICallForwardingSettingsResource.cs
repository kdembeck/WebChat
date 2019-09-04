using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ICallForwardingSettingsResource
    {
        string activePeriod { get; set; }
        string activeSetting { get; set; }
        string unansweredCallHandling { get; set; }
        CallForwardingSettingsLinks _links { get; set; }
        CallForwardingSettingsEmbedded _embedded { get; set; }
        IImmediateForwardSettingsResource immediateForwardSettings { get; }
        ISimultaneousRingSettingsResource simultaneousRingSettings { get; }
        IUnansweredCallSettingsResource unansweredCallSettings { get; }
        Task<ICallForwardingSettingsResource> Get(string resourceUrl);
        Task<ICallForwardingSettingsResource> Get();
    }

    public class CallForwardingSettingsLinks
    {
        public Link self;
        public Link turnOffCallForwarding;
    }

    public class CallForwardingSettingsEmbedded
    {
        public IImmediateForwardSettingsResource immediateForwardSettings;
        public ISimultaneousRingSettingsResource simultaneousRingSettings;
        public IUnansweredCallSettingsResource unansweredCallSettings;

        public CallForwardingSettingsEmbedded()
        {
            immediateForwardSettings = new ImmediateForwardSettingsResource();
            simultaneousRingSettings = new SimultaneousRingSettingsResource();
            unansweredCallSettings = new UnansweredCallSettingsResource();
        }
    }
}
