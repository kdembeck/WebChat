using System;
using System.Collections.Generic;
using KDembeck.UcwaWebApiClient.Resources;

namespace KDembeck.ChatEngine
{
    public interface IChatAgent
    {
        AgentState agentState { get; set; }
        string sipUri { get; set; }
        string displayName { get; set; }
        DateTime lastSessionEnded { get; set; }
        DateTime lastSessionOffered { get; set; }
        Dictionary<string, int> queueLevels { get; set; }
        IContactResource contact { get; set; }
        IContactPresenceResource contactPresence { get; set; }
        List<string> conversationIds { get; set; }
    }
}
