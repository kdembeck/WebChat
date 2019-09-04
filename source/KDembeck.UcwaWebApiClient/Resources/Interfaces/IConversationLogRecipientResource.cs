using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IConversationLogRecipientResource
    {
        string displayName { get; set; }
        string sipUri { get; set; }
        ConversationLogRecipientLinks _links { get; set; }
        //IContactResource contact { get; set; }
        //Stream contactPhoto { get; set; }
        //IContactPresenceResource contactPresence { get; set; }
        Task<IConversationLogRecipientResource> Get(string resourceUrl);
        Task<IConversationLogRecipientResource> Get();
        Task<IContactResource> getContact();
        Task<Stream> getContactPhoto();
        Task<IContactPresenceResource> getContactPresence();
    }

    public class ConversationLogRecipientLinks
    {
        public Link self;
        public Link contact;
        public Link contactPhoto;
        public Link contactPresence;
    }
}
