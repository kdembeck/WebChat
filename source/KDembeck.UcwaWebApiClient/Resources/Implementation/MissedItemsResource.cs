using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MissedItemsResource : ResourceBase, IMissedItemsResource
    {
        public int? conversationLogsCount { get; set; }
        public string conversationLogsNotifications { get; set; }
        public int? missedConversationsCount { get; set; }
        public int? unreadMissedConversationsCount { get; set; }
        public int? unreadVoicemailsCount { get; set; }
        public int? voiceMailsCount { get; set; }
        public MissedItemsLinks _links { get; set; }

        private void initializeProperties()
        {
            conversationLogsCount = null;
            conversationLogsNotifications = null;
            missedConversationsCount = null;
            unreadMissedConversationsCount = null;
            unreadVoicemailsCount = null;
            voiceMailsCount = null;
            _links = new MissedItemsLinks();
        }

        public MissedItemsResource()
        {
            initializeProperties();
        }

        public MissedItemsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IMissedItemsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IMissedItemsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
