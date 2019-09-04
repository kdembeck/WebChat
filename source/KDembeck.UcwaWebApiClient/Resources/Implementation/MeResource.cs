using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Dynamic;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MeResource : ResourceBase, IMeResource
    {
        public string company { get; set; }
        public string department { get; set; }
        public List<string> emailAddresses { get; set; }
        public string endpointUri { get; set; }
        public string homePhoneNumber { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string name { get; set; }
        public string officeLocation { get; set; }
        public string otherPhoneNumber { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
        public string workPhoneNumber { get; set; }
        public MeLinks _links { get; set; }

        public MeResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public MeResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            company = null;
            department = null;
            emailAddresses = new List<string>();
            endpointUri = null;
            homePhoneNumber = null;
            mobilePhoneNumber = null;
            name = null;
            officeLocation = null;
            otherPhoneNumber = null;
            title = null;
            uri = null;
            workPhoneNumber = null;
            _links = new MeLinks();
        }

        public new async Task<IMeResource> Get(string ResourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(ResourceUrl);                
            }
            return this;
        }

        public async Task<IMeResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public async Task<ICallForwardingSettingsResource> getCallForwardingSettings()
        {
            if (httpUtility != null && _links.callForwardingSettings != null)
            {
                ICallForwardingSettingsResource callForwardingSettingsResource = new CallForwardingSettingsResource(httpUtility);
                await callForwardingSettingsResource.Get(httpUtility.baseUrl + _links.callForwardingSettings.href);
                return callForwardingSettingsResource;
            }
            else
                return null;
        }

        public async Task<ILocationResource> getLocation()
        {
            if (httpUtility != null && _links.location != null)
            {
                ILocationResource locationResource = new LocationResource(httpUtility);
                await locationResource.Get(httpUtility.baseUrl + _links.location.href);
                return locationResource;
            }
            else
                return null;
        }

        public async Task makeMeAvailable(AudioPreference? audioPreference = null, TimeSpan? inactiveTimeout = null, string phoneNumber = null, PreferredAvailability? signInAs = null, List<MessageFormat> supportedMessageFormats = null, List<ModalityType> supportedModalities = null, TimeSpan? voipFallbackToPhoneAudioTimeOut = null)
        {
            if (httpUtility != null && _links.makeMeAvailable != null)
            {
                dynamic makeMeAvailableSettings = new ExpandoObject();
                if (audioPreference != null)
                    makeMeAvailableSettings.audioPreference = audioPreference;
                if (inactiveTimeout != null)
                    makeMeAvailableSettings.inactiveTimeout = inactiveTimeout;
                if (phoneNumber != null)
                    makeMeAvailableSettings.phoneNumber = phoneNumber;
                if (signInAs != null)
                    makeMeAvailableSettings.signInAs = signInAs;
                if (supportedMessageFormats != null)
                    makeMeAvailableSettings.supportedMessageFormats = supportedMessageFormats;
                if (supportedModalities != null)
                    makeMeAvailableSettings.supportedModalities = supportedModalities;
                if (voipFallbackToPhoneAudioTimeOut != null)
                    makeMeAvailableSettings.voipFallbackToPhoneAudioTimeOut = voipFallbackToPhoneAudioTimeOut;                
                
                string makeMeAvailableSettingsJson = JsonConvert.SerializeObject(makeMeAvailableSettings, new Newtonsoft.Json.Converters.StringEnumConverter());

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.makeMeAvailable.href, makeMeAvailableSettingsJson);
            }
        }

        public async Task<INoteResource> getNote()
        {
            if (httpUtility != null && _links.note != null)
            {
                INoteResource noteResource = new NoteResource(httpUtility);
                await noteResource.Get(httpUtility.baseUrl + _links.note.href);
                return noteResource;
            }
            else
                return null;
        }

        public async Task<IPhonesResource> getPhones()
        {
            if (httpUtility != null && _links.phones != null)
            {
                IPhonesResource phonesResource = new PhonesResource(httpUtility);
                await phonesResource.Get(httpUtility.baseUrl + _links.phones.href);
                return phonesResource;
            }
            else
                return null;
        }

        public async Task<Stream> getPhoto()
        {
            if (httpUtility != null && _links.photo != null)
            {
                Stream photoStream = null;
                photoStream = await httpUtility.httpGetImageJpeg(httpUtility.baseUrl + _links.photo.href);
                return photoStream;
            }
            else
                return null;
        }

        public async Task<IPresenceResource> getPresence()
        {
            if (httpUtility != null && _links.presence != null)
            {
                IPresenceResource presenceResource = new PresenceResource(httpUtility);
                await presenceResource.Get(httpUtility.baseUrl + _links.presence.href);
                return presenceResource;
            }
            else
                return null;    
        }

        public async Task reportMyActivity()
        {
            if (httpUtility != null && _links.reportMyActivity != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.reportMyActivity.href);
            }
        }
    }
}
