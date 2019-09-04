using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingExtensionResource : IResourceBase
    {
        OnlineMeetingExtensionLinks _links { get; set; }
        string id { get; set; }
        string type { get; set; }
        new Task<IOnlineMeetingExtensionResource> Get(string ResourceUrl);
        Task<IOnlineMeetingExtensionResource> Get();
    }

    public class OnlineMeetingExtensionLinks
    {
        public Link self;
    }
}
