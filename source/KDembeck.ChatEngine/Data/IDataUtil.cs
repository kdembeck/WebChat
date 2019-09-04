using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine.Data
{
    public interface IDataUtil
    {
        string getSystemConfigSettingValue(string settingName);
        List<SystemConfigInfo> getAllSystemConfigSettingsAndValued();
        List<TenantInfo> getAllTenants();
        List<QueueInfo> getAllQueuesForTenant(string tenantGuidId);
        List<QueueAgentInfo> getAllAgentsForQueue(string queueGuidId);
        List<TenantAgentInfo> getAllAgentsForTenant(string tenantGuidId);
        void unlockAllAgents();
        void unlockAgent(string agentSipUri);
        void lockAgent(string agentSipUri);
        List<TenantIdInfo> getTenantAndQueueIds();
    }
}
