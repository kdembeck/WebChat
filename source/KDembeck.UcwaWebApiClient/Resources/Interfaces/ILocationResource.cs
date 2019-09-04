using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ILocationResource : IResourceBase
    {
        string location { get; set; }
        LocationLinks _links { get; set; }
        Task<ILocationResource> Get();
        new Task<ILocationResource> Get(string resourceUrl);
        Task setLocation(string location);
    }

    public class LocationLinks
    {
        public Link self;
    }
}
