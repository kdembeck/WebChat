using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public class ChatSessionQueue : IChatSessionQueue
    {
        //MAKE SURE WE'RE UNSUBSCRIBING OUR EVENTS

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<IChatSession> chatSessions { get; private set; }
        public bool draining { get; private set; }  
        public bool drained { get; private set; }       //I DON'T THINK WE NEED THIS AT ALL
        public int Count { get { return chatSessions.Count; } }
        public event EventHandler<EventArgs> ChatSessionQueueDrained;

        public ChatSessionQueue()
        {
            chatSessions = new List<IChatSession>();
            draining = false;
            drained = false;
        }

        public void Enqueue(IChatSession chatSession)
        {
            if (!draining)
                chatSessions.Add(chatSession);
        }

        public IChatSession Dequeue()
        {
            if (!draining)
            {
                IChatSession chatSession = chatSessions.OrderBy(x => x?.queuedTime).Where(x => x?.initialized == true).FirstOrDefault();
                if (chatSession != null)
                    chatSessions.RemoveAll(x => x.threadId == chatSession.threadId);
                return chatSession;
            }
            else
                return null;
        }

        public int Remove(string conversationGuidId)
        {
            //IChatSession chatSession = chatSessions.Where(x => x.conversationId == conversationGuidId).FirstOrDefault();
            //if (chatSession != null)
            //    chatSession.drain();        //THIS IS WRONG. DON'T USE DRAIN UNLESS WE'RE ACTUALLY SHUTTING DONW THE SYSTEM
            if (!draining)
                return chatSessions.RemoveAll(x => x.conversationId == conversationGuidId);
            else
                return 0;
        }

        public List<IChatSession> ToList()
        {
            if (!draining)
                return chatSessions.OrderBy(x => x.queuedTime).ToList();
            else
                return new List<IChatSession>();
        }

        public IChatSession Find(string conversationGuidId)
        {
            if (!draining)
                return chatSessions.Where(x => x.conversationId == conversationGuidId).FirstOrDefault();
            else
                return null;
        }

        public async Task Drain()
        {
            draining = true;
            if (chatSessions.Count > 0)
            {   
                foreach (IChatSession chatSession in chatSessions)
                {
                    await chatSession.drain();
                }
            }
            chatSessions.Clear();
            drained = true;
            ChatSessionQueueDrained?.Invoke(this, new EventArgs());
        }
    }
}
