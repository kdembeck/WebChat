using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public interface IInstantMessagingUtility
    {
        Task sendInstantMessageToAgent(string agentSipUri, string subject, string messageText);
        void drain();
    }
}
