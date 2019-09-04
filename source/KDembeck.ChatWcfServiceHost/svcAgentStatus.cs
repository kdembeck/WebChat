using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace KDembeck.ChatWcfServiceLibrary
{
    [DataContract]
    public class svcAgentStatus
    {
        [DataMember]
        public string agentDisplayName;

        [DataMember]
        public string agentSipUri;

        [DataMember]
        public string agentState;
    }
}
