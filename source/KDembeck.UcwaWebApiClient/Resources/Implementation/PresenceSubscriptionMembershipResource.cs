using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PresenceSubscriptionMembershipResource : ResourceBase, IPresenceSubscriptionMembershipResource
    {
        public PresenceSubscriptionMembershipLinks _links { get; set; }

        public PresenceSubscriptionMembershipResource()
        {
            initializeProperties();
        }

        public PresenceSubscriptionMembershipResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new PresenceSubscriptionMembershipLinks();
        }
        
        public async Task<IPresenceSubscriptionMembershipResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IPresenceSubscriptionMembershipResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
        
        public async Task removeContactFromPresenceSubscription()
        {
            if (httpUtility != null && _links.self != null)
            {
                await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
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

        public async Task<IPresenceSubscriptionResource> getPresenceSubscription()
        {
            if (httpUtility != null && _links.presenceSubscription != null)
            {
                IPresenceSubscriptionResource presenceSubscriptionResource = new PresenceSubscriptionResource(httpUtility);
                await presenceSubscriptionResource.Get(httpUtility.baseUrl + _links.presenceSubscription.href);
                return presenceSubscriptionResource;
            }
            else
                return null;
        }

    }
}
