using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using KDembeck.UcwaWebApiClient.Utilities;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ApplicationResource : ResourceBase, IApplicationResource
    {        
        public string culture { get; set; }
        public string userAgent { get; set; }
        public string type { get; set; }
        public string endpointId { get; set; }
        public string instanceId { get; set; }
        public string id { get; set; }        
        public ApplicationLinks _links { get; set; }
        public ApplicationEmbedded _embedded { get; set; }

        //I don't want to have to access these through _embedded when using them
        public ICommunicationResource communication { get { return _embedded.communication; } }
        public IOnlineMeetingsResource onlineMeetings { get { return _embedded.onlineMeetings; } }
        public IMeResource me { get { return _embedded.me; } }
        public IPeopleResource people { get { return _embedded.people; } }        
        private EventChannelListener eventChannelListener;        

        public ApplicationResource(IHttpUtility HttpUtility)
        {   
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public ApplicationResource()
        {
            initializeProperties();            
        }

        private void initializeProperties()
        {
            type = null;
            culture = null;
            endpointId = null;
            id = null;
            instanceId = null;
            userAgent = null;
            _links = new ApplicationLinks();            
            _embedded = new ApplicationEmbedded();
        }

        public async Task initializeResources()
        {
            if (_embedded.communication != null && _embedded.communication.httpUtility == null)
            {
                _embedded.communication.httpUtility = httpUtility;
            }

            if (_embedded.onlineMeetings != null && _embedded.onlineMeetings.httpUtility == null)
            {
                _embedded.onlineMeetings.httpUtility = httpUtility;
            }

            if (_embedded.me != null && _embedded.me.httpUtility == null)
            {
                _embedded.me.httpUtility = httpUtility;                
            }

            if (_embedded.people != null && _embedded.people.httpUtility == null)
            {
                _embedded.people.httpUtility = httpUtility;
            }
        }

        public new async Task<IApplicationResource> Get(string ResourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(ResourceUrl);
                await initializeResources();
            }
            else
            {
                //raise an error                
            }
            return this;
        }

        public async Task<IApplicationResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                await initializeResources();                
            }
            else
            {
                //raise an error                
            }
            return this;
        }
        
        public async Task signOut()
        {
            if (httpUtility != null && _links.self != null)
            {
                await httpUtility.httpDelete(httpUtility.baseUrl + _links.self.href);
            }
        }

        public async Task reportMyNetwork(string chassisID, string clientNetworkType, string ip, string mac, string portID, string rssi, string subnetID, string wapBSSID)
        {
            string reportMyNetworkJson = JsonConvert.SerializeObject(new
            {
                chassisID = chassisID,
                clientNetworkType = clientNetworkType,
                ip = ip,
                mac = mac,
                portID = portID,
                rssi = rssi,
                subnetID = subnetID,
                wapBSSID = wapBSSID
            });

            await httpUtility.httpPostJson(httpUtility.baseUrl + _links.reportMyNetwork.href, reportMyNetworkJson);
        }

        //THIS LOOKS ALL FUCKED UP TO ME. I DON"T LIKE THIS HERE
        public void startEventChannel(IEventHandler eventHandler)
        {   
            eventChannelListener = new EventChannelListener(httpUtility);
            eventChannelListener.OnEventChannelListenerEventReceived += eventHandler.Handle_OnEventChannelListenerEventReceived;
            eventChannelListener.Start(httpUtility.baseUrl + _links.events.href);
        }

        public async Task<IPoliciesResource> getPolicies()
        {
            string policiesString = await httpUtility.httpGetJson(httpUtility.baseUrl + _links.policies.href);
            IPoliciesResource policiesResource = new PoliciesResource(httpUtility);
            JsonConvert.PopulateObject(policiesString, policiesResource);
            return policiesResource;
        }
    }
}
