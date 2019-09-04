using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPresenceResource : IResourceBase
    {
        string activity { get; set; }
        Availability? availability { get; set; }
        PresenceLinks _links { get; set; }
        Task<IPresenceResource> Get();
        new Task<IPresenceResource> Get(string resourceUrl);
        Task setAvailability(PreferredAvailability? availability = null);
    }

    public class PresenceLinks
    {
        public Link self;
    }
}
