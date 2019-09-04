using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IConversationsResource : IResourceBase
    {
        ConversationsLinks _links { get; set; }
        new Task<IConversationsResource> Get(string resourceUrl);
        Task<IConversationsResource> Get();
        Task<List<IConversationResource>> getConversations();
    }

    public class ConversationsLinks
    {
        public Link self;
        public List<Link> conversation;

        public ConversationsLinks()
        {
            conversation = new List<Link>();
        }
    }
}
