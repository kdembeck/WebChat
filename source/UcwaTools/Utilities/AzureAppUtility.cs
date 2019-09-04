//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using UcwaTools;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace UcwaTools.Utilities
//{
//    public static class AzureAppUtility
//    {
//        public static AzureAdApplicationInformation ParseApplicationManifest(string ApplicationManifestJsonString)
//        {
//            AzureAdApplicationInformation azureAdInfo = new AzureAdApplicationInformation();

//            dynamic ApplicationManifestJsonObject = JObject.Parse(ApplicationManifestJsonString)

//            azureAdInfo.ClientId = ApplicationManifestJsonString.appId;
//            azureAdInfo.ResourceId = ApplicationManifestJsonString.requiredResourceAccess[1].resourceAppId;
//            azureAdInfo.Tenant

//            return azureAdInfo;
//        }
//    }

//    public class AzureApplicationManifestInfo
//    {
//        public string AppId { get; set; } //clientId
//        public string ResourceAppId { get; set; } //resourceId
//        public string ReplyUrl { get; set; } //redirectUri

//        public string TenantId()
//        {
//            string tenantId = "";

//            tenantId = ReplyUrl.Split('/').

//            return tenantId;        
//        }

//    }

//}
