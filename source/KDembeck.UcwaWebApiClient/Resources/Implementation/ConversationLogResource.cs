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
    public class ConversationLogResource : ResourceBase, IConversationLogResource
    {
        public string creationTime { get; set; }
        public string direction { get; set; }
        public Importance? importance { get; set; }
        public List<ModalityType> modalities { get; set; }
        public string onlineMeetingUri { get; set; }
        public string previewMessage { get; set; }
        public string status { get; set; }
        public string subject { get; set; }
        public string threadId { get; set; }
        public int totalRecipientsCount { get; set; }
        public string type { get; set; }
        public ConversationLogLinks _links { get; set; }
        public ConversationLogEmbedded _embedded { get; set; }

        public ConversationLogResource()
        {
            initializeProperties();
        }

        public ConversationLogResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            creationTime = null;
            direction = null;
            importance = null;
            modalities = new List<ModalityType>();
            onlineMeetingUri = null;
            previewMessage = null;
            status = null;
            subject = null;
            threadId = null;
            totalRecipientsCount = -1;
            type = null;
            _links = new ConversationLogLinks();    
        }
        
        public new async Task<IConversationLogResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationLogResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
        
        public async Task Delete()
        {
            if (httpUtility != null && _links.self != null)
            {
                await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
        }

        public async Task<IAudioVideoInvitationResource> continueAudio(string operationId = null, string mediaOffer = null, string sessionContext = null)
        {
            throw new NotImplementedException();
        }

        public async Task continueAudioVidoe()
        {
            throw new NotImplementedException();
        }

        public async Task continueMessaging(string message = null, string operationId = null)
        {
            if (httpUtility != null && _links.continueMessaging != null)
            {
                dynamic continueMessagingSettings = new ExpandoObject();
                if (message != null)
                    continueMessagingSettings.message = message;
                if (operationId != null)
                    continueMessagingSettings.operationId = operationId;
                string continueMessagingJson = JsonConvert.SerializeObject(continueMessagingSettings);
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.continueMessaging.href, continueMessagingJson);
            }
        }

        public async Task continuePhoneAudio(string phoneNumber = null, string operationId = null)
        {
            if (httpUtility != null && _links.continuePhoneAudio != null)
            {
                dynamic continuePhoneAudioSettings = new ExpandoObject();
                if (phoneNumber != null)
                    continuePhoneAudioSettings.phoneNumber = phoneNumber;
                if (operationId != null)
                    continuePhoneAudioSettings.operationId = operationId;
                string continuePhoneAudioJson = JsonConvert.SerializeObject(continuePhoneAudioSettings);
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.continuePhoneAudio.href, continuePhoneAudioJson);
            }
        }

        public async Task continueVideo()
        {
            throw new NotImplementedException();
        }

        public async Task<IConversationLogTranscriptsResource> getConversationLogTrasncripts()
        {
            if (httpUtility != null && _links.conversationLogTranscripts != null)
            {
                ConversationLogTranscriptsResource conversationLogTranscriptsResource = new ConversationLogTranscriptsResource(httpUtility);
                await conversationLogTranscriptsResource.Get(httpUtility.baseUrl + _links.conversationLogTranscripts.href);
                return conversationLogTranscriptsResource;
            }
            else
                return null;
        }

        public async Task markAsRead()
        {
            if (httpUtility != null && _links.markAsRead != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.markAsRead.href);
            }
        }

    }
}
