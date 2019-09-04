using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine.Dashboard
{
    public class ChatSessionStatus
    {
        public string conversationId;        
        public string invitedAgentDisplayName;
        public string webUserName;
        public DateTime queuedTime;
        public DateTime messagingStartTime;
        public List<string> participantDisplayNames;

        public ChatSessionStatus()
        {
            participantDisplayNames = new List<string>();
        }
    }
}
