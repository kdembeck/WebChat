using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class CommunicationResource : IResource
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string ResourceString { get; set; }
        public string simultaneousRingNumberMatch;
        public string videoBasedScreenSharing;
        public string audioPreference;
        public string conversationHistory;
        public string lisLocation;
        public string lisQueryResult;
        public string phoneNumber;
        public string publishEndpointLocation;
        public List<string> supportedMessageFormats;
        public List<string> supportedModalities;
        public CommunicationLinks _links;

        public CommunicationResource()
        {
            _links = new CommunicationLinks();  
            supportedMessageFormats = new List<string>();
            supportedModalities = new List<string>();
        }

        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";
            try
            {
                ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + resourceUri);
                FillResourceValues(ResourceString);
            }
            catch (Exception ex) { }
            return ResourceString;
        }

        public void FillResourceValues(string resourceString)
        {   
            ResourceString = resourceString;

            try
            {
                dynamic resourceObject = JObject.Parse(ResourceString);

                rel = resourceObject.rel;
                href = resourceObject.href;

                audioPreference = resourceObject.audioPreference;
                conversationHistory = resourceObject.conversationHistory;
                lisLocation = resourceObject.lisLocation;
                phoneNumber = resourceObject.phoneNumber;
                publishEndpointLocation = resourceObject.publishEndpointLocation;                
                foreach (string supportedMessageFormat in resourceObject.supportedMessageFormats)
                {
                    supportedMessageFormats.Add(supportedMessageFormat);
                }
                foreach (string supportedModality in resourceObject.supportedModalities)
                {
                    supportedModalities.Add(supportedModality);
                }
                videoBasedScreenSharing = resourceObject.videoBasedScreenSharing;

                if (resourceObject._links != null)
                {
                    if (resourceObject._links.conversationLogs != null)
                        _links.conversationLogs.href = resourceObject._links.conversationLogs.href;
                    if (resourceObject._links.conversations != null)
                        _links.conversations.href = resourceObject._links.conversations.href;
                    if (resourceObject._links.joinOnlineMeeting != null)
                        _links.joinOnlineMeeting.href = resourceObject._links.joinOnlineMeeting.href;
                    if (resourceObject._links.mediaPolicies != null)
                        _links.mediaPolicies.href = resourceObject._links.mediaPolicies.href;
                    if (resourceObject._links.mediaRelayAccessToken != null)
                        _links.mediaRelayAccessToken.href = resourceObject._links.mediaRelayAccessToken.href;
                    if (resourceObject._links.missedItems != null)
                        _links.missedItems.href = resourceObject._links.missedItems.href;
                    if (resourceObject._links.replayMessage != null)
                        _links.replayMessage.href = resourceObject._links.replayMessage.href;
                    if (resourceObject._links.self != null)
                        _links.self.href = resourceObject._links.self.href;
                    if (resourceObject._links.startAudioOnBehalfOfDelegator != null)
                        _links.startAudioOnBehalfOfDelegator.href = resourceObject._links.startAudioOnBehalfOfDelegator.href;
                    if (resourceObject._links.startAudioVideo != null)
                        _links.startAudioVideo.href = resourceObject._links.startAudioVideo.href;
                    if (resourceObject._links.startEmergencyCall != null)
                        _links.startEmergencyCall.href = resourceObject._links.startEmergencyCall.href;
                    if (resourceObject._links.startMessaging != null)
                        _links.startMessaging.href = resourceObject._links.startMessaging.href;
                    if (resourceObject._links.startOnlineMeeting != null)
                        _links.startOnlineMeeting.href = resourceObject._links.startOnlineMeeting.href;
                    if (resourceObject._links.startPhoneAudio != null)
                        _links.startPhoneAudio.href = resourceObject._links.startPhoneAudio.href;
                    if (resourceObject._links.startPhoneAudioOnBehalfOfDelegator != null)
                        _links.startPhoneAudioOnBehalfOfDelegator.href = resourceObject._links.startPhoneAudioOnBehalfOfDelegator.href;
                    if (resourceObject._links.startVideo != null)
                        _links.startVideo.href = resourceObject._links.startVideo.href;
                }
            }
            catch (Exception ex) { throw ex; }
        }
        public override string ToString()
        {
            if (ResourceString != null)
                return ResourceString;
            else
                return "";
        }
    }

    public class CommunicationLinks
    {
        public Link self;
        public Link conversationLogs;
        public Link conversations;
        public Link joinOnlineMeeting;
        public Link mediaPolicies;
        public Link mediaRelayAccessToken;
        public Link missedItems;
        public Link replayMessage;
        public Link startAudioOnBehalfOfDelegator;
        public Link startAudioVideo;
        public Link startEmergencyCall;
        public Link startMessaging;
        public Link startOnlineMeeting;
        public Link startPhoneAudioOnBehalfOfDelegator;
        public Link startPhoneAudio;
        public Link startVideo;
    }
}
