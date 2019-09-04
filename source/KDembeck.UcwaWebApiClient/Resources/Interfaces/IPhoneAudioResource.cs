using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPhoneAudioResource : IResourceBase
    {
        string state { get; set; }
        PhoneAudioLinks _links { get; set; }
        new Task<IPhoneAudioResource> Get(string resourceUrl);
        Task<IPhoneAudioResource> Get();
        Task addPhoneAudio(string toUri, string phoneNumber = null, string operationId = null);
        Task<IConversationResource> getConversation();
        Task holdPhoneAudio();
        Task resumePhoneAudio();
        Task stopPhoneAudio();
    }

    public class PhoneAudioLinks
    {
        public Link self;
        public Link addPhoneAudio;
        public Link conversation;
        public Link holdPhoneAudio;
        public Link resumePhoneAudio;
        public Link stopPhoneAudio;
    }
}
