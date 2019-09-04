using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IConversationLogTranscriptsResource : IResourceBase
    {
        ConvesationLogTranscriptsLinks _links { get; set; }
        ConversationLogTranscriptsEmbedded _embedded { get; set; }
        List<ConversationLogTranscriptResource> conversationLogTranscripts { get; }
        new Task<IConversationLogTranscriptsResource> Get(string resourceUrl);
        Task<IConversationLogTranscriptsResource> Get();
        Task<IConversationLogTranscriptsResource> getNextConversationLogTranscripts();
    }

    public class ConvesationLogTranscriptsLinks
    {
        public Link self;
        public Link nextConversationLogTranscripts;
    }

    public class ConversationLogTranscriptsEmbedded
    {
        public List<ConversationLogTranscriptResource> conversationLogTranscript;
        public ConversationLogTranscriptsEmbedded()
        {
            conversationLogTranscript = new List<ConversationLogTranscriptResource>();
        }
    }
}
