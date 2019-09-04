using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PhoneDialInformationResource : ResourceBase, IPhoneDialInformationResource
    {
        public PhoneDialInformationLinks _links { get; set; }
        public PhoneDialInformationEmbedded _embedded { get; set; }
        public string conferenceId { get; set; }
        public string defaultRegion { get; set; }
        public string externalDirectoryUri { get; set; }
        public string internalDirectoryUri { get; set; }
        public string isAudioConferenceProviderEnabled { get; set; }
        public string participantPassCode { get; set; }
        public string tollFreeNumbers { get; set; }
        public string tollNumber { get; set; }
        public List<DialInRegionResource> dialInRegions { get { return _embedded.dialInRegion; } }

        public PhoneDialInformationResource(IHttpUtility HttpUtility)
        {
            httpUtility = httpUtility;
            initializeProperties();
        }

        public PhoneDialInformationResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new PhoneDialInformationLinks();
            _embedded = new PhoneDialInformationEmbedded();
            conferenceId = null;
            defaultRegion = null;
            externalDirectoryUri = null;
            internalDirectoryUri = null;
            isAudioConferenceProviderEnabled = null;
            participantPassCode = null;
            tollFreeNumbers = null;
            tollNumber = null;
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.dialInRegion != null && _embedded.dialInRegion.Count > 0)
                {
                    foreach (DialInRegionResource dialInRegion in _embedded.dialInRegion)
                    {
                        dialInRegion.httpUtility = httpUtility;
                    }
                }
            }
        }

        public new async Task<IPhoneDialInformationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {                
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IPhoneDialInformationResource> Get()
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
