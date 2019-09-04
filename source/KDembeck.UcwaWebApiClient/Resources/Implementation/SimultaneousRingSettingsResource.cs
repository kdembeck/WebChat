using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class SimultaneousRingSettingsResource : ResourceBase, ISimultaneousRingSettingsResource
    {
        public int ringDelay { get; set; }
        public string target { get; set; }        
        public SimultaneousRingSettingsLinks _links { get; set; }

        public SimultaneousRingSettingsResource()
        {
            initializeProperties();
        }

        public SimultaneousRingSettingsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {   
            ringDelay = -1;
            target = null;
            _links = new SimultaneousRingSettingsLinks();
        }
        
        public async Task<ISimultaneousRingSettingsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<ISimultaneousRingSettingsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
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

        public async Task simultaneousRingToContact(string targetUri)
        {
            if (httpUtility != null && _links.simultaneousRingToContact != null)
            {
                string simultaneousRingToContactJson = JsonConvert.SerializeObject(new
                {
                    target = targetUri
                });
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.simultaneousRingToContact + "?target=" + targetUri, simultaneousRingToContactJson);
            }
        }

        public async Task simultaneousRingToDelegates(int ringDelay)
        {
            if (httpUtility != null && _links.simultaneousRingToDelegates != null)
            {
                if (ringDelay > 55)
                    ringDelay = 55;
                else if (ringDelay < 0)
                    ringDelay = 0;

                if (ringDelay > 0)
                {
                    string simultaneousRingToDelegatesJson = JsonConvert.SerializeObject(new
                    {
                        ringDelay = ringDelay
                    });
                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.simultaneousRingToDelegates.href + "?ringDelay=" + ringDelay.ToString(), simultaneousRingToDelegatesJson);
                }
            }
        }

        public async Task simultaneousRingToDelegates()
        {
            if (httpUtility != null && _links.simultaneousRingToDelegates != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.simultaneousRingToDelegates.href);
            }
        }

        public async Task simultaneousRingToTeam(int ringDelay)
        {
            if (httpUtility != null && _links.simultaneousRingToTeam != null)
            {
                if (ringDelay > 55)
                    ringDelay = 55;
                else if (ringDelay < 0)
                    ringDelay = 0;

                if (ringDelay > 0)
                {
                    string simultaneousRingToTeamJson = JsonConvert.SerializeObject(new
                    {
                        ringDelay = ringDelay
                    });
                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.simultaneousRingToTeam.href + "?ringDelay=" + ringDelay.ToString(), simultaneousRingToTeamJson);
                }
            }
        }

        public async Task simultaneousRingToTeam()
        {
            if (httpUtility != null && _links.simultaneousRingToTeam != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.simultaneousRingToTeam.href);
            }
        }
    }
}
