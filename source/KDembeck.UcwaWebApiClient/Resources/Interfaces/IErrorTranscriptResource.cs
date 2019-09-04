using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{

    public interface IErrorTranscriptResource : IResourceBase
    {
        string reason { get; set; }
        ErrorTranscriptLinks _links { get; set; }
        new Task<IErrorTranscriptResource> Get(string resourceUrl);
        Task<IErrorTranscriptResource> Get();
    }

    public class ErrorTranscriptLinks
    {
        public Link self;
    }
    
}
