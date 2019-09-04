using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine.Data
{
    public class TenantIdInfo
    {
        public string tenantName;
        public string tenantGuidId;
        public List<QueueIdInfo> tenantQueues;
    }
}
