using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources 
{
    public class ConversationsResource : ResourceBase, IConversationsResource
    {
        public ConversationsLinks _links { get; set; }

        public ConversationsResource()
        {
            initializeProperties();
        }

        public ConversationsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new ConversationsLinks();
        }
        public new async Task<IConversationsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<List<IConversationResource>> getConversations()
        {
            if (httpUtility != null && _links.conversation != null && _links.conversation.Count > 0)
            {
                List<IConversationResource> conversations = new List<IConversationResource>();
                foreach (Link conversationLink in _links.conversation)
                {
                    IConversationResource newConversationResource = new ConversationResource(httpUtility);
                    await newConversationResource.Get(httpUtility.baseUrl + conversationLink.href);
                    conversations.Add(newConversationResource);
                }
                return conversations;
            }
            else return null;
        }
    }
}
