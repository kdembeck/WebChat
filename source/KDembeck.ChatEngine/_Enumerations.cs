using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public enum AgentState { Available, InvitationPending, InSession, PostSession, Unavailable, UnavailableInvitationDeclined, Offline };
    public enum ChatSessionState { Initializing, Waiting, InvitationPending, Active, PostSession, Ended, Draining };
    public enum QueueState { Initializing, Ready, Draining };
    public enum ChatAgentManagerState { Initializing, Ready, Draining };
    //public enum TenantState { Initializing, Ready, Draining };
}
