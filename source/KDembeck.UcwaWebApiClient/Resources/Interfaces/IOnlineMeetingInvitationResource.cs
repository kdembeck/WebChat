using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingInvitationResource : IResourceBase
    {
        string anonymousDisplayName { get; set; }
        List<ModalityType> availableModalities { get; set; }
        InvitationDirection? direction { get; set; }
        Importance? importance { get; set; }
        string onlineMeetingUri { get; set; }
        string message { get; set; }
        string operationId { get; set; }
        InvitationState? state { get; set; }
        string subject { get; set; }
        string threadId { get; set; }
        string to { get; set; }
        OnlineMeetingInvitationLinks _links { get; set; }
        OnlineMeetingInvitationEmbedded _embeded { get; set; }
        ParticipantResource from { get; }
        new Task<IOnlineMeetingInvitationResource> Get(string resourceUrl);
        Task<IOnlineMeetingInvitationResource> Get();
        Task accept();
        Task<IContactResource> getAcceptedByContact();
        Task cancel();
        Task<IConversationResource> getConversation();
        Task decline();
        Task<IParticipantResource> getFromParticipant();
        Task<IContactResource> getOnBehalfOfContact();
    }

    public class OnlineMeetingInvitationLinks
    {
        public Link self;
        public Link accept;
        public Link acceptedByContact;
        public Link cancel;
        public Link conversation;
        public Link decline;
        public Link from;
        public Link onBehalfOf;
        public Link to;
    }

    public class OnlineMeetingInvitationEmbedded
    {
        public ParticipantResource from;
    }
}
