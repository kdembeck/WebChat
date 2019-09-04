using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public interface IChatSessionQueue
    {
        event EventHandler<EventArgs> ChatSessionQueueDrained;

        bool draining { get; }
        bool drained { get; }
        int Count { get; }
        List<IChatSession> chatSessions { get; }
        void Enqueue(IChatSession chatSession);
        IChatSession Dequeue();
        int Remove(string conversationGuidId);
        List<IChatSession> ToList();
        IChatSession Find(string conversationGuidId);
        Task Drain();
    }
}
