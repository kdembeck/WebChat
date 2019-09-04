using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IMessagingResource : IResourceBase
    {
        List<MessageFormat> negotiatedMessageFormats { get; set; }
        string state { get; set; }
        MessagingLinks _links { get; set; }
        new Task<IMessagingResource> Get(string resourceUrl);
        Task<IMessagingResource> Get();
        Task addMessaging(string operationId = null);
        Task<IConversationResource> getConversation();
        Task sendMessageHtml(string htmlMessage);
        Task sendMessageHtml(string htmlMessage, string operationId);
        Task sendMessagePlainText(string message);
        Task sendMessagePlainText(string message, string operationId);
        Task setIsTyping();
        Task stopMessaging();
        Task getTypingParticipants();
    }

    public class MessagingLinks
    {
        public Link self;
        public Link addMessaging;
        public Link conversation;
        public Link sendMessage;
        public Link setIsTyping;
        public Link stopMessaging;
        public Link typingParticipants;
    }
}
