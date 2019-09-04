using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMessagingInvitationResource : IResourceBase
    {
        string customContent { get; set; }
        string direction { get; set; }
        string importance { get; set; }
        string message { get; set; }
        string operationId { get; set; }
        InvitationState? state { get; set; }
        string subject { get; set; }
        string threadId { get; set; }
        string to { get; set; }
        MessagingInvitationLinks _links { get; set; }
        MessagingInvitationEmbedded _embedded { get; set; }
        List<ParticipantResource> acceptedByParticipant { get; }
        ParticipantResource from { get; }
        new Task<IMessagingInvitationResource> Get(string resourceUrl);
        Task<IMessagingInvitationResource> Get();
        Task accept();
        Task<IContactResource> getAcceptedByContact();
        Task cancel();
        Task<IConversationResource> getConversationResource();
        Task decline();
        Task<IMessagingResource> getDerivedMessagingResource();
        Task<IParticipantResource> getFromParticipant();
        Task<IMessagingResource> getMessagingResource();
        Task<IContactResource> getOnBehalfOfContact();
        Task<IContactResource> getToContact();
    }

    public class MessagingInvitationLinks
    {
        public Link self;
        public Link accept;
        public Link acceptedByContact;
        public Link cancel;
        public Link conversation;
        public Link decline;
        public Link derivedMessaging;
        public Link from;
        public Link messaging;
        public Link onBehalfOf;
        public Link to;
    }

    public class MessagingInvitationEmbedded
    {
        public List<ParticipantResource> acceptedByParticipant;
        public ParticipantResource from;
    }
}
