using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MessagingInvitationResource : ResourceBase, IMessagingInvitationResource
    {

        public string customContent { get; set; }
        public string direction { get; set; }
        public string importance { get; set; }
        public string message { get; set; }
        public string operationId { get; set; }
        public InvitationState? state { get; set; }
        public string subject { get; set; }
        public string threadId { get; set; }
        public string to { get; set; }
        public MessagingInvitationLinks _links { get; set; }
        public MessagingInvitationEmbedded _embedded { get; set; }
        public List<ParticipantResource> acceptedByParticipant { get { return _embedded.acceptedByParticipant; } }
        public ParticipantResource from { get { return _embedded.from; } }

        private void initializeProperties()
        {
            customContent = null;
            direction = null;
            importance = null;
            message = null;
            operationId = null;
            state = null;
            subject = null;
            threadId = null;
            to = null;
            _links = new MessagingInvitationLinks();
            _embedded = new MessagingInvitationEmbedded();
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

        public MessagingInvitationResource()
        {
            initializeProperties();
        }

        public MessagingInvitationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IMessagingInvitationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IMessagingInvitationResource> Get()
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
            else return null;
        }

        public async Task cancel()
        {
            if (httpUtility != null && _links.cancel != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.cancel.href);
            }
        }

        public async Task<IConversationResource> getConversationResource()
        {
            if (httpUtility != null && _links.conversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else return null;
        }

        public async Task decline()
        {
            if (httpUtility != null && _links.decline != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.decline.href);
            }
        }

        public async Task<IMessagingResource> getDerivedMessagingResource()
        {
            if (httpUtility != null && _links.derivedMessaging != null)
            {
                IMessagingResource messagingResource = new MessagingResource(httpUtility);
                await messagingResource.Get(httpUtility.baseUrl + _links.derivedMessaging.href);
                return messagingResource;
            }
            else return null;
        }

        public async Task<IParticipantResource> getFromParticipant()
        {
            if (httpUtility != null && _links.from != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.from.href);
                return participantResource;
            }
            else return null;
        }

        public async Task<IMessagingResource> getMessagingResource()
        {
            if (httpUtility != null && _links.messaging != null)
            {
                IMessagingResource messagingResource = new MessagingResource(httpUtility);
                await messagingResource.Get(httpUtility.baseUrl + _links.messaging.href);
                return messagingResource;
            }
            else return null;
        }

        public async Task<IContactResource> getOnBehalfOfContact()
        {
            if (httpUtility != null && _links.onBehalfOf != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.onBehalfOf.href);
                return contactResource;
            }
            else return null;
        }

        public async Task<IContactResource> getToContact()
        {
            if (httpUtility != null && _links.to != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.to.href);
                return contactResource;
            }
            else return null;
        }

    }
}
