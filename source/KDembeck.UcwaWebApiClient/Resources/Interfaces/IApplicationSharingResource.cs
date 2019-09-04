using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IApplicationSharingResource
    {
        ConnectionState? state { get; }
        ApplicationSharingLinks _links { get; }
        Task<IApplicationSharingResource> Get(string resourceUrl);
        Task<IApplicationSharingResource> Get();
        Task<IParticipantResource> getApplicationSharer();
        Task<IConversationResource> getConversation();
        Task reportMediaDiagnostics(ErrorCode? errorCode = null, ErrorSubcode? errorSubcode = null);     
    }

    public class ApplicationSharingLinks
    {
        public Link self;
        public Link applicationSharer;
        public Link conversation;
        public Link reportMediaDiagnostics;
    }
}
