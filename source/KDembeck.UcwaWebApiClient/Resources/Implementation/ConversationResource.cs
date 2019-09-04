using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ConversationResource : ResourceBase, IConversationResource
    {
        public List<ModalityType> activeModalities { get; set; }
        public string audienceMessaging { get; set; }
        public string audienceMute { get; set; }
        public string created { get; set; }
        public string expirationTime { get; set; }
        public string importance { get; set; }
        public int? participantCount { get; set; }
        public bool? readLocally { get; set; }
        public bool? recording { get; set; }
        public string state { get; set; }
        public string subject { get; set; }
        public string threadId { get; set; }
        public ConversationLinks _links { get; set; }

        private void initializeProperties()
        {
            activeModalities = new List<ModalityType>();
            audienceMessaging = null;
            audienceMute = null;
            created = null;
            expirationTime = null;
            importance = null;
            participantCount = null;
            readLocally = null;
            recording = null;
            state = null;
            subject = null;
            threadId = null;
            _links = new ConversationLinks();
        }

        public ConversationResource()
        {
            initializeProperties();
        }

        public ConversationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }
        
        public new async Task<IConversationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IConversationResource> Get()
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

        public async Task addParticipant(string toUri, string operationId)
        {
            if (httpUtility != null && _links.addParticipant != null)
            {
                string addParticipantJson = JsonConvert.SerializeObject(new
                {
                    to = toUri,
                    operationId = operationId
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addParticipant.href, addParticipantJson);
            }            
        }

        public async Task addParticipant(string toUri)
        {
            if (httpUtility != null && _links.addParticipant != null)
            {
                string addParticipantJson = JsonConvert.SerializeObject(new
                {
                    to = toUri                    
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addParticipant.href, addParticipantJson);
            }            
        }

        public async Task<IAttendeesResource> getAttendees()
        {
            if (httpUtility != null && _links.attendees != null)
            {
                IAttendeesResource attendeesResource = new AttendeesResource(httpUtility);
                await attendeesResource.Get(httpUtility.baseUrl + _links.attendees.href);
                return attendeesResource;
            }
            else return null;
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

        public async Task<IDataCollaborationResource> getDataCollaboration()
        {
            if (httpUtility != null & _links.dataCollaboration != null)
            {
                IDataCollaborationResource dataCollaborationResource = new DataCollaborationResource(httpUtility);
                await dataCollaborationResource.Get(httpUtility.baseUrl + _links.dataCollaboration.href);
                return dataCollaborationResource;
            }
            else return null;
        }

        public async Task disableAudienceMessaging()
        {
            if (httpUtility != null && _links.disableAudienceMessaging != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.disableAudienceMessaging.href);
            }
        }

        public async Task disableAudienceMuteLock()
        {
            if (httpUtility != null && _links.disableAudienceMuteLock != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.disableAudienceMuteLock.href);
            }
        }

        public async Task enableAudienceMessaging()
        {
            if (httpUtility != null && _links.enableAudienceMessaging != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.enableAudienceMessaging.href);
            }
        }

        public async Task enableAudienceMuteLock()
        {
            if (httpUtility != null && _links.enableAudienceMuteLock != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.enableAudienceMuteLock.href);
            }
        }

        public async Task<ILeadersResource> getLeaders()
        {
            if (httpUtility != null & _links.leaders != null)
            {
                ILeadersResource leadersResource = new LeadersResource(httpUtility);
                await leadersResource.Get(httpUtility.baseUrl + _links.leaders.href);
                return leadersResource;
            }
            else
                return null;
        }

        public async Task<ILobbyResource> getLobby()
        {
            if (httpUtility != null && _links.lobby != null)
            {
                ILobbyResource lobbyResource = new LobbyResource(httpUtility);
                await lobbyResource.Get(httpUtility.baseUrl + _links.lobby.href);
                return lobbyResource;
            }
            else
                return null;
        }

        public async Task<IParticipantResource> getLocalParticipant()
        {
            if (httpUtility != null && _links.localParticipant != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.localParticipant.href);
                return participantResource;
            }
            else return null;
        }


        public async Task<IMessagingResource> getMessaging()
        {
            if (httpUtility != null && _links.messaging != null)
            {
                IMessagingResource messagingResource = new MessagingResource(httpUtility);
                await messagingResource.Get(httpUtility.baseUrl + _links.messaging.href);
                return messagingResource;
            }
            else return null;
        }

        public async Task<IOnlineMeetingResource> getOnlineMeeting()
        {
            if (httpUtility != null & _links.onlineMeeting != null)
            {
                IOnlineMeetingResource onlineMeetingResource = new OnlineMeetingResource(httpUtility);
                await onlineMeetingResource.Get(httpUtility.baseUrl + _links.onlineMeeting.href);
                return onlineMeetingResource;
            }
            else return null;
        }

        public async Task<List<IParticipantResource>> getParticipants()
        {
            List<IParticipantResource> participantsList = new List<IParticipantResource>();
            if (httpUtility != null && _links.participants != null)
            {
                IParticipantsResource participantsResource = new ParticipantsResource(httpUtility);
                await participantsResource.Get(httpUtility.baseUrl + _links.participants.href);
                
                if (participantsResource._links.participant != null && participantsResource._links.participant.Count > 0)
                {
                    foreach (ParticipantLink participantLink in participantsResource._links.participant)
                    {
                        IParticipantResource participantResource = new ParticipantResource(httpUtility);
                        await participantResource.Get(httpUtility.baseUrl + participantLink.href);
                        participantsList.Add(participantResource);
                    }                    
                }                
            }
            return participantsList;
        }

        public async Task<IPhoneAudioResource> getPhoneAudio()
        {
            if (httpUtility != null && _links.phoneAudio != null)
            {
                IPhoneAudioResource phoneAudioResource = new PhoneAudioResource(httpUtility);
                await phoneAudioResource.Get(httpUtility.baseUrl + _links.phoneAudio.href);
                return phoneAudioResource;
            }
            else
                return null;
        }

        public async Task userAcknowledged()
        {
            //no idea what this is
            throw new NotImplementedException();
        }

    }
}
