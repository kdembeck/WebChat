using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMessageTranscriptResource : IResourceBase
    {
        string htmlMessage { get; set; }
        string plainMessage { get; set; }
        MessageTranscriptLinks _links { get; set; }
        new Task<IMessageTranscriptResource> Get(string resourceUrl);
        Task<IMessageTranscriptResource> Get();
    }

    public class MessageTranscriptLinks
    {
        public Link self;
    }
}
