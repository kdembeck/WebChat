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
    public class AudioVideoInvitationResource : ResourceBase, IAudioVideoInvitationResource
    {
        public string address { get; set; }
        public string bandwidthControlId { get; set; }
        public string building { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string customContent { get; set; }
        public string delegator { get; set; }
        public string direction { get; set; }
        public string importance { get; set; }
        public bool? joinAudioMuted { get; set; }
        public bool? joinVideoMuted { get; set; }
        public string locationState { get; set; }
        public string operationId { get; set; }
        public string privateLine { get; set; }
        public string mediaOffer { get; set; }
        public string sessionContext { get; set; }
        public string state { get; set; }
        public string subject { get; set; }
        public string threadId { get; set; }
        public string to { get; set; }
        public string zip { get; set; }
        public AudioVideoInvitationLinks _links { get; set; }
        public AudioVideoInvitationEmbedded _embedded { get; set; }

        private void initializeProperties()
        {
            address = null;
            bandwidthControlId = null;
            building = null;
            city = null;
            country = null;
            customContent = null;
            delegator = null;
            direction = null;
            importance = null;
            joinAudioMuted = null;
            joinVideoMuted = null;
            locationState = null;
            operationId = null;
            privateLine = null;
            mediaOffer = null;
            sessionContext = null;
            state = null;
            subject = null;
            threadId = null;
            to = null;
            zip = null;
            _links = new AudioVideoInvitationLinks();
            _embedded = new AudioVideoInvitationEmbedded();
        }

        public AudioVideoInvitationResource()
        {
            initializeProperties();
        }

        public AudioVideoInvitationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IAudioVideoInvitationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IAudioVideoInvitationResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IContactResource> getAcceptedByContact()
        {
            if (httpUtility != null && _links.acceptedByContact != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.acceptedByContact.href);
                return contactResource;
            }
            else
                return null;
        }

        public async Task acceptWithAnswer(string sessionContext, string processedOfferId = null, byte[] spd = null)
        {
            if (httpUtility != null && _links.acceptWithAnswer != null)
            {
                dynamic acceptWithAnswerSettings = new ExpandoObject();                
                if (spd != null)
                    acceptWithAnswerSettings.spd = spd;

                string acceptWithAnswerUrl = httpUtility.baseUrl + _links.acceptWithAnswer.href + "?sessionContext=" + sessionContext;
                if (processedOfferId != null)
                    acceptWithAnswerUrl += "&processedOfferId=" + processedOfferId;

                await httpUtility.httpPostJson(acceptWithAnswerUrl, acceptWithAnswerSettings);
            }
        }

        public async Task acceptWithPhoneAudio()
        {
            if (httpUtility != null && _links.acceptWithPhoneAudio != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.acceptWithPhoneAudio.href);
            }
        }

        public async Task<IAudioVideoResource> getAudioVideo()
        {
            if (httpUtility != null && _links.audioVideo != null)
            {
                IAudioVideoResource audioVideoResource = new AudioVideoResource(httpUtility);
                await audioVideoResource.Get(httpUtility.baseUrl + _links.audioVideo.href);
                return audioVideoResource;
            }
            else
                return null;
        }

        public async Task cancel()
        {
            if (httpUtility != null && _links.cancel != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.cancel.href);
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

        public async Task decline()
        {
            if (httpUtility != null && _links.decline != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.decline.href);
            }
        }

        public async Task<IContactResource> getDelegator()
        {
            if (httpUtility != null && _links.delegator != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.delegator.href);
                return contactResource;
            }
            else
                return null;
        }

        public async Task<IAudioVideoResource> getDerivedAudioVideo()
        {
            if (httpUtility != null && _links.derivedAudioVideo != null)
            {
                IAudioVideoResource audioVideoResource = new AudioVideoResource(httpUtility);
                await audioVideoResource.Get(httpUtility.baseUrl + _links.audioVideo.href);
                return audioVideoResource;
            }
            else
                return null;
        }

        public async Task<IConversationResource> getDerivedConversation()
        {
            if (httpUtility != null && _links.derivedConversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.derivedConversation.href);
                return conversationResource;
            }
            else
                return null;
        }

        public async Task<IContactResource> getForwardedBy()
        {
            if (httpUtility != null && _links.forwardedBy != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.forwardedBy.href);
                return contactResource;
            }
            else
                return null;
        }

        public async Task<IParticipantResource> getFrom()
        {
            if (httpUtility != null && _links.from != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.from.href);
                return participantResource;
            }
            else
                return null;
        }

        public async Task<IContactResource> getOnBehalfOf()
        {
            if (httpUtility != null && _links.onBehalfOf != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.onBehalfOf.href);
                return contactResource;
            }
            else return null;
        }

        public async Task getReplacesAudioVideo()
        {
            throw new NotImplementedException();
        }

        public async Task reportMediaDiagnostics(ErrorCode? errorCode = null, ErrorSubcode? errorSubcode = null)
        {
            if (httpUtility != null && _links.reportMediaDiagnostics != null)
            {
                dynamic reprotMediaDiagnosticsSettings = new ExpandoObject();
                if (errorCode != null)
                    reprotMediaDiagnosticsSettings.errorCode = errorCode;
                if (errorSubcode != null)
                    reprotMediaDiagnosticsSettings.errorSubcode = errorSubcode;
                string reportMediaDiagnosticsJson = JsonConvert.SerializeObject(reprotMediaDiagnosticsSettings, new Newtonsoft.Json.Converters.StringEnumConverter());

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.reportMediaDiagnostics.href, reportMediaDiagnosticsJson);
            }
        }

        public async Task sendProvisionalAnswer(string sessionContext, string processOfferId = null, byte[] sdp = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IContactResource> getTo()
        {
            if (httpUtility != null && _links.to != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.to.href);
                return contactResource;
            }
            else
                return null;
        }

        public async Task<IContactResource> getTrasnferredBy()
        {
            if (httpUtility != null && _links.transferredBy != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.transferredBy.href);
                return contactResource;
            }
            else
                return null;
        }
    }
}
