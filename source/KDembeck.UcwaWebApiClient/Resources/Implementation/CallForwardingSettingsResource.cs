using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class CallForwardingSettingsResource : ResourceBase, ICallForwardingSettingsResource
    {
        public string activePeriod { get; set; }
        public string activeSetting { get; set; }
        public string unansweredCallHandling { get; set; }
        public CallForwardingSettingsLinks _links { get; set; }
        public CallForwardingSettingsEmbedded _embedded { get; set; }
        public IImmediateForwardSettingsResource immediateForwardSettings { get { return _embedded.immediateForwardSettings; } }
        public ISimultaneousRingSettingsResource simultaneousRingSettings { get { return _embedded.simultaneousRingSettings; } }
        public IUnansweredCallSettingsResource unansweredCallSettings { get { return _embedded.unansweredCallSettings; } }

        public CallForwardingSettingsResource()
        {
            initializeProperties();
        }

        public CallForwardingSettingsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            activePeriod = null;
            activeSetting = null;
            unansweredCallHandling = null;
            _links = new CallForwardingSettingsLinks();
            _embedded = new CallForwardingSettingsEmbedded();            
        }

        private void initializeResources()
        {            
            if (httpUtility != null)
            {
                if (_embedded.immediateForwardSettings != null)
                {
                    _embedded.immediateForwardSettings.httpUtility = httpUtility;                    
                }
                if (_embedded.simultaneousRingSettings != null)
                {
                    _embedded.simultaneousRingSettings.httpUtility = httpUtility;
                }
                if (_embedded.unansweredCallSettings != null)
                {
                    _embedded.unansweredCallSettings.httpUtility = httpUtility;
                }
            }
        }

        public new async Task<ICallForwardingSettingsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<ICallForwardingSettingsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }
    }
}
