using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantResource : IResourceBase
    {
        bool? anonymous { get; set; }
        bool? inLobby { get; set; }
        bool? local { get; set; }
        string name { get; set; }
        bool? organizer { get; set; }
        string otherPhoneNumber { get; set; }
        ParticipantRole? role { get; set; }
        SourceNetwork? sourceNetwork { get; set; }
        string uri { get; set; }
        string workPhoneNumber { get; set; }
        ParticipantLinks _links { get; set; }
        new Task<IParticipantResource> Get(string resourceUrl);
        Task<IParticipantResource> Get();
        Task admit();
        Task<IContactResource> getContact();
        Task<Stream> getContactPhoto();
        Task<IContactPresenceResource> getContactPresence();
        Task<IConversationResource> getConversation();
        Task demote();
        Task eject();
        Task<IMeResource> getMeResource();
        Task<IParticipantApplicationSharingResource> getParticipantApplicationSharing();
        Task<IParticipantAudioResource> getParticipantAudio();
        Task<IParticipantDataCollaborationResource> getParticipantDataCollaboration();
        Task<IParticipantMessagingResource> getParticipantMessaging();
        Task<IParticipantPanoramicVideoResource> getParticipantPanoramicVideo();
        Task<IParticipantVideoResource> getParticipantVideo();
        Task promote();
        Task reject();
    }

    public class ParticipantLinks
    {
        public Link self;
        public Link admit;
        public Link contact;
        public Link contactPhoto;
        public Link contactPresence;
        public Link conversation;
        public Link demote;
        public Link eject;
        public Link me;
        public Link participantApplicationSharing;
        public Link participantAudio;
        public Link participantDataCollaboration;
        public Link participantMessaging;
        public Link participantPanoramicVideo;
        public Link participantVideo;
        public Link promote;
        public Link reject;
    }
}
