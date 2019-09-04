using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IOnlineMeetingInvitationCustomizationResource : IResourceBase
    {
        OnlineMeetingInvitationCustomizationLinks _links { get; set; }
        string enterpriseHelpUrl { get; set; }
        string invitationFooterText { get; set; }
        string invitationHelpUrl { get; set; }
        string invitationLegalUrl { get; set; }
        string invitationLogoUrl { get; set; }
        Task<IOnlineMeetingInvitationCustomizationResource> Get();
        new Task<IOnlineMeetingInvitationCustomizationResource> Get(string resourceUrl);
    }

    public class OnlineMeetingInvitationCustomizationLinks
    {
        public Link self;
    }
}
