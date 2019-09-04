using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class AudioVideoSessionResource : ResourceBase, IAudioVideoSessionResource
    {
        public string remoteEndpoint { get; set; }
        public string sessionContext { get; set; }
        public string state { get; set; }
        public AudioVideoSessionLinks _links { get; set; }

        private void initializeProperties()
        {
            remoteEndpoint = null;
            sessionContext = null;
            state = null;
            _links = new AudioVideoSessionLinks();
        }

        public AudioVideoSessionResource()
        {
            initializeProperties();
        }

        public AudioVideoSessionResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IAudioVideoSessionResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IAudioVideoSessionResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IApplicationSharingResource> getApplicationSharing()
        {
            if (httpUtility != null && _links.applicationSharing != null)
            {
                IApplicationSharingResource applicationSharingResource = new ApplicationSharingResource(httpUtility);
                await applicationSharingResource.Get(httpUtility.baseUrl + _links.applicationSharing.href);
                return applicationSharingResource;
            }
            else
                return null;
        }

        public async Task<IAudioVideoResource> getAudioVideo()
        {
            if (httpUtility != null && _links.audioVideo != null)
            {
                IAudioVideoResource audioVideoResource = new AudioVideoResource(httpUtility);
                await audioVideoResource.Get(httpUtility.baseUrl + _links.audioVideo.href);
                return audioVideoResource;
            }
            else return null;
        }

        public async Task<IConversationResource> getConversation()
        {
            if (httpUtility != null && _links.conversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else return null;
        }

        public async Task<IDataCollaborationResource> getDataCollaboration()
        {
            if (httpUtility != null && _links.dataCollaboration != null)
            {
                IDataCollaborationResource dataCollaborationResource = new DataCollaborationResource(httpUtility);
                await dataCollaborationResource.Get(httpUtility.baseUrl + _links.dataCollaboration.href);
                return dataCollaborationResource;
            }
            else
                return null;
        }

        public async Task publishCallQualityFeedback(string mediaEndpoint=null, string mediaQualityOfExperience=null)
        {
            if (httpUtility != null && _links.publishCallQualityFeedback.href != null)
            {
                if (mediaEndpoint != null || mediaQualityOfExperience != null)
                {
                    dynamic publishCallQualityFeedbackSettings = new ExpandoObject();
                    if (mediaEndpoint != null)
                        publishCallQualityFeedbackSettings.mediaEndpoint = mediaEndpoint;
                    if (mediaQualityOfExperience != null)
                        publishCallQualityFeedbackSettings.mediaQualityOfExperience = mediaQualityOfExperience;
                    string publishCallQualityFeedbackSettingsJson = JsonConvert.SerializeObject(publishCallQualityFeedbackSettings);
                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.publishCallQualityFeedback.href, publishCallQualityFeedbackSettingsJson);
                }
            }            
        }

        public async Task renegotiations(string operationId, byte[] sdp = null)
        {
            throw new NotImplementedException();
        }

        public async Task resumeAudio(string operationId = null, byte[] sdp = null)
        {
            throw new NotImplementedException();
        }

        public async Task resumeAudioVideo(string operationId = null, byte[] sdp = null)
        {
            throw new NotImplementedException();
        }
    }
}
