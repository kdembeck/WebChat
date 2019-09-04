using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IDataCollaborationResource : IResourceBase
    {
        string state { get; set; }
        DataCollaborationLinks _links { get; set; }
        new Task<IDataCollaborationResource> Get(string resourceUrl);
        Task<IDataCollaborationResource> Get();
        Task<IConversationResource> getConversation();
    }

    public class DataCollaborationLinks
    {
        public Link self;
        public Link conversation;
    }
}
