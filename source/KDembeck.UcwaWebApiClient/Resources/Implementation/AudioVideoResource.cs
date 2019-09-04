using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class AudioVideoResource : ResourceBase, IAudioVideoResource
    {
        public string state { get; set; }
        public string supportsReplaces { get; set; }
        public VideoSourcesAllowed? videoSourcesAllowed { get; set; }
        public AudioVideoLinks _links { get; set; }

        private void initializeProperties()
        {
            state = null;
            supportsReplaces = null;
            videoSourcesAllowed = null;
            _links = new AudioVideoLinks();
        }

        public AudioVideoResource()
        {
            initializeProperties();
        }

        public AudioVideoResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IAudioVideoResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IAudioVideoResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task addAudio(string toUri, string operationId = null, string mediaOffer = null, string sessionContext = null, bool? joinAudioMuted = null)
        {
            if (httpUtility != null && _links.addAudio != null)
            {
                dynamic addAudioSettings = new ExpandoObject();
                addAudioSettings.to = toUri;
                if (operationId != null)
                    addAudioSettings.operationId = operationId;
                if (mediaOffer != null)
                    addAudioSettings.mediaOffer = mediaOffer;
                if (sessionContext != null)
                    addAudioSettings.sessionContext = sessionContext;
                if (joinAudioMuted != null)
                    addAudioSettings.joinAudioMuted = joinAudioMuted;

                string addAudioSettingsJson = JsonConvert.SerializeObject(addAudioSettings);
                string audioVideoInvitationResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addAudio.href, addAudioSettingsJson);                
            }
        }

        public async Task addAudioVideo(string toUri, string operationId = null, string mediaOffer = null, string sessionContext = null, bool? joinAudioMuted = null, bool? joinVideoMuted = null)
        {
            if (httpUtility != null && _links.addAudioVideo != null)
            {
                dynamic addAudioVideoSettings = new ExpandoObject();
                addAudioVideoSettings.to = toUri;
                if (operationId != null)
                    addAudioVideoSettings.operationId = operationId;
                if (mediaOffer != null)
                    addAudioVideoSettings.mediaOffer = mediaOffer;
                if (sessionContext != null)
                    addAudioVideoSettings.sessionContext = sessionContext;
                if (joinAudioMuted != null)
                    addAudioVideoSettings.joinAudioMuted = joinAudioMuted;
                if (joinVideoMuted != null)
                    addAudioVideoSettings.joinVideoMuted = joinVideoMuted;

                string addAudioVideoSettingsJson = JsonConvert.SerializeObject(addAudioVideoSettings);
                string audioVideoInvitationResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addAudioVideo.href, addAudioVideoSettingsJson);
            }
        }

        public async Task<IAudioVideoPoliciesResource> getAudioVideoPolicies()
        {
            if (httpUtility != null && _links.audioVideoPolicies != null)
            {
                IAudioVideoPoliciesResource audioVideoPoliciesResource = new AudioVideoPoliciesResource(httpUtility);
                await audioVideoPoliciesResource.Get(httpUtility.baseUrl + _links.audioVideoPolicies.href);
                return audioVideoPoliciesResource;
            }
            return null;
        }

        public async Task<IAudioVideoSessionResource> getAudioVideoSession()
        {
            if (httpUtility != null && _links.audioVideoSession != null)
            {
                IAudioVideoSessionResource audioVideoSession = new AudioVideoSessionResource(httpUtility);
                await audioVideoSession.Get(httpUtility.baseUrl + _links.audioVideoSession.href);
                return audioVideoSession;
            }
            else
                return null;
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

        public async Task escalateAudioVideo(string operationId, string sessionContext, byte[] sdp = null)
        {
            if (httpUtility != null && _links.escalateAudioVideo != null)
            {
                string escalateAudioVideoJson = null;
                if (sdp != null)
                {
                    escalateAudioVideoJson = JsonConvert.SerializeObject(new
                    {
                        sdp = sdp
                    });
                }
                string audioVideoInvitationResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.escalateAudioVideo.href + "?operationId=" + operationId + "&sessionContext=" + sessionContext, escalateAudioVideoJson);
            }
        }

        public async Task escalateAudio(string operationId, string sessionContext, byte[] sdp = null)
        {
            if (httpUtility != null && _links.escalateAudio != null)
            {
                string escalateAudioJson = null;
                if (sdp != null)
                {
                    escalateAudioJson = JsonConvert.SerializeObject(new
                    {
                        sdp = sdp
                    });
                }
                string audioVideoInvitationResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.escalateAudio.href + "?operationId=" + operationId + "&sessionContext=" + sessionContext, escalateAudioJson);
            }
        }

        public async Task replaceWithPhoneAudio(string toUri, string phoneNumber = null, string operationId = null)
        {
            if (httpUtility != null && _links.replaceWithPhoneAudio != null)
            {
                dynamic replaceWithPhoneAudioSettings = new ExpandoObject();
                replaceWithPhoneAudioSettings.to = toUri;
                if (phoneNumber != null)
                    replaceWithPhoneAudioSettings.phoneNumber = phoneNumber;
                if (operationId != null)
                    replaceWithPhoneAudioSettings.operationId = operationId;
                string replaceWithPhoneAudioJson = JsonConvert.SerializeObject(replaceWithPhoneAudioSettings);
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.replaceWithPhoneAudio.href, replaceWithPhoneAudioJson);
            }
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

        public async Task stopAudio()
        {
            if (httpUtility != null && _links.stopAudio != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.stopAudio.href);
            }
        }

        public async Task stopAudioVideo()
        {
            if (httpUtility != null && _links.stopAudioVideo != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.stopAudioVideo.href);
            }
        }

        public async Task stopVideo()
        {
            if (httpUtility != null && _links.stopVideo != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.stopVideo.href);
            }
        }

        public async Task videoFreeze()
        {
            throw new NotImplementedException();
        }

        public async Task<IParticipantResource> videoLockedOnParticipant()
        {
            if (httpUtility != null && _links.videoLockedOnParticipant != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.videoLockedOnParticipant.href);
                return participantResource;
            }
            else
                return null;
        }
    }
}
