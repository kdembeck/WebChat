using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class UnansweredCallSettingsResource : ResourceBase, IUnansweredCallSettingsResource
    {
        public int ringDelay { get; set; }
        public string target { get; set; }
        //public ContactResource contact { get; set; }
        public UnansweredCallSettingsLinks _links { get; set; }

        public UnansweredCallSettingsResource()
        {
            initializeProperties();
        }

        public UnansweredCallSettingsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            ringDelay = -1;
            target = null;
            _links = new UnansweredCallSettingsLinks();
            //contact = null; 
        }

        //public async Task initializeResources()
        //{
        //    contact = await getContact();
        //}
                
        public async Task<IUnansweredCallSettingsResource> Get()
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

        public new async Task<IUnansweredCallSettingsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                //await initializeResources();
            }
            return this;
        }

        public async Task Update(int ringDelay, string target)
        {
            if (httpUtility != null && _links.self != null)
            {
                if (ringDelay > 60)
                    ringDelay = 60;
                else if (ringDelay < 0)
                    ringDelay = 0;

                string unansweredCallSettingsJson = JsonConvert.SerializeObject(new
                {
                    ringDelay = ringDelay,
                    target = target
                });
                await httpUtility.httpPutJson(httpUtility.baseUrl + _links.self.href, unansweredCallSettingsJson);
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

        public async Task resetUnansweredCallSettings()
        {
            if (httpUtility != null && _links.resetUnansweredCallSettings != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.resetUnansweredCallSettings.href);
            }
        }

        public async Task unansweredCallToContact(int ringDelaySeconds, string targetUri)
        {
            if (httpUtility != null && _links.unansweredCallToContact != null)
            {
                if (ringDelaySeconds > 60)
                    ringDelaySeconds = 60;
                else if (ringDelaySeconds < 0)
                    ringDelaySeconds = 0;

                string unansweredCallToContactJson = JsonConvert.SerializeObject(new
                {
                    ringDelay = ringDelaySeconds,
                    target = targetUri
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.unansweredCallToContact.href, unansweredCallToContactJson);
            }
        }

        public async Task unansweredCallToVoicemail(int ringDelaySeconds)
        {
            if (httpUtility != null && _links.unansweredCallToVoicemail != null)
            {
                if (ringDelaySeconds > 60)
                    ringDelaySeconds = 60;
                else if (ringDelaySeconds < 0)
                    ringDelaySeconds = 0;

                string unansweredCallToVoicemailJson = JsonConvert.SerializeObject(new
                {
                    ringDelay = ringDelaySeconds                    
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.unansweredCallToContact.href + "?ringDelay=" + ringDelaySeconds.ToString(), unansweredCallToVoicemailJson);
            }
        }
    }
}
