using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPhoneAudioInvitationResource : IResourceBase
    {
        string customContent { get; set; }
        string delegator { get; set; }
        InvitationDirection? direction { get; set; }
        Importance? importance { get; set; }
        bool? joinAudioMuted { get; set; }
        string operationId { get; set; }
        string phoneNumber { get; set; }
        bool? privateLine { get; set; }
        ConnectionState? state { get; set; }
        string subject { get; set; }
        string threadId { get; set; }
        string to { get; set; }
        PhoneAudioInvitationLinks _links { get; set; }
        PhoneAudioInvitationEmbedded _embedded { get; set; }
        List<ParticipantResource> acceptedByParticipant { get; }
        ParticipantResource from { get; }
        new Task<IPhoneAudioInvitationResource> Get(string resourceUrl);
        Task<IPhoneAudioInvitationResource> Get();
        Task accept();
        Task<IContactResource> getAcceptedByContact();
        Task cancel();
        Task<IConversationResource> getConversation();
        Task decline();
        Task<IContactResource> getDelegator();
        Task<IConversationResource> getDerivedConversation();
        Task<IPhoneAudioResource> getDerivedPhoneAudio();
        Task<IContactResource> getForwardedBy();
        Task<IParticipantResource> getFrom();
        Task<IContactResource> getOnBehalfOf();
        Task<IPhoneAudioResource> getPhoneAudio();
        Task getReplacesPhoneAudio();
        Task<IContactResource> getTo();
        Task<IContactResource> getTrasnferredBy();
    }


    public class PhoneAudioInvitationLinks
    {
        public Link self;
        public Link accept;
        public Link acceptedByContact;
        public Link cancel;
        public Link conversation;
        public Link decline;
        public Link delegator;
        public Link derivedConversation;
        public Link derivedPhoneAudio;
        public Link forwardedBy;
        public Link from;
        public Link onBehalfOf;
        public Link phoneAudio;
        public Link replacesPhoneAudio;
        public Link to;
        public Link transferredBy;
    }

    public class PhoneAudioInvitationEmbedded
    {
        public List<ParticipantResource> acceptedByParticipant;
        public ParticipantResource from;
    }
}
