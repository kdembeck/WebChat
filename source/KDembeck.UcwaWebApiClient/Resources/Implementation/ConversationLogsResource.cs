using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ConversationLogsResource : ResourceBase, IConversationLogsResource
    {
        public ConversationLogsLinks _links { get; set; }

        public ConversationLogsResource()
        {
            initializeProperties();
        }

        public ConversationLogsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new ConversationLogsLinks();
        }

        public new async Task<IConversationLogsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationLogsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationLogsResource> Get(string resourceUrl, int limit)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl + "?limit=" + limit.ToString());
            }
            return this;
        }

        public async Task<IConversationLogsResource> Get(int limit)
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl + "?limit=" + limit.ToString());
            }
            return this;
        }

        public async Task<List<IConversationLogResource>> getConversationLogs()
        {
            if (httpUtility != null && _links.conversationLog != null && _links.conversationLog.Count > 0)
            {
                List<IConversationLogResource> conversationLogs = new List<IConversationLogResource>();
                foreach (Link conversationLogLink in _links.conversationLog)
                {
                    IConversationLogResource conversationLogResource = new ConversationLogResource(httpUtility);
                    await conversationLogResource.Get(httpUtility.baseUrl + conversationLogLink.href);
                    conversationLogs.Add(conversationLogResource);
                }
                return conversationLogs;
            }
            else
                return null;
        }
    }
}
