using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ConversationLogTranscriptsResource : ResourceBase, IConversationLogTranscriptsResource
    {
        public ConvesationLogTranscriptsLinks _links { get; set; }
        public ConversationLogTranscriptsEmbedded _embedded { get; set; }
        public List<ConversationLogTranscriptResource> conversationLogTranscripts { get { return _embedded.conversationLogTranscript; } }

        private void initializeProperties()
        {
            _links = new ConvesationLogTranscriptsLinks();
            _embedded = new ConversationLogTranscriptsEmbedded();
        }

        public ConversationLogTranscriptsResource()
        {
            initializeProperties();
        }

        public ConversationLogTranscriptsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IConversationLogTranscriptsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationLogTranscriptsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationLogTranscriptsResource> getNextConversationLogTranscripts()
        {
            if (httpUtility != null && _links.nextConversationLogTranscripts != null)
            {
                IConversationLogTranscriptsResource conversationLogTranscriptsResource = new ConversationLogTranscriptsResource(httpUtility);
                await conversationLogTranscriptsResource.Get(httpUtility.baseUrl + _links.nextConversationLogTranscripts.href);
                return conversationLogTranscriptsResource;
            }
            else
                return null;
        }
    }

}
