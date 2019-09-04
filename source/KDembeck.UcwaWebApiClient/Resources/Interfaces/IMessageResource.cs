using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMessageResource : IResourceBase
    {
        string direction { get; set; }
        string htmlMessage { get; }
        string status { get; set; }
        string plainMessage { get; }
        string timeStamp { get; set; }
        string operationId { get; set; }
        MessageLinks _links { get; set; }
        new Task<IMessageResource> Get(string resourceUrl);
        Task<IMessageResource> Get();
        Task<IContactResource> getContact();
        Task<List<IParticipantResource>> getFailedDeliveryParticipants();
        Task<IParticipantResource> getParticipant();
        Task<IMessagingResource> getMessaging();
    }

    public class MessageLinks
    {
        public Link self;
        public Link contact;
        public List<ParticipantLink> failedDeliveryParticipant;
        public ParticipantLink participant;
        public Link messaging;
        public Link plainMessage;
        public Link htmlMessage;

        public MessageLinks()
        {
            failedDeliveryParticipant = new List<ParticipantLink>();
        }
    }
}
