using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MessageResource : ResourceBase, IMessageResource
    {
        public string direction { get; set; }
        public string htmlMessage { get { return _links.htmlMessage.href; } }
        public string status { get; set; }
        public string plainMessage { get { return _links.plainMessage.href; } }
        public string timeStamp { get; set; }
        public string operationId { get; set; }
        public MessageLinks _links { get; set; }

        private void initializeProperties()
        {
            direction = null;
            status = null;
            timeStamp = null;
            operationId = null; 
            _links = new MessageLinks();
        }

        public MessageResource()
        {
            initializeProperties();
        }

        public MessageResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IMessageResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IMessageResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IContactResource> getContact()
        {
            if (httpUtility != null && _links.contact != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.contact.href);
                return contactResource;
            }
            else return null;
        }

        public async Task<List<IParticipantResource>> getFailedDeliveryParticipants()
        {
            if (httpUtility != null && _links.failedDeliveryParticipant != null && _links.failedDeliveryParticipant.Count > 0)
            {
                List<IParticipantResource> failedDeliveryParticipantsList = new List<IParticipantResource>();
                foreach (ParticipantLink participantLink in _links.failedDeliveryParticipant)
                {
                    IParticipantResource participant = new ParticipantResource(httpUtility);
                    await participant.Get(httpUtility.baseUrl + participantLink.href);
                    failedDeliveryParticipantsList.Add(participant);
                }
                return failedDeliveryParticipantsList;
            }
            else
                return null;
        }

        public async Task<IParticipantResource> getParticipant()
        {
            if (httpUtility != null && _links.participant != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.participant.href);
                return participantResource;
            }
            else
                return null;
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
    }
}
