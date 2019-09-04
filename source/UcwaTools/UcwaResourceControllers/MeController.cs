using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class MeController
    {
        private HttpHelper _httpHelper;

        public MeController(HttpHelper httpHelper)
        {
            _httpHelper = new HttpHelper();
            _httpHelper.ApplicationRootUri = httpHelper.ApplicationRootUri;
            _httpHelper.AuthenticationResult = httpHelper.AuthenticationResult;            
        }

        public async Task ReportMyActivity(string reportMyActivityUri)
        {   
            try
            {                
                string jsonPostData = "";               
                await _httpHelper.HttpPostAction(_httpHelper.ApplicationRootUri + reportMyActivityUri, jsonPostData);
            }
            catch (Exception ex) { throw ex; }
        }
        
        public async Task<string> Presence(string presenceUri)
        {   
            string availability = "";
            try
            {
                availability = await _httpHelper.HttpGetAction(_httpHelper.ApplicationRootUri + presenceUri);
            }
            catch (Exception ex) { throw ex; }
            return availability;
        }

        public async Task MakeMeAvailable(MakeMeAvailableInformation makeMeAvailableInformation, string makeMeAvailableUri)
        {
            try
            {
                await _httpHelper.HttpPostAction(_httpHelper.ApplicationRootUri + makeMeAvailableUri, JsonConvert.SerializeObject(makeMeAvailableInformation));
            }
            catch(Exception ex) { throw ex; }
        }
    }

    public struct MakeMeAvailableInformation
    {
        public string audioPreference;
        public string inactiveTimeout;
        public string phoneNumber;
        public string signInAs;
        public List<string> supportedMessagingFormats;
        public List<string> supportedModalities;
        public string voipFallbackToPhoneAudioTimeOut;
    }
}
