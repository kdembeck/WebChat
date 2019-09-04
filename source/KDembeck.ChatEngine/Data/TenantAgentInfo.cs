using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Resources;

namespace KDembeck.ChatEngine.Data
{
    public class TenantAgentInfo
    {
        public TenantAgentInfo()
        {
            queueLevels = new List<QueueLevel>();
        }

        public string sipUri;
        public string displayName;
        public DateTime lastSessionEnded;
        public DateTime lastSessionOffered;
        public List<QueueLevel> queueLevels;
        public bool locked;
        //public ContactResource contactResource;
    }

    public class QueueLevel
    {
        public string queueId;
        public int level;
    }
}
