using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IConversationResource : IResourceBase
    {
        List<ModalityType> activeModalities { get; set; }
        string audienceMessaging { get; set; }
        string audienceMute { get; set; }
        string created { get; set; }
        string expirationTime { get; set; }
        string importance { get; set; }
        int? participantCount { get; set; }
        bool? readLocally { get; set; }
        bool? recording { get; set; }
        string state { get; set; }
        string subject { get; set; }
        string threadId { get; set; }
        ConversationLinks _links { get; set; }
        new Task<IConversationResource> Get(string resourceUrl);
        Task<IConversationResource> Get();
        Task Delete();
        Task addParticipant(string toUri, string operationId);
        Task addParticipant(string toUri);
        Task<IAttendeesResource> getAttendees();
        Task<IAudioVideoResource> getAudioVideo();
        Task<IDataCollaborationResource> getDataCollaboration();
        Task disableAudienceMessaging();
        Task disableAudienceMuteLock();
        Task enableAudienceMessaging();
        Task enableAudienceMuteLock();
        Task<ILeadersResource> getLeaders();
        Task<ILobbyResource> getLobby();
        Task<IParticipantResource> getLocalParticipant();
        Task<IMessagingResource> getMessaging();
        Task<IOnlineMeetingResource> getOnlineMeeting();
        Task<List<IParticipantResource>> getParticipants();
        Task<IPhoneAudioResource> getPhoneAudio();
        Task userAcknowledged();
    }

    public class ConversationLinks
    {
        public Link self;
        public Link addParticipant;
        public Link attendees;
        public Link audioVideo;
        public Link dataCollaboration;
        public Link disableAudienceMessaging;
        public Link disableAudienceMuteLock;
        public Link enableAudienceMessaging;
        public Link enableAudienceMuteLock;
        public Link leaders;
        public Link lobby;
        public Link localParticipant;
        public Link messaging;
        public Link onlineMeeting;
        public Link participants;
        public Link phoneAudio;
        public Link userAcknowledged;
    }
}
