using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine.Dashboard
{
    public class QueueStatus
    {
        public string tenantId;        
        public string queueId;
        public string queueDisplayName;
        public int numberOfWaitingSessions = 0;
        public int numberOfActiveSessions = 0;
        public int numberOfAbandonedSessions = 0;
        public int numberOfHandledSessions = 0;
        public int numberOfAgentsAvailable = 0;
        public int numberOfAgentsUnavailable = 0;
        public int numberOfAgentsOffline = 0;
        public int numberOfAgentsInSession = 0;
        public int numberOfAgentsInvitationPending = 0;
        public int numberOfAgentsPostSession = 0;
        public List<AgentStatus> agentStatuses;
        public List<ChatSessionStatus> activeChatSessionStatuses;
        public List<ChatSessionStatus> waitingChatSessionStatuses;

        public QueueStatus()
        {
            agentStatuses = new List<AgentStatus>();
            activeChatSessionStatuses = new List<ChatSessionStatus>();
            waitingChatSessionStatuses = new List<ChatSessionStatus>();
        }        
    }
}
