//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using System.Globalization;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;


////TODO: Send a POST to the makeMeAvailable URI. Fire up a timer to send POSTs to the reportMyActivity URI

//namespace UcwaTools
//{
//    public delegate void OnUcwaAppLoggedInEventHandler(bool loginSuccess);

//    public class UcwaApp
//    {   
//        public string Username;        
//        public string Password;        
//        public string ResourceId;        
//        public string ClientId;        
//        public string RedirectUri;        
//        public string Tenant;

//        public string AppId;

//        public string WelcomeMessage;

//        public EventChannelListener EventChannelListener;

//        public ApplicationResource ApplicationResource { get { return _applicationResource; } }

//        public OnUcwaAppLoggedInEventHandler OnUcwaAppLoggedInEvent;
        
//        private HttpHelper _httpHelper;        
//        private ApplicationResource _applicationResource;        
//        private string _applicationsResourceUri;
//        private bool isLoggedIn;
        
//        public UcwaApp(string resourceId, string clientId, string redirectUri, string tenant)
//        {
//            try
//            {
//                ResourceId = resourceId;
//                ClientId = clientId;
//                RedirectUri = redirectUri;
//                Tenant = tenant;
                
//                _httpHelper = new HttpHelper();                
//                AppId = Guid.NewGuid().ToString();
                
//            }
//            catch (Exception ex) { }
//        }

//        public async void Login(string username, string password)
//        {   
//            try
//            {
//                Username = username;
//                Password = password;

//                if (await DoAutoDiscovery())
//                {
//                    ApplicationInformation appInfo = new ApplicationInformation();
//                    appInfo.UserAgent = "WebChatAgent";
//                    appInfo.EndpointId = Guid.NewGuid().ToString();
//                    appInfo.Culture = "en-US";

//                    string applicationsResourceString = await CreateApplication(appInfo, _applicationsResourceUri);

//                    _applicationResource = new ApplicationResource(_httpHelper);
                    
//                    _applicationResource.OnApplicationInitializedEvent += OnApplicationInitialized;
//                    _applicationResource.FillResourceValues(applicationsResourceString);
//                    EventChannelListener = _applicationResource.EventChannelListener;
//                }
//            }
//            catch (Exception ex) { OnUcwaAppLoggedInEvent.Invoke(false); }            
//        }

//        private void OnApplicationInitialized()
//        {
//            try
//            {             
//                isLoggedIn = true;
//                OnUcwaAppLoggedInEvent.Invoke(true);
//            }
//            catch (Exception ex) { }
//        }

//        public async Task<string> CreateApplication(ApplicationInformation applicationInformation, string applicationsResourceUri)
//        {
//            string ResourceString = "";
//            try
//            {
//                string jsonPostString = JsonConvert.SerializeObject(applicationInformation);
//                ResourceString = await _httpHelper.HttpPostAction(applicationsResourceUri, jsonPostString);                
//            }
//            catch (Exception ex) { }
//            return ResourceString;
//        }

//        private async Task<bool> DoAutoDiscovery()
//        {   
//            try
//            {
//                string autoDiscoverResultString = await _httpHelper.HttpGetAction("https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root");                
//                dynamic autoDiscoverResultObject = JObject.Parse(autoDiscoverResultString);

//                if (autoDiscoverResultObject._links != null)
//                {
//                    if (autoDiscoverResultObject._links.user != null)
//                    {
//                        string rootOauthUserUri = autoDiscoverResultObject._links.user.href;

//                        AuthenticationContext authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/" + Tenant);
//                        UserCredential creds = new UserPasswordCredential(Username, Password);
                        
//                        _httpHelper.AuthenticationResult = await authenticationContext.AcquireTokenAsync(HttpHelper.GetBaseUri(rootOauthUserUri), ClientId, creds);
                        
//                        autoDiscoverResultString = await _httpHelper.HttpGetAction(rootOauthUserUri);
//                        autoDiscoverResultObject = JObject.Parse(autoDiscoverResultString);
                        
//                        if (autoDiscoverResultObject._links != null)
//                        {
//                            while (autoDiscoverResultObject._links.redirect != null)
//                            {
//                                string redirectUri = autoDiscoverResultObject._links.redirect.href;
//                                autoDiscoverResultString = await _httpHelper.HttpGetAction(redirectUri);
//                                autoDiscoverResultObject = JObject.Parse(autoDiscoverResultString);

//                                if (autoDiscoverResultObject._links != null)
//                                {
//                                    if (autoDiscoverResultObject._links.user != null)
//                                    {
//                                        rootOauthUserUri = autoDiscoverResultObject._links.user.href;
//                                        break;
//                                    }
//                                }
//                                else { break; }
//                            }
//                        }

//                        _httpHelper.AuthenticationResult = await authenticationContext.AcquireTokenAsync(HttpHelper.GetBaseUri(rootOauthUserUri), ClientId, creds);
//                        string rootOauthUserResultString = await _httpHelper.HttpGetAction(rootOauthUserUri);
//                        dynamic rootOauthUserResultObject = JObject.Parse(rootOauthUserResultString);

//                        if (rootOauthUserResultObject._links != null)
//                        {
//                            if (rootOauthUserResultObject._links.applications != null)
//                            {
//                                _applicationsResourceUri = rootOauthUserResultObject._links.applications.href;
//                            }
//                        }

//                        _httpHelper.ApplicationRootUri = HttpHelper.GetBaseUri(_applicationsResourceUri);
//                        _httpHelper.AuthenticationResult = await authenticationContext.AcquireTokenAsync(_httpHelper.ApplicationRootUri, ClientId, creds);
//                    }
//                }
//            }
//            catch (Exception ex) { throw ex; }
//            return true;
//        }
//    }
//}
