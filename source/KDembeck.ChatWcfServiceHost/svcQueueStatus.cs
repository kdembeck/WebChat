using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KDembeck.ChatWcfServiceLibrary
{
    [DataContract]
    public class svcQueueStatus
    {
        [DataMember]
        public string tenantId;

        [DataMember]
        public string queueId;

        [DataMember]
        public string queueDisplayName;

        [DataMember]
        public int numberOfWaitingSessions;

        [DataMember]
        public int numberOfActiveSessions;

        [DataMember]
        public int numberOfAbandonedSessions;

        [DataMember]
        public int numberOfHandledSessions;

        [DataMember]
        public int numberOfAgentsAvailable;

        [DataMember]
        public int numberOfAgentsUnavailable;

        [DataMember]
        public int numberOfAgentsOffline;

        [DataMember]
        public int numberOfAgentsInSession;

        [DataMember]
        public int numberOfAgentsInvitationPending;

        [DataMember]
        public int numberOfAgentsPostSession;

        [DataMember]
        public List<svcAgentStatus> agentStatuses;

        [DataMember]
        public List<svcChatSessionStatus> activeChatSessionStatuses;

        [DataMember]
        public List<svcChatSessionStatus> waitingChatSessionStatuses;
    }
}
