using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantInvitationResource : ResourceBase, IParticipantInvitationResource
    {
        public InvitationDirection? direction { get; set; }
        public Importance? importance { get; set; }
        public string message { get; set; }
        public string operationId { get; set; }
        public InvitationState? state { get; set; }
        public string subject { get; set; }
        public string threadId { get; set; }
        public string to { get; set; }
        public ParticipantInvitationLinks _links { get; set; }
        public ParticipantInvitationEmbedded _embedded { get; set; }
        public List<ParticipantResource> acceptedByParticipant { get { return _embedded.acceptedByParticipant; } }
        public ParticipantResource from { get { return _embedded.from; } }

        private void initializeProperties()
        {
            direction = null;
            importance = null;
            message = null;
            operationId = null;
            state = null;
            subject = null;
            threadId = null;
            to = null;
            _links = new ParticipantInvitationLinks();
            _embedded = new ParticipantInvitationEmbedded();
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

        public ParticipantInvitationResource()
        {
            initializeProperties();
        }

        public ParticipantInvitationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IParticipantInvitationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IParticipantInvitationResource> Get()
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

        public async Task cancel()
        {
            if (httpUtility != null && _links.cancel != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.cancel.href);
            }
        }

        public async Task<IConversationResource> conversation()
        {
            if (httpUtility != null && _links.conversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else return null;
        }

        public async Task<IParticipantResource> fromParticipant()
        {
            if (httpUtility != null && _links.from.href != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.from.href);
                return participantResource;
            }
            else
                return null;
        }

        public async Task<IParticipantResource> participant()
        {
            if (httpUtility != null && _links.from.href != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.participant.href);
                return participantResource;
            }
            else
                return null;
        }

        public async Task<IContactResource> toContact()
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
