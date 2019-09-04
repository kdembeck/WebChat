using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IApplicationResource : IResourceBase
    {
        string culture { get; set; }
        string userAgent { get; set; }
        string type { get; set; }
        string endpointId { get; set; }
        string instanceId { get; set; }
        string id { get; set; }
        ApplicationLinks _links { get; set; }
        ApplicationEmbedded _embedded { get; set; }
        ICommunicationResource communication { get; }
        IOnlineMeetingsResource onlineMeetings { get; }
        IMeResource me { get; }
        IPeopleResource people { get; }
        Task initializeResources();
        Task<IApplicationResource> Get(string ResourceUrl);
        Task<IApplicationResource> Get();
        Task signOut();
        Task reportMyNetwork(string chassisID, string clientNetworkType, string ip, string mac, string portID, string rssi, string subnetID, string wapBSSID);
        void startEventChannel(IEventHandler eventHandler);
        Task<IPoliciesResource> getPolicies();
    }

    public class ApplicationLinks
    {
        public Link self;
        public Link batch;
        public Link events;
        public Link policies;
        public Link reportMyNetwork;
    }

    public class ApplicationEmbedded
    {
        public ICommunicationResource communication;
        public IMeResource me;
        public IOnlineMeetingsResource onlineMeetings;
        public IPeopleResource people;
        public IPoliciesResource policies;

        public ApplicationEmbedded()
        {
            communication = new CommunicationResource();
            me = new MeResource();
            onlineMeetings = new OnlineMeetingsResource();
            people = new PeopleResource();
            policies = new PoliciesResource();
        }
    }
}
