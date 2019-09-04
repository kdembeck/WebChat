using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PhoneAudioResource : ResourceBase, IPhoneAudioResource
    {
        public string state { get; set; }
        public PhoneAudioLinks _links { get; set; }

        private void initializeProperties()
        {
            state = null;
            _links = new PhoneAudioLinks();
        }

        public PhoneAudioResource()
        {
            initializeProperties();
        }

        public PhoneAudioResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IPhoneAudioResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public async Task<IPhoneAudioResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public async Task addPhoneAudio(string toUri, string phoneNumber = null, string operationId = null)            
        {
            if (httpUtility != null && _links.addPhoneAudio != null)
            {
                dynamic addPhoneAudioSettings = new ExpandoObject();
                addPhoneAudioSettings.to = toUri;
                if (phoneNumber != null)
                    addPhoneAudioSettings.phoneNumber = phoneNumber;
                if (operationId != null)
                    addPhoneAudioSettings.operationId = operationId;
                string addPhoneAudioJson = JsonConvert.SerializeObject(addPhoneAudioSettings);
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addPhoneAudio.href, addPhoneAudioJson);
            }
        }

        public async Task<IConversationResource> getConversation()
        {
            if (httpUtility != null && _links.conversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else
                return null;
        }

        public async Task holdPhoneAudio()
        {
            if (httpUtility != null && _links.holdPhoneAudio != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.holdPhoneAudio.href);
            }
        }

        public async Task resumePhoneAudio()
        {
            if (httpUtility != null && _links.resumePhoneAudio != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.resumePhoneAudio.href);
            }
        }

        public async Task stopPhoneAudio()
        {
            if (httpUtility != null && _links.stopPhoneAudio != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.stopPhoneAudio.href);
            }
        }
    }
}
