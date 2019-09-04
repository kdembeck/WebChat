using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingExtensionsResource : IResourceBase
    {
        OnlineMeetingExtensionsLinks _links { get; set; }
        OnlineMeetingExtensionsEmbedded _embedded { get; set; }
        new Task<IOnlineMeetingExtensionsResource> Get(string resourceUrl);
        Task<IOnlineMeetingExtensionsResource> Get();
    }

    public class OnlineMeetingExtensionsLinks
    {
        public Link self;
    }

    public class OnlineMeetingExtensionsEmbedded
    {
        public List<OnlineMeetingExtensionResource> onlineMeetingExtension;

        public OnlineMeetingExtensionsEmbedded()
        {
            onlineMeetingExtension = new List<OnlineMeetingExtensionResource>();
        }
    }
}
