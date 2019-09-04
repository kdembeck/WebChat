using System.Collections.Generic;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IConversationLogResource
    {
        string creationTime { get; set; }
        string direction { get; set; }
        Importance? importance { get; set; }
        List<ModalityType> modalities { get; set; }
        string onlineMeetingUri { get; set; }
        string previewMessage { get; set; }
        string status { get; set; }
        string subject { get; set; }
        string threadId { get; set; }
        int totalRecipientsCount { get; set; }
        string type { get; set; }
        ConversationLogLinks _links { get; set; }
        ConversationLogEmbedded _embedded { get; set; }
        Task<IConversationLogResource> Get(string resourceUrl);
        Task<IConversationLogResource> Get();
        Task Delete();
        Task<IAudioVideoInvitationResource> continueAudio(string operationId = null, string mediaOffer = null, string sessionContext = null);
        Task continueMessaging(string message = null, string operationId = null);
        Task continuePhoneAudio(string phoneNumber = null, string operationId = null);
        Task continueVideo();
        Task<IConversationLogTranscriptsResource> getConversationLogTrasncripts();
        Task markAsRead();
    }

    public class ConversationLogLinks
    {
        public Link self;
        public Link continueAudio;
        public Link continueAudioVideo;
        public Link continueMessaging;
        public Link continuePhoneAudio;
        public Link continueVideo;
        public Link conversationLogTranscripts;
        public Link markAsRead;
    }

    public class ConversationLogEmbedded
    {
        //not sure how to get around declaring this as a concrete type when using JsonConvert to populate the object
        public IEnumerable<ConversationLogRecipientResource> conversationLogRecipient;
        public ConversationLogEmbedded()
        {
            conversationLogRecipient = new List<ConversationLogRecipientResource>();
        }
    }
}
