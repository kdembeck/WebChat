using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public interface IChatQueueManager
    {
        event EventHandler<ChatQueueManagerInitializedEventArgs> InitializedEvent;
        List<IChatQueue> chatQueues { get; }
        string queueNewChatSession(string webUserName, string webUserEmail, string extendedData, string queueId);
        //void webUserLeftConversation(string conversationId, string queueId);
        void initialize();
        void drain();
    }

    public class ChatQueueManagerInitializedEventArgs : EventArgs
    {

    }
}
