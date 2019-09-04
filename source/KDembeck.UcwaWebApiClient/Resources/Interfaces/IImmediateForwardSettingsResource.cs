using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IImmediateForwardSettingsResource : IResourceBase
    {
        string target { get; set; }
        //ContactResource contact { get; set; }
        ImmediateForwardSettingsLinks _links { get; set; }
        Task<IImmediateForwardSettingsResource> Get();
        new Task<IImmediateForwardSettingsResource> Get(string resourceUrl);
        Task<IContactResource> getContact();
        Task immediateForwardToContact(string targetUri);
        Task immediateForwardToDelegates();
        Task immediateForwardToVoicemail();
    }

    public class ImmediateForwardSettingsLinks
    {
        public Link self;
        public Link contact;
        public Link immediateForwardToContact;
        public Link immediateForwardToDelegates;
        public Link immediateForwardToVoicemail;
    }
}
