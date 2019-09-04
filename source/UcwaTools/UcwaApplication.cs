using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Timers;
using log4net.Config;
using UcwaTools.AppManagers;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public delegate void OnUcwaApplication_StartApplicationCompleted(bool LoginSuccess, string ApplicationId);
    public delegate void OnEventChannelEventReceived(string Sender, string eventObjectString);

    public class UcwaApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OnUcwaApplication_StartApplicationCompleted UcwaApplication_StartApplicationCompletedEvent;
        public OnEventChannelEventReceived OnEventChannelEventReceivedEvent;
        
        private HttpHelper httpHelper;
        private ApplicationResource applicationResource;
        private Timer Timer_ReportMyActivity;
        private EventChannelListener eventChannelListener;
        public EventChannelEventHandler events;

        private MeController meController;
        private CommunicationManager _communicationManager;
        private OnlineMeetingsManager _onlineMeetingsManager;     

        public CommunicationManager Communication { get { return _communicationManager; } }
        public OnlineMeetingsManager OnlineMeetings { get { return _onlineMeetingsManager; } }

        private string _applicationId;
        public string ApplicationId { get { return _applicationId; } }

        public UcwaApplication()
        {   
            XmlConfigurator.Configure();            
        }

        private async Task InitializeResources()
        {

            //await applicationsResource.GetResource(applicationsResource._links.self.href, httpHelper);

            if (_communicationManager == null)
            {
                _communicationManager = new CommunicationManager(httpHelper);
            }
            await _communicationManager.Update(applicationResource._embedded.communication._links.self.href);
            //events.OnMessagingInvitationStartedEvent += _communicationManager.Handle_OnMessagingInvitationStartedEvent;
            //events.OnMessagingInvitationCompletedEvent += _communicationManager.Handle_OnMessagingInvitationCompletedEvent;
            //events.OnIncomingMessageReceivedEvent += _communicationManager.Handle_OnIncomingMessageReceivedEvent;
            //events.OnConversatedDeletedEvent += _communicationManager.Handle_OnConversationDeletedEvent;
            //events.OnOnlineMeetingInvitationCompletedEvent += _communicationManager.Handle_OnOnlineMeetingInvitationCompletedEvent;
            //events.OnCommunicationConversationUpdatedEvent += _communicationManager.Handle_OnCommunicationConversationUpdatedEvent;

            if (_onlineMeetingsManager == null)
            {
                _onlineMeetingsManager = new OnlineMeetingsManager(httpHelper);
            }
            await _onlineMeetingsManager.Update(applicationResource._embedded.onlineMeetings._links.self.href);

        }

        public async Task<bool> StartApplication(AzureAdApplicationInformation azureAdApplicationInformation, UcwaApplicationInformation ucwaApplicationInformation, MakeMeAvailableInformation makeMeAvailableInformation, string applicationId=null)
        {
            bool startApplicationSuccess = false;
            try
            {
                log.Info("Starting application with application Id: " + applicationId);                
                log.Debug("Starting UCWA application with Azure AD application information: " + JsonConvert.SerializeObject(azureAdApplicationInformation));
                log.Debug("Starting UCWA application with UCWA application information: " + JsonConvert.SerializeObject(ucwaApplicationInformation));

                _applicationId = applicationId;
                httpHelper = new HttpHelper();
                
                string applicationsResourceUri = await AzureAdApplicationController.Login(azureAdApplicationInformation, httpHelper);

                log.Debug("Log in to Azure AD completed. Creating UCWA application.");

                if (applicationsResourceUri != null)
                {
                    string applicationResourceString = await CreateUcwaApplication(ucwaApplicationInformation, applicationsResourceUri);

                    applicationResource = new ApplicationResource();
                    applicationResource.FillResourceValues(applicationResourceString);

                    meController = new MeController(httpHelper);

                    log.Debug("Created UCWA Application. Starting event channel listener.");

                    StartEventChannel();

                    log.Debug("Event channel listener started. Calling MakeMeAvailable.");

                    await MakeMeAvailable(makeMeAvailableInformation);

                    log.Debug("MakeMeAvailable completed. Starting ReportMyActivity");

                    startReportMyActivityTimer();

                    log.Debug("Initializing resources.");

                    await InitializeResources();

                    log.Info("UCWA Application started.");

                    startApplicationSuccess = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
                throw ex;
            }
            return startApplicationSuccess;
        }

        #region Private StartApplication helper methods

        private void StartEventChannel()
        {
            try
            {
                string eventsUri = httpHelper.ApplicationRootUri + applicationResource._links.events.href;

                //do i need to be using a new http client here? 
                events = new EventChannelEventHandler(applicationResource, httpHelper);
                eventChannelListener = new EventChannelListener(httpHelper);

                eventChannelListener.OnEventChannelListenerEventReceivedEvent += events.Handle_OnEventChannelListenerEventReceivedEvent;
                eventChannelListener.OnEventChannelListenerEventReceivedEvent += HandleOnEventChannelEventReceived;
                eventChannelListener.Start(eventsUri);
            }
            catch (Exception ex) { throw ex; }
        }

        private async Task<bool> MakeMeAvailable(MakeMeAvailableInformation makeMeAvailableInformation)
        {
            bool success = false;

            if (applicationResource._embedded.me._links.makeMeAvailable.href != null)
            {
                try
                {
                    //MakeMeAvailableInformation makeMeAvailableInfo = new MakeMeAvailableInformation();
                    //makeMeAvailableInfo.signInAs = "Online";
                    //makeMeAvailableInfo.supportedMessagingFormats = new List<string>();
                    //makeMeAvailableInfo.supportedMessagingFormats.Add("Plain");
                    //makeMeAvailableInfo.supportedModalities = new List<string>();
                    //makeMeAvailableInfo.supportedModalities.Add("Messaging");

                    await meController.MakeMeAvailable(makeMeAvailableInformation, applicationResource._embedded.me._links.makeMeAvailable.href);

                    success = true;
                }
                catch (Exception ex) { }
            }

            return success;
        }

        private void startReportMyActivityTimer()
        {   
            Timer_ReportMyActivity = new Timer(30000);
            Timer_ReportMyActivity.Elapsed += Timer_ReportMyActivity_Elapsed;
            Timer_ReportMyActivity.Start();
        }

        private async void Timer_ReportMyActivity_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer_ReportMyActivity.Stop();
            if (!string.IsNullOrEmpty(applicationResource._embedded.me._links.reportMyActivity.href))
                try { await meController.ReportMyActivity(applicationResource._embedded.me._links.reportMyActivity.href); }
                catch(Exception ex) { throw ex; }
            Timer_ReportMyActivity.Start();
        }

        private async Task<string> CreateUcwaApplication(UcwaApplicationInformation applicationInformation, string applicationsResourceUri)
        {
            string applicationsResourceString = "";
            try
            {
                string jsonPostString = JsonConvert.SerializeObject(applicationInformation);
                applicationsResourceString = await httpHelper.HttpPostAction(applicationsResourceUri, jsonPostString);
            }
            catch (Exception ex) { }
            return applicationsResourceString;
        }

        #endregion

        private void HandleOnEventChannelEventReceived(string sender, string eventResourceString)
        {
            if (OnEventChannelEventReceivedEvent != null)
                OnEventChannelEventReceivedEvent.Invoke(sender, eventResourceString);
        }
    }

    public struct UcwaApplicationInformation
    {
        public string UserAgent;
        public string EndpointId;
        public string Culture;
    }
}
