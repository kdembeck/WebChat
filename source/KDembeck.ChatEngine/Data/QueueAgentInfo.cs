using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine.Data
{
    public class QueueAgentInfo
    {
        public string sipUri;
        public string displayName;
        public string tenantId;
        public DateTime lastSessionEnded;
        public DateTime lastSessionOffered;
        public int queuePriorityLevel;
        public bool locked;
    }
}
