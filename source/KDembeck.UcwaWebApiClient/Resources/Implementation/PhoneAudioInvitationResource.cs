using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PhoneAudioInvitationResource : ResourceBase, IPhoneAudioInvitationResource
    {
        public string customContent { get; set; }
        public string delegator { get; set; }
        public InvitationDirection? direction { get; set; }
        public Importance? importance { get; set; }
        public bool? joinAudioMuted { get; set; }
        public string operationId { get; set; }
        public string phoneNumber { get; set; }
        public bool? privateLine { get; set; }
        public ConnectionState? state { get; set; }
        public string subject { get; set; }
        public string threadId { get; set; }
        public string to { get; set; }
        public PhoneAudioInvitationLinks _links { get; set; }
        public PhoneAudioInvitationEmbedded _embedded { get; set; }
        public List<ParticipantResource> acceptedByParticipant { get { return _embedded.acceptedByParticipant; } }
        public ParticipantResource from { get { return _embedded.from; } }

        private void initializeProperties()
        {
            customContent = null;
            delegator = null;
            direction = null;
            importance = null;
            joinAudioMuted = null;
            operationId = null;
            phoneNumber = null;
            privateLine = null;
            state = null;
            subject = null;
            threadId = null;
            to = null;
            _links = new PhoneAudioInvitationLinks();
            _embedded = new PhoneAudioInvitationEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.acceptedByParticipant != null && _embedded.acceptedByParticipant.Count > 0)
                {
                    foreach (ParticipantResource participant in _embedded.acceptedByParticipant)
                    {
                        participant.httpUtility = httpUtility;
                    }
                }
                if (_embedded.from != null)
                {
                    _embedded.from.httpUtility = httpUtility;
                }
            }
        }

        public PhoneAudioInvitationResource()
        {
            initializeProperties();
        }

        public PhoneAudioInvitationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IPhoneAudioInvitationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IPhoneAudioInvitationResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task accept()
        {
            if (httpUtility != null && _links.accept != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.accept.href);
            }
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

        public async Task<IPhoneAudioResource> getDerivedPhoneAudio()
        {
            if (httpUtility != null && _links.derivedPhoneAudio != null)
            {
                IPhoneAudioResource phoneAudioResource = new PhoneAudioResource(httpUtility);
                await phoneAudioResource.Get(httpUtility.baseUrl + _links.derivedPhoneAudio.href);
                return phoneAudioResource;
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

        public async Task getReplacesPhoneAudio()
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
