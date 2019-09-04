using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ImmediateForwardSettingsResource : ResourceBase, IImmediateForwardSettingsResource
    {
        public string target { get; set; }
        //public IContactResource contact { get; set; }
        public ImmediateForwardSettingsLinks _links { get; set; }

        public ImmediateForwardSettingsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public ImmediateForwardSettingsResource()
        {   
            initializeProperties();
        }

        private void initializeProperties()
        {
            target = null;
            _links = new ImmediateForwardSettingsLinks();
        }

        //public async Task initializeResources()
        //{
        //    contact = await getContact();
        //}

        public async Task<IImmediateForwardSettingsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                //await initializeResources();
            }
            return this;
        }

        public new async Task<IImmediateForwardSettingsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                //await initializeResources();
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

        public async Task immediateForwardToContact(string targetUri)
        {
            if (httpUtility != null && _links.immediateForwardToContact != null)
            {
                string immediateForwardToContactJson = JsonConvert.SerializeObject(new
                {
                    target = targetUri
                });
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.immediateForwardToContact.href + "?target=" + target, immediateForwardToContactJson);
            }
        }

        public async Task immediateForwardToDelegates()
        {
            if (httpUtility != null && _links.immediateForwardToDelegates != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.immediateForwardToDelegates.href);
            }
        }

        public async Task immediateForwardToVoicemail()
        {
            if (httpUtility != null && _links.immediateForwardToVoicemail != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.immediateForwardToVoicemail.href);
            }
        }
    }
}
