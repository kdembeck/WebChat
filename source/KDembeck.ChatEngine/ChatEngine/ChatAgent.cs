using System;
using System.Collections.Generic;
using KDembeck.ChatEngine.Data;
using KDembeck.UcwaWebApiClient.Resources;

namespace KDembeck.ChatEngine
{   
    public class ChatAgent : IChatAgent
    {
        //private IDataUtil dataUtil;

        public ChatAgent()
        {
            queueLevels = new Dictionary<string, int>();
            conversationIds = new List<string>();
        }

        public AgentState agentState { get; set; }
        public string sipUri { get; set; }
        public string displayName { get; set; }
        public DateTime lastSessionEnded { get; set; }
        public DateTime lastSessionOffered { get; set; }
        public Dictionary<string, int> queueLevels { get; set; }
        public IContactResource contact { get; set; }
        public IContactPresenceResource contactPresence { get; set; }
        public List<string> conversationIds { get; set; }
    }
}
