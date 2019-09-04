using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.Utilities;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace KDembeck.UcwaWebApiClient
{
    public class UcwaClient : IUcwaClient
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Timer reportMyActivityTimer;
        private IHttpUtility httpUtility;
        private IApplicationResource applicationResource;
        public IApplicationResource application { get { return applicationResource; } }
        public ClientState state { get; private set; }        
        private IEventHandler eventHandler;  
        public IEventHandler events { get { return eventHandler; } }

        public UcwaClient()
        {
            httpUtility = new HttpUtility(new HttpClient());
        }

        public async Task<bool> loginAs(string Username, string Password, string Tenant, string ClientId, string AutoDiscoverUrl, string LoginBaseUrl, string UserAgent, string EndpointId, string Culture, string instanceId,
            AudioPreference? audioPreference = null, TimeSpan? inactiveTimeout = null, string phoneNumber = null, PreferredAvailability? signInAs = null, List<MessageFormat> supportedMessagingFormats = null, List<ModalityType> supportedModalities = null, TimeSpan? voipFallbackToPhoneAudioTimeOut = null)
        {

            //trap for: 
            //  wrong username/password
            //  wrong application Id
            //  wrong tenant domain

            try
            {
                AuthenticationContext authenticationContext = new AuthenticationContext(LoginBaseUrl + "/" + Tenant);
                UserCredential applicationCredentials = new UserPasswordCredential(Username, Password);
                AutoDiscoverResource autoDiscoverResource = new AutoDiscoverResource(httpUtility);

                //log.Info("Test");

                try
                {
                    while (autoDiscoverResource._links.user == null || autoDiscoverResource._links.user.href == null)
                        await autoDiscoverResource.Get(AutoDiscoverUrl);
                }
                catch (Exception ex)
                {
                    throw;
                }                

                try
                {
                    httpUtility.authenticationResult = await authenticationContext.AcquireTokenAsync(httpUtility.getBaseUri(autoDiscoverResource._links.user.href), ClientId, applicationCredentials);
                    await autoDiscoverResource.Get(autoDiscoverResource._links.user.href);
                }
                catch (Exception ex)
                {
                    throw;
                }

                try
                {
                    while (autoDiscoverResource._links.redirect != null && autoDiscoverResource._links.redirect.href != null)
                    {
                        await autoDiscoverResource.Get(autoDiscoverResource._links.redirect.href);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }              

                try
                {
                    while (autoDiscoverResource._links.user == null)
                    {
                        //await autoDiscoverResource.Get(autoDiscoverResource._links.self.href);
                        autoDiscoverResource._links.user = autoDiscoverResource._links.self;
                    }
                    httpUtility.authenticationResult = await authenticationContext.AcquireTokenAsync(httpUtility.getBaseUri(autoDiscoverResource._links.user.href), ClientId, applicationCredentials);
                    while (autoDiscoverResource._links.applications.href == null)
                        await autoDiscoverResource.Get(autoDiscoverResource._links.user.href);
                }
                catch (Exception ex)
                {
                    throw;
                }

                try
                {
                    httpUtility.baseUrl = httpUtility.getBaseUri(autoDiscoverResource._links.applications.href);
                    httpUtility.authenticationResult = await authenticationContext.AcquireTokenAsync(httpUtility.baseUrl, ClientId, applicationCredentials);
                    
                    applicationResource = new ApplicationResource(httpUtility);
                    string jsonPostString = JsonConvert.SerializeObject(new { userAgent = UserAgent, endpointId = EndpointId, culture = Culture });
                    string applicationResourceString = await httpUtility.httpPostJson(autoDiscoverResource._links.applications.href, jsonPostString);
                    dynamic applicationResourceObject = JObject.Parse(applicationResourceString);
                    JsonConvert.PopulateObject(applicationResourceString, applicationResource);  
                    //applicationResource                  
                    startEventChannel();
                    await initializeResources();                    
                    await applicationResource.me.makeMeAvailable(audioPreference, inactiveTimeout, phoneNumber, signInAs, supportedMessagingFormats, supportedModalities, voipFallbackToPhoneAudioTimeOut);                    
                    startReportMyActivityTimer();

                    //wait for the next OnMeUpdatedEvent
                }
                catch (Exception ex)
                {
                    throw;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task logOut()
        {
            if (state == ClientState.LoggedIn)
            {
                if (application != null)
                    await application.signOut();
            }
        }

        public async Task<bool> testCredentials(string Username, string Password, string Tenant, string ClientId, string AutoDiscoverUrl, string LoginBaseUrl)
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(LoginBaseUrl + "/" + Tenant);
            UserCredential applicationCredentials = new UserPasswordCredential(Username, Password);
            AutoDiscoverResource autoDiscoverResource = new AutoDiscoverResource(httpUtility);
            AuthenticationResult authenticationResult;

            bool loginSuccess;
            
            while (autoDiscoverResource._links.user == null || autoDiscoverResource._links.user.href == null)
                await autoDiscoverResource.Get(AutoDiscoverUrl);
            
            try
            {
                authenticationResult = await authenticationContext.AcquireTokenAsync(httpUtility.getBaseUri(autoDiscoverResource._links.user.href), ClientId, applicationCredentials);                
                loginSuccess = true;
            }
            catch (Exception ex)
            {
                loginSuccess = false;
            }

            return loginSuccess;
        }

        private async Task initializeResources()
        {
            if (applicationResource.httpUtility == null)
                applicationResource.httpUtility = httpUtility;

            await applicationResource.initializeResources();

            if (applicationResource._embedded.communication != null)
            {
                eventHandler.OnCommunicationUpdated += applicationResource._embedded.communication.Handle_OnCommunicationUpdatedEvent;
            }

            if (applicationResource._embedded.me != null)
            {
                eventHandler.OnMeUpdatedEvent += Handle_OnMeUpdatedEvent;
            }            
        }

        private void startEventChannel()
        {
            eventHandler = new EventChannel.EventHandler(httpUtility);
            applicationResource.startEventChannel(eventHandler);            
        }

        private void startReportMyActivityTimer()
        {
            reportMyActivityTimer = new Timer(30000);
            reportMyActivityTimer.Elapsed += reportMyActivityTimer_Elapsed;
            reportMyActivityTimer.Start();
        }

        private async void reportMyActivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            reportMyActivityTimer.Stop();
            await applicationResource.me.reportMyActivity();                
            reportMyActivityTimer.Start();
        }

        private async void Handle_OnMeUpdatedEvent(object sender, EventArgs eventArgs)
        {
            await applicationResource._embedded.me.Get();
        }
    }
}
