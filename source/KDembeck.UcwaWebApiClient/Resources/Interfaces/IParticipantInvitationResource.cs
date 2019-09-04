using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantInvitationResource : IResourceBase
    {
        InvitationDirection? direction { get; set; }
        Importance? importance { get; set; }
        string message { get; set; }
        string operationId { get; set; }
        InvitationState? state { get; set; }
        string subject { get; set; }
        string threadId { get; set; }
        string to { get; set; }
        ParticipantInvitationLinks _links { get; set; }
        ParticipantInvitationEmbedded _embedded { get; set; }
        List<ParticipantResource> acceptedByParticipant { get; }
        ParticipantResource from { get; }
        new Task<IParticipantInvitationResource> Get(string resourceUrl);
        Task<IParticipantInvitationResource> Get();
        Task cancel();
        Task<IConversationResource> conversation();
        Task<IParticipantResource> fromParticipant();
        Task<IParticipantResource> participant();
        Task<IContactResource> toContact();
    }

    public class ParticipantInvitationLinks
    {
        public Link self;
        public Link cancel;
        public Link conversation;
        public Link from;
        public Link participant;
        public Link to;
    }

    public class ParticipantInvitationEmbedded
    {
        public List<ParticipantResource> acceptedByParticipant;
        public ParticipantResource from;

        public ParticipantInvitationEmbedded()
        {
            acceptedByParticipant = new List<ParticipantResource>();
        }
    }
}
