using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPhonesResource : IResourceBase
    {
        List<PhoneResource> phone { get; }
        PhonesLinks _links { get; set; }
        PhonesEmbedded _embedded { get; set; }        
        Task<IPhonesResource> Get();
        new Task<IPhonesResource> Get(string resourceUrl);
    }

    public class PhonesLinks
    {
        public Link self;
    }

    public class PhonesEmbedded
    {
        public List<PhoneResource> phone;

        public PhonesEmbedded()
        {
            phone = new List<PhoneResource>();
        }
    }
}
