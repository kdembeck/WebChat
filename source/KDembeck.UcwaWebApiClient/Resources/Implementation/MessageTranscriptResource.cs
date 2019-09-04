using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MessageTranscriptResource : ResourceBase, IMessageTranscriptResource
    {
        public string htmlMessage { get; set; }
        public string plainMessage { get; set; }
        public MessageTranscriptLinks _links { get; set; }

        private void initializeProperties()
        {
            htmlMessage = null;
            plainMessage = null;
            _links = new MessageTranscriptLinks();
        }

        public MessageTranscriptResource()
        {
            initializeProperties();
        }

        public MessageTranscriptResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IMessageTranscriptResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IMessageTranscriptResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
