using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IDialInRegionResource : IResourceBase
    {
        DialInRegionLinks _links { get; set; }
        List<string> languages { get; set; }
        string name { get; set; }
        string number { get; set; }
        Task<IDialInRegionResource> Get();
        new Task<IDialInRegionResource> Get(string resourceUrl);
    }

    public class DialInRegionLinks
    {
        public Link self;
    }
}
