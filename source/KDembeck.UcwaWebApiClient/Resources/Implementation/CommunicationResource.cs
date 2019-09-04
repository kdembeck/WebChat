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
    public class CommunicationResource : ResourceBase, ICommunicationResource
    {
        public SimultaneousRingNumberMatch? simultaneousRingNumberMatch { get; set; }
        public VideoBasedScreenSharing? videoBasedScreenSharing { get; set; }
        public AudioPreference? audioPreference { get; set; }
        public ConversationHistory? conversationHistory { get; set; }
        public string lisLocation { get; set; }
        public string lisQueryResult { get; set; }
        public string phoneNumber { get; set; }
        public bool? publishEndpointLocation { get; set; }
        public List<MessageFormat> supportedMessageFormats { get; set; }
        public List<ModalityType> supportedModalities { get; set; }
        public CommunicationLinks _links { get; set; }

        public CommunicationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public CommunicationResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            simultaneousRingNumberMatch = null;
            videoBasedScreenSharing = null;
            audioPreference = null;
            conversationHistory = null;
            lisLocation = null;
            lisQueryResult = null;
            phoneNumber = null;
            publishEndpointLocation = null;
            supportedMessageFormats = new List<MessageFormat>();
            supportedModalities = new List<ModalityType>();
            _links = new CommunicationLinks();
        }

        public new async Task<ICommunicationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<ICommunicationResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationLogsResource> getConversationLogs()
        {
            if (httpUtility != null && _links.conversationLogs != null)
            {
                IConversationLogsResource conversationLogsResource = new ConversationLogsResource(httpUtility);
                await conversationLogsResource.Get(httpUtility.baseUrl + _links.conversationLogs.href);
                return conversationLogsResource;
            }
            else return null;
        }

        public async Task<List<IConversationResource>> getConversations()
        {
            if (httpUtility != null && _links.conversations != null)
            {
                IConversationsResource conversationsResource = new ConversationsResource(httpUtility);
                await conversationsResource.Get(httpUtility.baseUrl + _links.conversations.href);
                return await conversationsResource.getConversations();
            }
            else return null;
        }

        public async Task<IOnlineMeetingInvitationResource> joinExistingOnlineMeeting(string onlineMeetingUri, string operationId = null)
        {
            if (httpUtility != null && _links.joinOnlineMeeting != null)
            {
                dynamic joinOnlineMeetingSettings = new ExpandoObject();
                joinOnlineMeetingSettings.onlineMeetingUri = onlineMeetingUri;
                if (operationId != null)
                    joinOnlineMeetingSettings.operationId = operationId;

                string joinOnlinMeetingJson = JsonConvert.SerializeObject(joinOnlineMeetingSettings);
                string onlineMeetingInvitationResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.joinOnlineMeeting.href + "?onlineMeetingUri=" + onlineMeetingUri, joinOnlinMeetingJson);
                IOnlineMeetingInvitationResource onlineMeetingInvitationResource = new OnlineMeetingInvitationResource(httpUtility);
                JsonConvert.PopulateObject(onlineMeetingInvitationResourceString, onlineMeetingInvitationResource);
                return onlineMeetingInvitationResource;
            }
            else return null;
        }

        public async Task<IOnlineMeetingInvitationResource> createAndJoinNewOnlineMeeting(string subject, Importance? importance = null, string operationId = null, string threadId = null)
        {
            if (httpUtility != null && _links.joinOnlineMeeting != null)
            {
                dynamic createAndJoinNewOnlineMeetingSettings = new ExpandoObject();
                createAndJoinNewOnlineMeetingSettings.subject = subject;
                if (importance != null)
                    createAndJoinNewOnlineMeetingSettings.importance = importance;
                if (operationId != null)
                    createAndJoinNewOnlineMeetingSettings.operationId = operationId;
                if (threadId != null)
                    createAndJoinNewOnlineMeetingSettings.threadId = threadId;

                string createAndJoinNewOnlineMeetingJson = JsonConvert.SerializeObject(createAndJoinNewOnlineMeetingSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                string onlineMeetingInvitationResourceString = await httpUtility.httpPostJson(httpUtility.baseUrl + _links.joinOnlineMeeting.href, createAndJoinNewOnlineMeetingJson);
                IOnlineMeetingInvitationResource onlineMeetingInvitationResource = new OnlineMeetingInvitationResource(httpUtility);
                JsonConvert.PopulateObject(onlineMeetingInvitationResourceString, onlineMeetingInvitationResource);
                return onlineMeetingInvitationResource;
            }
            else return null;
        }

        public async Task<IMediaPoliciesResource> getMediaPolicies()
        {
            if (httpUtility != null && _links.mediaPolicies != null)
            {
                IMediaPoliciesResource mediaPoliciesResource = new MediaPoliciesResource(httpUtility);
                await mediaPoliciesResource.Get(httpUtility.baseUrl + _links.mediaPolicies.href);
                return mediaPoliciesResource;
            }
            else return null;
        }

        public async Task<IMissedItemsResource> getMissedItems()
        {
            if (httpUtility != null && _links.missedItems != null)
            {
                IMissedItemsResource missedItemsResource = new MissedItemsResource(httpUtility);
                await missedItemsResource.Get(httpUtility.baseUrl + _links.missedItems.href);
                return missedItemsResource;
            }
            else return null;
        }

        public async Task replayMessage()
        {
            if (httpUtility != null && _links.replayMessage != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.replayMessage.href);
            }
        }

        public async Task startAudio(string toUri, string operationId = null, string subject = null, Importance? importance = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, string mediaOffer = null)
        {
            if (httpUtility != null && _links.startAudio != null)
            {
                dynamic startAudioSettings = new ExpandoObject();
                startAudioSettings.to = toUri;
                if (operationId != null)
                    startAudioSettings.operationId = operationId;
                if (subject != null)
                    startAudioSettings.subject = subject;
                if (importance != null)
                    startAudioSettings = importance;
                if (threadId != null)
                    startAudioSettings.threadId = threadId;
                if (sessionContext != null)
                    startAudioSettings.sessionContext = sessionContext;
                if (joinAudioMuted != null)
                    startAudioSettings.joinAudioMuted = joinAudioMuted;
                if (mediaOffer != null)
                {
                    //we're not messing with this. Not quite sure how to use it
                }

                string startAudioJson = JsonConvert.SerializeObject(startAudioSettings, new Newtonsoft.Json.Converters.StringEnumConverter());

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startAudio.href, startAudioJson);

                //AudioVideoInvitationResource audioVideoInvitationResource = new AudioVideoInvitationResource(httpUtility);
            }
            //else
            //    return null;
        }

        public async Task startAudioOnBehalfOfDelegator(string toUri, string subject = null, Importance? importance = null, string delegator = null, string operationId = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, string customContent = null, string mediaOffer = null)
        {
            if (httpUtility != null && _links.startAudioOnBehalfOfDelegator != null)
            {
                dynamic startAudioOnBehalfOfDelegatorSettings = new ExpandoObject();
                startAudioOnBehalfOfDelegatorSettings.to = toUri;
                if (subject != null)
                    startAudioOnBehalfOfDelegatorSettings.subject = subject;
                if (importance != null)
                    startAudioOnBehalfOfDelegatorSettings.importance = importance;
                if (delegator != null)
                    startAudioOnBehalfOfDelegatorSettings.delegator = delegator;
                if (operationId != null)
                    startAudioOnBehalfOfDelegatorSettings.operationId = operationId;
                if (threadId != null)
                    startAudioOnBehalfOfDelegatorSettings.threadId = threadId;
                if (sessionContext != null)
                    startAudioOnBehalfOfDelegatorSettings.sessionContext = sessionContext;
                if (joinAudioMuted != null)
                    startAudioOnBehalfOfDelegatorSettings.joinAudioMuted = joinAudioMuted;
                if (customContent != null)
                    startAudioOnBehalfOfDelegatorSettings.customContent = customContent;
                if (mediaOffer != null)
                    startAudioOnBehalfOfDelegatorSettings.mediaOffer = mediaOffer;

                string startAudioOnBehalfOfDelegatorJson = JsonConvert.SerializeObject(startAudioOnBehalfOfDelegatorSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startAudioOnBehalfOfDelegator.href, startAudioOnBehalfOfDelegatorJson);
            }
        }

        public async Task startAudioVideo(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, bool? joinVideoMuted = null, string mediaOffer = null)
        {
            if (httpUtility != null && _links.startAudioVideo != null)
            {
                dynamic startAudioVideoSettings = new ExpandoObject();
                startAudioVideoSettings.to = toUri;
                if (subject != null)
                    startAudioVideoSettings.subject = subject;
                if (importance != null)
                    startAudioVideoSettings.importance = importance;
                if (operationId != null)
                    startAudioVideoSettings.operationId = operationId;
                if (threadId != null)
                    startAudioVideoSettings.threadId = threadId;
                if (sessionContext != null)
                    startAudioVideoSettings.sessionContext = sessionContext;
                if (joinAudioMuted != null)
                    startAudioVideoSettings.joinAudioMuted = joinAudioMuted;
                if (joinVideoMuted != null)
                    startAudioVideoSettings.joinVideoMuted = joinVideoMuted;
                if (mediaOffer != null)
                    startAudioVideoSettings.mediaOffer = mediaOffer;

                string startAudioVideoJson = JsonConvert.SerializeObject(startAudioVideoSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startAudioVideo.href, startAudioVideoJson);
            }
        }

        public async Task startEmergencyCall(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, string sessionContext = null, bool? joinAudioMuted = null, string mediaOffer = null, string address = null, string building = null, string city = null, string country = null, string state = null, string zip = null)
        {
            if (httpUtility != null && _links.startEmergencyCall != null)
            {
                dynamic startEmergencyCallSettings = new ExpandoObject();
                startEmergencyCallSettings.to = toUri;
                if (subject != null)
                    startEmergencyCallSettings.subject = subject;
                if (importance != null)
                    startEmergencyCallSettings.importance = importance;
                if (operationId != null)
                    startEmergencyCallSettings.operationId = operationId;
                if (threadId != null)
                    startEmergencyCallSettings.threadId = null;
                if (sessionContext != null)
                    startEmergencyCallSettings.sessionContext = sessionContext;
                if (joinAudioMuted != null)
                    startEmergencyCallSettings.joinAudioMuted = joinAudioMuted;
                if (mediaOffer != null)
                    startEmergencyCallSettings.mediaOffer = mediaOffer;
                if (address != null)
                    startEmergencyCallSettings.address = address;
                if (building != null)
                    startEmergencyCallSettings.building = building;
                if (city != null)
                    startEmergencyCallSettings.city = city;
                if (country != null)
                    startEmergencyCallSettings.country = country;
                if (state != null)
                    startEmergencyCallSettings.state = state;
                if (zip != null)
                    startEmergencyCallSettings.zip = zip;

                string startEmergencyCallJson = JsonConvert.SerializeObject(startEmergencyCallSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startEmergencyCall.href, startEmergencyCallJson);
            }
        }

        public async Task startMessaging(string operationId, string toUri = null, string subject = null, Importance? importance = null, string message = null, MessageFormat messageFormat = MessageFormat.Plain , string threadId = null, string customContent = null)
        {
            if (httpUtility != null && _links.startMessaging != null)
            {
                dynamic startMessagingSettings = new ExpandoObject();
                startMessagingSettings.to = toUri;
                if (subject != null)
                    startMessagingSettings.subject = subject;
                if (importance != null)
                    startMessagingSettings.importance = importance;
                if (message != null)
                {
                    string messageString;
                    if (messageFormat == MessageFormat.Plain)
                        messageString = "data:text/plain;charset=utf-8," + message;
                    else
                        messageString = "data:text/html;charset=utf-8," + message;
                    startMessagingSettings._links = new ExpandoObject();
                    startMessagingSettings._links.message = new ExpandoObject();
                    startMessagingSettings._links.message.href = messageString;
                }
                if (operationId != null)
                    startMessagingSettings.operationId = operationId;
                if (threadId != null)
                    startMessagingSettings.threadId = threadId;
                if (customContent != null)
                    startMessagingSettings._links.customContent = customContent;

                string startMessagingJson = JsonConvert.SerializeObject(startMessagingSettings, new Newtonsoft.Json.Converters.StringEnumConverter());

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startMessaging.href, startMessagingJson);
            }
        }

        public async Task startOnlineMeeting(string subject = null, Importance? importance = null, string operationId = null, string threadId = null)
        {
            if (httpUtility != null && _links.startOnlineMeeting != null)
            {
                dynamic startOnlineMeetingSettings = new ExpandoObject();
                if (subject != null)
                    startOnlineMeetingSettings.subject = subject;
                if (importance != null)
                    startOnlineMeetingSettings.importance = importance;
                if (operationId != null)
                    startOnlineMeetingSettings.operationId = operationId;
                if (threadId != null)
                    startOnlineMeetingSettings.threadId = threadId;

                string startOnlineMeetingsJson = JsonConvert.SerializeObject(startOnlineMeetingSettings, new Newtonsoft.Json.Converters.StringEnumConverter());

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startOnlineMeeting.href, startOnlineMeetingsJson);
            }
        }

        public async Task startPhoneAudioOnBehalfOfDelegator(string toUri, string subject = null, Importance? importance = null, string delegator = null, string operationId = null, string threadId = null)
        {
            if (httpUtility != null && _links.startPhoneAudioOnBehalfOfDelegator != null)
            {
                dynamic startPhoneAudioOnBehalfOfDelegatorSettings = new ExpandoObject();
                startPhoneAudioOnBehalfOfDelegatorSettings.to = toUri;
                if (subject != null)
                    startPhoneAudioOnBehalfOfDelegatorSettings.subject = subject;
                if (importance != null)
                    startPhoneAudioOnBehalfOfDelegatorSettings.importance = importance;
                if (delegator != null)
                    startPhoneAudioOnBehalfOfDelegatorSettings.delegator = delegator;
                if (operationId != null)
                    startPhoneAudioOnBehalfOfDelegatorSettings.operationId = operationId;
                if (threadId != null)
                    startPhoneAudioOnBehalfOfDelegatorSettings.threadId = threadId;
                string startPhoneAudioOnBehalfOfDelegatorJson = JsonConvert.SerializeObject(startPhoneAudioOnBehalfOfDelegatorSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startPhoneAudioOnBehalfOfDelegator.href, startPhoneAudioOnBehalfOfDelegatorJson);
            }
        }

        public async Task startPhoneAudio(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, string customContent = null)
        {
            if (httpUtility != null && _links.startPhoneAudio != null)
            {
                dynamic startPhoneAudioSettings = new ExpandoObject();
                startPhoneAudioSettings.to = toUri;
                if (subject != null)
                    startPhoneAudioSettings.subject = subject;
                if (importance != null)
                    startPhoneAudioSettings.importance = importance;
                if (operationId != null)
                    startPhoneAudioSettings.operationId = operationId;
                if (threadId != null)
                    startPhoneAudioSettings.threadId = threadId;
                if (customContent != null)
                    startPhoneAudioSettings._links.customContent = customContent;
                string startPhoneAudioJson = JsonConvert.SerializeObject(startPhoneAudioSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startPhoneAudio.href, startPhoneAudioJson);
            }
        }

        public async Task startVideo(string toUri, string subject = null, Importance? importance = null, string operationId = null, string threadId = null, bool? joinVideoMuted = null, string sessionContext = null, string mediaOffer = null)
        {
            if (httpUtility != null && _links.startVideo != null)
            {
                dynamic startVideoSettings = new ExpandoObject();
                startVideoSettings.to = toUri;
                if (subject != null)
                    startVideoSettings.subject = subject;
                if (importance != null)
                    startVideoSettings.importance = importance;
                if (operationId != null)
                    startVideoSettings.operationId = operationId;
                if (threadId != null)
                    startVideoSettings.threadId = null;
                if (joinVideoMuted != null)
                    startVideoSettings.joinVideoMuted = joinVideoMuted;
                if (sessionContext != null)
                    startVideoSettings.sessionContext = sessionContext;
                if (mediaOffer != null)
                    startVideoSettings.mediaOffer = mediaOffer;
                string startVideoJson = JsonConvert.SerializeObject(startVideoSettings, new Newtonsoft.Json.Converters.StringEnumConverter());
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.startVideo.href, startVideoJson);
            }
        }

        public async void Handle_OnCommunicationUpdatedEvent(object sender, EventArgs eventArgs)
        {
            await Get();
        }
    }
}
