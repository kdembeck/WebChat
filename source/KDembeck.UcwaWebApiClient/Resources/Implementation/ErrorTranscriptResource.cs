using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ErrorTranscriptResource : ResourceBase, IErrorTranscriptResource
    {
        public string reason { get; set; }
        public ErrorTranscriptLinks _links { get; set; }

        private void initializeProperties()
        {
            reason = null;
            _links = new ErrorTranscriptLinks(); 
        }

        public ErrorTranscriptResource()
        {
            initializeProperties();
        }

        public ErrorTranscriptResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IErrorTranscriptResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IErrorTranscriptResource> Get()
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
