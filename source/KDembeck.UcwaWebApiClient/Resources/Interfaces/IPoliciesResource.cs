using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPoliciesResource : IResourceBase
    {        
        PoliciesLinks _links { get; set; }
        string allowDeviceContactsSync { get; set; }
        string audioOnlyOnWifi { get; set; }
        string callLogArchiving { get; set; }
        string customerExperienceImprovementProgram { get; set; }
        string emergencyDialMask { get; set; }
        string emergencyDialString { get; set; }
        List<string> emergencyNumbers { get; set; }
        string emergencyServiceDisclaimer { get; set; }
        string emoticons { get; set; }
        string encryptAppData { get; set; }
        string clientExchangeConnectivity { get; set; }
        string exchangeUnifiedMessaging { get; set; }
        string helpEnvironment { get; set; }
        string htmlMessaging { get; set; }
        int locationRefreshInterval { get; set; }
        string locationRequired { get; set; }
        string logging { get; set; }
        string loggingLevel { get; set; }
        string messageArchiving { get; set; }
        string messagingUrls { get; set; }
        string multiViewJoin { get; set; }
        string onlineFeedbackUrl { get; set; }
        string photos { get; set; }
        string saveCallLogs { get; set; }
        string saveCredentials { get; set; }
        string saveMessagingHistory { get; set; }
        string sendFeedbackUrl { get; set; }
        string sharingOnlyOnWifi { get; set; }
        string softwareQualityMetrics { get; set; }
        string telephonyMode { get; set; }
        string useLocationForE911Only { get; set; }
        string videoOnlyOnWifi { get; set; }
        string voicemailUri { get; set; }
        new Task<IPoliciesResource> Get(string resourceUrl);
        Task<IPoliciesResource> Get();
    }

    public class PoliciesLinks
    {
        public Link self;
    }
}
