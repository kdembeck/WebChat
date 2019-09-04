using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IConversationLogsResource : IResourceBase
    {
        ConversationLogsLinks _links { get; set; }
        new Task<IConversationLogsResource> Get(string resourceUrl);
        Task<IConversationLogsResource> Get();
        Task<IConversationLogsResource> Get(string resourceUrl, int limit);
        Task<IConversationLogsResource> Get(int limit);
        Task<List<IConversationLogResource>> getConversationLogs();
    }

    public class ConversationLogsLinks
    {
        public Link self;
        public List<Link> conversationLog;

        public ConversationLogsLinks()
        {
            conversationLog = new List<Link>();
        }
    }
}
