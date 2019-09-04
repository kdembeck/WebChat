using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.ChatEngine.Data;
using KDembeck.ChatEngine.Dashboard;

namespace KDembeck.ChatEngine
{
    public class ChatEngine : IChatEngine
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event EventHandler<EventArgs> ChatEngineStarted;
        public event EventHandler<EventArgs> ChatEngineStopped;
        public event EventHandler<ChatTenantInitializedEventArgs> ChatTenantInitialized;
        public ChatEngineStatus status { get; private set; }

        private List<IChatTenant> tenantChatList;
        private IDataUtil dataUtil;
        private List<SystemConfigInfo> systemConfig; //this shouldn't be a class level property. just query it, use it, then discard it 
        private IServiceDashboard serviceDashboard;
        private IStatusDashboard statusDashboard;

        public IServiceDashboard ServiceDashboard
        {
            get
            {
                if (status == ChatEngineStatus.Started)
                    return serviceDashboard;
                else
                    return null;
            }
        }

        public IStatusDashboard StatusDashboard
        {
            get
            {
                if (status == ChatEngineStatus.Started)
                    return statusDashboard;
                else
                    return null;
            }
        }

        public ChatEngine()
        {
            dataUtil = new DataUtil();
            status = ChatEngineStatus.Stopped;
        }

        public async Task startEngine()
        {
            log.Info("Starting...");
            status = ChatEngineStatus.Starting;
            systemConfig = dataUtil.getAllSystemConfigSettingsAndValued();
            string autoDiscoveryServiceRootUrl = systemConfig.Where(x => x.settingName == "AutoDiscoverServiceRootUrl").Select( x => x.settingValue ).FirstOrDefault();
            string microsoftLoginBaseUrl = systemConfig.Where(x => x.settingName == "LoginBaseUrl").Select(x => x.settingValue).FirstOrDefault();

            tenantChatList = new List<IChatTenant>();
            List<TenantInfo> tenantInfos = dataUtil.getAllTenants();
            foreach (TenantInfo tenantInfo in tenantInfos)
            {
                log.Info("Tenant found: " + tenantInfo.tenantDomain);
                try
                {
                    ChatTenant tenantChat = new ChatTenant(tenantInfo, dataUtil);
                    tenantChat.TenantStateChanged += Handle_TenantStateChanged;
                    tenantChat.Start(autoDiscoveryServiceRootUrl, microsoftLoginBaseUrl);
                    tenantChatList.Add(tenantChat);
                }
                catch (Exception ex)
                {
                    log.Error("Error message: " + ex.Message + "; Inner exception message: " + ex.InnerException.Message);
                    log.Error("Failed to initialize tenant chat settings for: " + tenantInfo.tenantDomain);
                }
            }
            serviceDashboard = new ServiceDashboard(tenantChatList);
            statusDashboard = new StatusDashboard(tenantChatList);
            status = ChatEngineStatus.Started;
            ChatEngineStarted?.Invoke(this, new EventArgs());
        }

        public async Task stopEngine()
        {
            if (status == ChatEngineStatus.Started)
            {
                status = ChatEngineStatus.Stopping;
                foreach (IChatTenant tenant in tenantChatList)
                {
                    if (tenant.state != TenantState.Stopped)
                    {
                        await tenant.Drain();
                    }                    
                }
                tenantChatList.Clear();
                status = ChatEngineStatus.Stopped;
                ChatEngineStopped?.Invoke(this, new EventArgs());
            }
        }

        private void Handle_TenantStateChanged(object sender, TenantStateChangedEventArgs e)
        {
           
        }

        private void Handle_TenantInitializedEvent(object sender, TenantInitializedEventArgs e)
        {
            //subscribe our service dashboard to the communication proxy events of the tenant

        }

        //start a timer to check for database updates periodically for system config updates - do another one in the tenant to look for tenant, agent, queue config updates
        //put a date/time upated field in the config table so we can just query compare to the last time we pulled updated config settings
    }
}
