using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ConversationLogTranscriptResource : ResourceBase, IConversationLogTranscriptResource
    {
        public string timeStamp { get; set; }
        public ConversationLogTranscriptLinks _links { get; set; }
        public ConversationLogTranscriptEmbedded _embedded { get; set; }
        public IAudioTranscriptResource audioTranscript { get { return _embedded.audioTranscript; } }
        public IErrorTranscriptResource errorTranscript { get { return _embedded.errorTranscript; } }
        public IMessageTranscriptResource messageTranscript { get { return _embedded.messageTranscript; } }

        private void initializeProperties()
        {
            timeStamp = null;
            _links = new ConversationLogTranscriptLinks();
            _embedded = new ConversationLogTranscriptEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.audioTranscript != null)
                    _embedded.audioTranscript.httpUtility = httpUtility;
                if (_embedded.errorTranscript != null)
                    _embedded.errorTranscript.httpUtility = httpUtility;
                if (_embedded.messageTranscript != null)
                    _embedded.messageTranscript.httpUtility = httpUtility;
            }
        }

        public ConversationLogTranscriptResource()
        {
            initializeProperties();
        }

        public ConversationLogTranscriptResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IConversationLogTranscriptResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IConversationLogTranscriptResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IContactResource> getContact()
        {
            if (httpUtility != null && _links.contact != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.contact.href);
                return contactResource;
            }
            else
                return null;
        }

        public async Task<IMeResource> getMe()
        {
            if (httpUtility != null && _links.me != null)
            {
                IMeResource meResource = new MeResource(httpUtility);
                await meResource.Get(httpUtility.baseUrl + _links.me.href);
                return meResource;
            }
            else
                return null;
        }
    }
}
