using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IAutoDiscoverResource 
    {
        AutoDiscoverLinks _links { get; set; }
        Task<IAutoDiscoverResource> Get(string ResourceUrl);
        Task<IAutoDiscoverResource> Get();
    }

    public class AutoDiscoverLinks
    {
        public Link self;
        public Link user;
        public Link xframe;
        public Link redirect;
        public ApplicationsLink applications;

        public AutoDiscoverLinks()
        {
            applications = new ApplicationsLink();
        }
    }

    public class ApplicationsLink
    {
        public string href;
        public int revision;
    }
}
