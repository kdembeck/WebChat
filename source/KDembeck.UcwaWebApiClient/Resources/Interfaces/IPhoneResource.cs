using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPhoneResource : IResourceBase
    {
        bool includeInContactCard { get; set; }
        string number { get; set; }
        PhoneType? type { get; set; }
        PhoneLinks _links { get; set; }
        Task<IPhoneResource> Get();
        new Task<IPhoneResource> Get(string resourceUrl);
        Task changeNumber(string number = null, bool? includeInContactCard = null);
        Task changeVisibility(bool? includeInContactCard = null);
    }

    public class PhoneLinks
    {
        public Link self;
        public Link changeNumber;
        public Link changeVisibility;
    }
}
