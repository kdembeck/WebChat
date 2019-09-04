using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace KDembeck.ChatWcfServiceLibrary
{
    [DataContract]
    public class svcChatSessionStatus
    {
        [DataMember]
        public string conversationId;

        [DataMember]
        public string invitedAgentDisplayName;

        [DataMember]
        public string webUserName;

        [DataMember]
        public DateTime queuedTime;

        [DataMember]
        public DateTime messagingStartTime;

        [DataMember]
        public List<string> participantDisplayNames;
    }
}
