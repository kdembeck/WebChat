using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ApplicationSharingResource : ResourceBase, IApplicationSharingResource
    {
        public ConnectionState? state { get; set; }
        public ApplicationSharingLinks _links { get; set; }

        private void initializeProperties()
        {
            state = null;
            _links = new ApplicationSharingLinks();
        }

        public ApplicationSharingResource()
        {
            initializeProperties();
        }

        public ApplicationSharingResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IApplicationSharingResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IApplicationSharingResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantResource> getApplicationSharer()
        {
            if (httpUtility != null && _links.applicationSharer != null)
            {
                IParticipantResource participantResource = new ParticipantResource(httpUtility);
                await participantResource.Get(httpUtility.baseUrl + _links.applicationSharer.href);
                return participantResource;
            }
            else
                return null;
        }

        public async Task<IConversationResource> getConversation()
        {
            if (httpUtility != null && _links.conversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else
                return null;
        }

        public async Task reportMediaDiagnostics(ErrorCode? errorCode = null, ErrorSubcode? errorSubcode = null)
        {
            if (httpUtility != null && _links.reportMediaDiagnostics != null)
            {
                dynamic reprotMediaDiagnosticsSettings = new ExpandoObject();
                if (errorCode != null)
                    reprotMediaDiagnosticsSettings.errorCode = errorCode;
                if (errorSubcode != null)
                    reprotMediaDiagnosticsSettings.errorSubcode = errorSubcode;
                string reportMediaDiagnosticsJson = JsonConvert.SerializeObject(reprotMediaDiagnosticsSettings, new Newtonsoft.Json.Converters.StringEnumConverter());

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.reportMediaDiagnostics.href, reportMediaDiagnosticsJson);
            }
        }

    }  
}
