using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IUnansweredCallSettingsResource : IResourceBase
    {
        int ringDelay { get; set; }
        string target { get; set; }
        UnansweredCallSettingsLinks _links { get; set; }
        Task<IUnansweredCallSettingsResource> Get();
        new Task<IUnansweredCallSettingsResource> Get(string resourceUrl);
        Task Update(int ringDelay, string target);
        Task<IContactResource> getContact();
        Task resetUnansweredCallSettings();
        Task unansweredCallToContact(int ringDelaySeconds, string targetUri);
        Task unansweredCallToVoicemail(int ringDelaySeconds);
    }

    public class UnansweredCallSettingsLinks
    {
        public Link self;
        public Link contact;
        public Link resetUnansweredCallSettings;
        public Link unansweredCallToContact;
        public Link unansweredCallToVoicemail;
    }
}
