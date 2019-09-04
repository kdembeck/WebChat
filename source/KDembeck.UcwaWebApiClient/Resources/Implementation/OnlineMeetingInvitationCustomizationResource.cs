using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingInvitationCustomizationResource : ResourceBase, IOnlineMeetingInvitationCustomizationResource
    {
        public OnlineMeetingInvitationCustomizationLinks _links { get; set; }
        public string enterpriseHelpUrl { get; set; }
        public string invitationFooterText { get; set; }
        public string invitationHelpUrl { get; set; }
        public string invitationLegalUrl { get; set; }
        public string invitationLogoUrl { get; set; }

        public OnlineMeetingInvitationCustomizationResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public OnlineMeetingInvitationCustomizationResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new OnlineMeetingInvitationCustomizationLinks();
            enterpriseHelpUrl = null;
            invitationFooterText = null;
            invitationHelpUrl = null;
            invitationLegalUrl = null;
            invitationLogoUrl = null;
        }

        public async Task<IOnlineMeetingInvitationCustomizationResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IOnlineMeetingInvitationCustomizationResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
