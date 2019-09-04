using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IAudioTranscriptResource : IResourceBase
    {
        string duration { get; set; }
        string status { get; set; }
        AudioTranscriptLinks _links { get; set; }
        Task<IAudioTranscriptResource> Get(string resourceUrl);
        Task<IAudioTranscriptResource> Get();
    }

    public class AudioTranscriptLinks
    {
        public Link self;
    }
}
