using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMissedItemsResource : IResourceBase
    {
        int? conversationLogsCount { get; set; }
        string conversationLogsNotifications { get; set; }
        int? missedConversationsCount { get; set; }
        int? unreadMissedConversationsCount { get; set; }
        int? unreadVoicemailsCount { get; set; }
        int? voiceMailsCount { get; set; }
        MissedItemsLinks _links { get; set; }
        new Task<IMissedItemsResource> Get(string resourceUrl);
        Task<IMissedItemsResource> Get();
    }

    public class MissedItemsLinks
    {
        public Link self;
    }
}
