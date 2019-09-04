using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IContactSupportedModalitiesResource
    {
        List<ModalityType> modalities { get; set; }
        ContactSupportedModalitiesLinks _links { get; set; }
        Task<IContactSupportedModalitiesResource> Get();
        Task<IContactSupportedModalitiesResource> Get(string resourceUrl);
    }

    public class ContactSupportedModalitiesLinks
    {
        public Link self;
    }
}
