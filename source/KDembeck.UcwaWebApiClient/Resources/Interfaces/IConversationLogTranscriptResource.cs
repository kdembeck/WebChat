using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IConversationLogTranscriptResource : IResourceBase
    {
        string timeStamp{get;set;}
        ConversationLogTranscriptLinks _links { get; set; }
        ConversationLogTranscriptEmbedded _embedded { get; set; }
        IAudioTranscriptResource audioTranscript { get; }
        IErrorTranscriptResource errorTranscript { get; }
        IMessageTranscriptResource messageTranscript { get; }
        Task<IConversationLogTranscriptResource> Get(string resourceUrl);
        Task<IConversationLogTranscriptResource> Get();
        Task<IContactResource> getContact();
        Task<IMeResource> getMe();
    }

    public class ConversationLogTranscriptLinks
    {
        public Link self;
        public Link contact;
        public Link me;
    }

    public class ConversationLogTranscriptEmbedded
    {
        public IAudioTranscriptResource audioTranscript;
        public IErrorTranscriptResource errorTranscript;
        public IMessageTranscriptResource messageTranscript;

        public ConversationLogTranscriptEmbedded()
        {
            audioTranscript = new AudioTranscriptResource();
            errorTranscript = new ErrorTranscriptResource();
            messageTranscript = new MessageTranscriptResource();
        }
    }
}
