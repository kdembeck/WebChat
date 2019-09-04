using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PoliciesResource : ResourceBase, IPoliciesResource
    {
        public PoliciesLinks _links { get; set; }
        public string allowDeviceContactsSync { get; set; }
        public string audioOnlyOnWifi { get; set; }
        public string callLogArchiving { get; set; }
        public string customerExperienceImprovementProgram { get; set; }
        public string emergencyDialMask { get; set; }
        public string emergencyDialString { get; set; }
        public List<string> emergencyNumbers { get; set; }
        public string emergencyServiceDisclaimer { get; set; }
        public string emoticons { get; set; }
        public string encryptAppData { get; set; }
        public string clientExchangeConnectivity { get; set; }
        public string exchangeUnifiedMessaging { get; set; }
        public string helpEnvironment { get; set; }
        public string htmlMessaging { get; set; }
        public int locationRefreshInterval { get; set; }
        public string locationRequired { get; set; }
        public string logging { get; set; }
        public string loggingLevel { get; set; }
        public string messageArchiving { get; set; }
        public string messagingUrls { get; set; }
        public string multiViewJoin { get; set; }
        public string onlineFeedbackUrl { get; set; }
        public string photos { get; set; }
        public string saveCallLogs { get; set; }
        public string saveCredentials { get; set; }
        public string saveMessagingHistory { get; set; }
        public string sendFeedbackUrl { get; set; }
        public string sharingOnlyOnWifi { get; set; }
        public string softwareQualityMetrics { get; set; }
        public string telephonyMode { get; set; }
        public string useLocationForE911Only { get; set; }
        public string videoOnlyOnWifi { get; set; }
        public string voicemailUri { get; set; }

        public PoliciesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public PoliciesResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new PoliciesLinks();           
            allowDeviceContactsSync = null;
            audioOnlyOnWifi = null;
            callLogArchiving = null;
            customerExperienceImprovementProgram = null;
            emergencyDialMask = null;
            emergencyDialString = null;
            emergencyNumbers = new List<string>();
            emergencyServiceDisclaimer = null;
            emoticons = null;
            encryptAppData = null;
            clientExchangeConnectivity = null;
            exchangeUnifiedMessaging = null;
            helpEnvironment = null;
            htmlMessaging = null;
            locationRefreshInterval = 0;
            locationRequired = null;
            logging = null;
            loggingLevel = null;
            messageArchiving = null;
            messagingUrls = null;
            multiViewJoin = null;
            onlineFeedbackUrl = null;
            photos = null;
            saveCallLogs = null;
            saveCredentials = null;
            saveMessagingHistory = null;
            sendFeedbackUrl = null;
            sharingOnlyOnWifi = null;
            softwareQualityMetrics = null;
            telephonyMode = null;
            useLocationForE911Only = null;
            videoOnlyOnWifi = null;
            voicemailUri = null;
        }

        public new async Task<IPoliciesResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IPoliciesResource> Get()
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
