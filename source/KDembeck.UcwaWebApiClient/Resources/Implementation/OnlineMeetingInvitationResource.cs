using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingInvitationResource : ResourceBase, IOnlineMeetingInvitationResource
    {
        public string anonymousDisplayName { get; set; }
        public List<ModalityType> availableModalities { get; set; }
        public InvitationDirection? direction { get; set; }
        public Importance? importance { get; set; }
        public string onlineMeetingUri { get; set; }
        public string message { get; set; }
        public string operationId { get; set; }
        public InvitationState? state { get; set; }
        public string subject { get; set; }
        public string threadId { get; set; }
        public string to { get; set; }
        public OnlineMeetingInvitationLinks _links { get; set; }
        public OnlineMeetingInvitationEmbedded _embeded { get; set; }
        public ParticipantResource from { get { return _embeded.from; } }

        private void initializeProperties()
        {
            anonymousDisplayName = null;
            availableModalities = new List<ModalityType>();
            direction = null;
            importance = null;
            onlineMeetingUri = null;
            message = null;
            operationId = null;
            state = null;
            subject = null;
            threadId = null;
            to = null;
            _links = new OnlineMeetingInvitationLinks();
            _embeded = new OnlineMeetingInvitationEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embeded.from != null)
                {
                    _embeded.from.httpUtility = httpUtility;
                }
            }
        }

        public OnlineMeetingInvitationResource()
        {
            initializeProperties();
        }

        public OnlineMeetingInvitationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IOnlineMeetingInvitationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IOnlineMeetingInvitationResource> Get()
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

        public async Task decline()
        {
            if (httpUtility != null && _links.decline != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.decline.href);
            }
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
    }
}
