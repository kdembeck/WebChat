using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine.Dashboard
{
    public interface IStatusDashboard
    {
        event EventHandler<QueueStatusUpdatedEventArgs> QueueStatusUpdated;

        Dictionary<string, string> getTenantsIdsAndDomainNames();
        Dictionary<string, string> getQueueIdsAndNamesForTenant(string tenantId);
        QueueStatus getQueueStatus(string queueId);
    }

    public class QueueStatusUpdatedEventArgs : EventArgs
    {
        string queueId;
        public QueueStatusUpdatedEventArgs(string queueId)
        {
            this.queueId = queueId;
        }
    }
}
