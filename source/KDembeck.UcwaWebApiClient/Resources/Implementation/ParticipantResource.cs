using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantResource : ResourceBase, IParticipantResource
    {
        public bool? anonymous { get; set; }
        public bool? inLobby { get; set; }
        public bool? local { get; set; }
        public string name { get; set; }
        public bool? organizer { get; set; }
        public string otherPhoneNumber { get; set; }
        public ParticipantRole? role { get; set; }
        public SourceNetwork? sourceNetwork { get; set; }
        public string uri { get; set; }
        public string workPhoneNumber { get; set; }
        public ParticipantLinks _links { get; set; }

        public ParticipantResource()
        {
            initializeProperties();
        }

        public ParticipantResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            anonymous = null;
            inLobby = null;
            local = null;
            name = null;
            organizer = null;
            otherPhoneNumber = null;
            role = null;
            sourceNetwork = null;
            uri = null;
            workPhoneNumber = null;
            _links = new ParticipantLinks();
        }

        public new async Task<IParticipantResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task admit()
        {
            if (httpUtility != null && _links.admit != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.admit.href);
            }
        }

        public async Task<IContactResource> getContact()
        {
            if (httpUtility != null && _links.contact != null)
            {
                ContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.contact.href);
                return contactResource;
            }
            else return null;
        }

        public async Task<Stream> getContactPhoto()
        {
            if (httpUtility != null && _links.contactPhoto != null)
            {
                Stream contactPhotoStream = null;
                contactPhotoStream = await httpUtility.httpGetImageJpeg(httpUtility.baseUrl + _links.contactPhoto.href);
                return contactPhotoStream;
            }
            else return null;
        }

        public async Task<IContactPresenceResource> getContactPresence()
        {
            if (httpUtility != null && _links.contactPresence != null)
            {
                IContactPresenceResource contactPresenceResource = new ContactPresenceResource(httpUtility);
                await contactPresenceResource.Get(httpUtility.baseUrl + _links.contactPresence.href);
                return contactPresenceResource;
            }
            else return null;
        }

        public async Task<IConversationResource> getConversation()
        {
            if (httpUtility != null && _links.contact != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else return null;
        }

        public async Task demote()
        {
            if (httpUtility != null && _links.demote != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.demote.href);
            }
        }

        public async Task eject()
        {
            if (httpUtility != null && _links.eject != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.eject.href);
            }
        }

        public async Task<IMeResource> getMeResource()
        {
            if (httpUtility != null && _links.me != null)
            {
                IMeResource meResource = new MeResource(httpUtility);
                await meResource.Get(httpUtility.baseUrl + _links.me.href);
                return meResource;
            }
            else return null;
        }

        public async Task<IParticipantApplicationSharingResource> getParticipantApplicationSharing()
        {
            if (httpUtility != null && _links.participantApplicationSharing != null)
            {
                IParticipantApplicationSharingResource participantApplicationSharingResource = new ParticipantApplicationSharingResource(httpUtility);
                await participantApplicationSharingResource.Get(httpUtility.baseUrl + _links.participantApplicationSharing.href);
                return participantApplicationSharingResource;
            }
            else
                return null;
        }

        public async Task<IParticipantAudioResource> getParticipantAudio()
        {
            if (httpUtility != null && _links.participantAudio != null)
            {
                IParticipantAudioResource participantAudioResource = new ParticipantAudioResource(httpUtility);
                await participantAudioResource.Get(httpUtility.baseUrl + _links.participantAudio.href);
                return participantAudioResource;
            }
            else
                return null;
        }

        public async Task<IParticipantDataCollaborationResource> getParticipantDataCollaboration()
        {
            if (httpUtility != null && _links.participantDataCollaboration != null)
            {
                IParticipantDataCollaborationResource participantDataCollaborationResource = new ParticipantDataCollaborationResource(httpUtility);
                await participantDataCollaborationResource.Get(httpUtility.baseUrl + _links.participantDataCollaboration.href);
                return participantDataCollaborationResource;
            }
            else
                return null;
        }

        public async Task<IParticipantMessagingResource> getParticipantMessaging()
        {
            if (httpUtility != null && _links.participantMessaging != null)
            {
                IParticipantMessagingResource participantMessagingResource = new ParticipantMessagingResource(httpUtility);
                await participantMessagingResource.Get(httpUtility.baseUrl + _links.participantMessaging.href);
                return participantMessagingResource;
            }
            else
                return null;
        }

        public async Task<IParticipantPanoramicVideoResource> getParticipantPanoramicVideo()
        {
            if (httpUtility != null && _links.participantMessaging != null)
            {
                IParticipantPanoramicVideoResource participantPanoramicVideoResource = new ParticipantPanoramicVideoResource(httpUtility);
                await participantPanoramicVideoResource.Get(httpUtility.baseUrl + _links.participantPanoramicVideo.href);
                return participantPanoramicVideoResource;
            }
            else
                return null;
        }

        public async Task<IParticipantVideoResource> getParticipantVideo()
        {
            if (httpUtility != null && _links.participantVideo != null)
            {
                IParticipantVideoResource participantVideoResource = new ParticipantVideoResource(httpUtility);
                await participantVideoResource.Get(httpUtility.baseUrl + _links.participantVideo.href);
                return participantVideoResource;
            }
            else
                return null;
        }

        public async Task promote()
        {
            if (httpUtility != null && _links.promote != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.promote.href);
            }
        }

        public async Task reject()
        {
            if (httpUtility != null && _links.reject != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.reject.href);
            }
        }
    }
}
