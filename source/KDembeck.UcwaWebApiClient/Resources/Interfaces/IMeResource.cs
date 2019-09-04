using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMeResource : IResourceBase
    {
        string company { get; set; }
        string department { get; set; }
        List<string> emailAddresses { get; set; }
        string endpointUri { get; set; }
        string homePhoneNumber { get; set; }
        string mobilePhoneNumber { get; set; }
        string name { get; set; }
        string officeLocation { get; set; }
        string otherPhoneNumber { get; set; }
        string title { get; set; }
        string uri { get; set; }
        string workPhoneNumber { get; set; }
        MeLinks _links { get; set; }
        new Task<IMeResource> Get(string ResourceUrl);
        Task<IMeResource> Get();
        Task<ICallForwardingSettingsResource> getCallForwardingSettings();
        Task<ILocationResource> getLocation();
        Task makeMeAvailable(AudioPreference? audioPreference = null, TimeSpan? inactiveTimeout = null, string phoneNumber = null, PreferredAvailability? signInAs = null, List<MessageFormat> supportedMessageFormats = null, List<ModalityType> supportedModalities = null, TimeSpan? voipFallbackToPhoneAudioTimeOut = null);
        Task<INoteResource> getNote();
        Task<IPhonesResource> getPhones();
        Task<Stream> getPhoto();
        Task<IPresenceResource> getPresence();
        Task reportMyActivity();
    }

    public class MeLinks
    {
        public Link self;
        public Link callForwardingSettings;
        public Link location;
        public Link makeMeAvailable;
        public Link note;
        public Link phones;
        public Link photo;
        public Link presence;
        public Link reportMyActivity;
    }
}
