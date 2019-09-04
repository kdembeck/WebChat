using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kdembeck.ChatData.DataEntities;

namespace Kdembeck.ChatData
{
    public interface IChatEngineData
    {
        void addTenant();
        void addQueueToTenant();
        void addAgentToQueue();
        void setTenantConfigValue();

        IEnumerable<Tenant> getAllTenants();
        IEnumerable<Queue> getAllQueuesForTenant(string tenantId);
        IEnumerable<Agent> getAllAgentsForQueue(string queueId);
        IEnumerable<Agent> getAllAgentsForTenant(string tenantId);
        //void getTenantConfig(string tenantId);

        void addSession();
        void updateSessionQueueHistory();
        void updateSessionInvitationHistory();
        void updateSessionMessageHistory();

    }
}
