using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IAudioVideoPoliciesResource
    {
        string joinScheduledOnlineMeeting { get; set; }
        string multiView { get; set; }
        AudioVideoPoliciesLinks _links { get; set; }
        Task<IAudioVideoPoliciesResource> Get(string resourceUrl);
        Task<IAudioVideoPoliciesResource> Get();
    }

    public class AudioVideoPoliciesLinks
    {
        public Link self;
    }
}
