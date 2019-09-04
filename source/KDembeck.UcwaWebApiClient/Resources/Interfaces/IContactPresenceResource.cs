using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IContactPresenceResource 
    {
        string activity { get; set; }
        Availability? availability { get; set; }
        string deviceType { get; set; }
        string lastActive { get; set; }
        ContactPresenceLinks _links { get; set; }
        Task<IContactPresenceResource> Get();
        Task<IContactPresenceResource> Get(string resourceUrl);
    }

    public class ContactPresenceLinks
    {
        public Link self;
    }
}
