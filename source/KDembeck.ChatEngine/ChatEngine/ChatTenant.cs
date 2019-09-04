using KDembeck.ChatEngine.Data;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using KDembeck.UcwaWebApiClient.EventChannel;
using KDembeck.ChatEngine.Dashboard;

namespace KDembeck.ChatEngine
{
    public class ChatTenant : IChatTenant
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                
        public event EventHandler<TenantStateChangedEventArgs> TenantStateChanged;
        public event EventHandler<TenantLoginFailedEventArgs> TenantLoginFailed;
        public event EventHandler<TenantInitializedEventArgs> InitializedEvent;

        public string tenantDomainName { get; private set; }
        public string tenantId { get; private set; }
        public bool initialized { get; private set; }
        //public bool loggedIn { get; private set; }       
        public TenantState state { get; private set; }
        public IChatAgentManager chatAgentManager { get; private set; }
        public IChatQueueManager chatQueueManager { get; private set; }
        public IChatCommunicationProxy chatCommunicationProxy { get; set; }
                
        private bool chatAgentManagerInitialized = false;
        private bool chatQueueManagerInitialized = false;
        private TenantInfo tenantInfo;
        private IDataUtil dataUtil;
        private IUcwaClient ucwaClient;
        
        public ChatTenant(TenantInfo tenantInfo, IDataUtil dataUtil)
        {
            this.tenantDomainName = tenantInfo.tenantDomain;
            this.tenantId = tenantInfo.guidId;
            this.dataUtil = dataUtil;
            this.tenantInfo = tenantInfo;            
            state = TenantState.Stopped;
            initialized = false;
            chatCommunicationProxy = new ChatCommunicationProxy();
            //loggedIn = false;
        }

        public async Task Start(string autoDiscoverServiceRootUrl, string microsoftLoginBaseUrl)
        {
            setTenantState(TenantState.Starting);
            log.Debug("Logging into tenant: " + tenantInfo.tenantDomain);
            try
            {
                await login(autoDiscoverServiceRootUrl, microsoftLoginBaseUrl);
            }
            catch (Exception ex)
            {
                setTenantState(TenantState.Stopping);
                await Drain();
                setTenantState(TenantState.Stopped);
                return;
            }
            log.Debug("Login succeeded for tenant: " + tenantInfo.tenantDomain + ". Beginning initialization of tenant.");
            try
            {
                initialize();
            }
            catch (Exception ex)
            {
                setTenantState(TenantState.Stopping);
                await Drain();
                setTenantState(TenantState.Stopped);
            }
            log.Debug("Initialization succeeded for tenant: " + tenantInfo.tenantDomain);
            setTenantState(TenantState.Started);
        }

        private void setTenantState(TenantState state)
        {
            this.state = state;
            TenantStateChanged?.Invoke(this, new TenantStateChangedEventArgs(tenantId, tenantDomainName, state));
        }

        private void initialize()
        {
            chatAgentManager = new ChatAgentManager(tenantId, dataUtil, ucwaClient);
            chatAgentManager.InitializedEvent += Handle_OnChatAgentManagerInitialized;
            chatAgentManager.initialize();
            
            chatQueueManager = new ChatQueueManager(tenantId, dataUtil, ucwaClient, chatAgentManager, chatCommunicationProxy);
            chatQueueManager.InitializedEvent += Handle_OnChatQueueManagerInitialized;
            chatQueueManager.initialize();
        }

        private async Task login(string autoDiscoverServiceRootUrl, string microsoftLoginBaseUrl)
        {
            if (ucwaClient == null)
                ucwaClient = new UcwaClient();

            List<MessageFormat> messageFormats = new List<MessageFormat>();
            messageFormats.Add(MessageFormat.Plain);
            messageFormats.Add(MessageFormat.Html);

            List<ModalityType> modalities = new List<ModalityType>();
            modalities.Add(ModalityType.Messaging);

            string endpointId = Guid.NewGuid().ToString();

            if (await ucwaClient.loginAs(tenantInfo.username, tenantInfo.password, tenantInfo.tenantDomain, tenantInfo.clientId,
                autoDiscoverServiceRootUrl, microsoftLoginBaseUrl, "Webchat Service", tenantInfo.instanceId, "en-US", null, null, null, null, PreferredAvailability.Online,
                messageFormats, modalities, null))
            {
                //loggedIn = true;
            }
            else
            {
                //couldn't log in. something went wrong. how to handle it? Login retry timer?
                //throw an exception up
                //loggedIn = false;
                log.Error("Tenant: " + tenantInfo.tenantDomain + ". Login failed for username: " + tenantInfo.username + " for tenant: " + tenantInfo.tenantDomain);
            }
        }

        public async Task Drain()
        {
            log.Info("Tenant: " + tenantDomainName + ". Draining tenant chat engine.");
            chatQueueManager.drain();
            chatAgentManager.drain();
            chatCommunicationProxy.drain();
        }       

        private void Handle_OnChatAgentManagerInitialized(object sender, ChatAgentManangerInitializedEventArgs e)
        {
            chatAgentManager.InitializedEvent -= Handle_OnChatAgentManagerInitialized;
            chatAgentManagerInitialized = true;
            if (chatQueueManagerInitialized && chatAgentManagerInitialized)
            {
                //raise event that tenant is initialized
                initialized = true;
                InitializedEvent?.Invoke(this, new TenantInitializedEventArgs(tenantId, tenantDomainName));
            }
        }

        private void Handle_OnChatQueueManagerInitialized(object sender, ChatQueueManagerInitializedEventArgs e)
        {
            chatQueueManager.InitializedEvent -= Handle_OnChatQueueManagerInitialized;
            chatQueueManagerInitialized = true;
            if (chatQueueManagerInitialized && chatAgentManagerInitialized)
            {
                //raise event that tenant is initialized
                initialized = true;
                InitializedEvent?.Invoke(this, new TenantInitializedEventArgs(tenantId, tenantDomainName));
            }
        }
    }
}
