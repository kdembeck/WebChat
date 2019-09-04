using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{  
    public class ApplicationResource : IResource
    {
        public string ResourceString { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
        public string Type;
        public string Culture;
        public string EndpointId;
        public string Id;
        public string InstanceId;
        public string UserAgent;
        public ApplicationLinks _links;
        public ApplicationEmbedded _embedded;

        public ApplicationResource()
        {   
            _links = new ApplicationLinks();
            _embedded = new ApplicationEmbedded();            
        }
        
        public async Task<string> GetResource(string applicationsResourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";
            ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + applicationsResourceUri);
            FillResourceValues(ResourceString);
            return ResourceString;
        }

        public void FillResourceValues(string resourceString)
        {  
           JsonConvert.PopulateObject(resourceString, this);           
        }
        
        //public override string ToString()
        //{
        //    if (ResourceString != null)
        //        return ResourceString;
        //    else
        //        return "";
        //}
    }

    public struct ApplicationLinks
    {
        public Link self;
        public Link batch;
        public Link events;
        public Link policies;
        public Link reportMyNetwork;
        public Link communication;
        public Link me;
        public Link onlineMeetings;
        public Link people;
    }

    public class ApplicationEmbedded
    {
        public MeResource me;
        public CommunicationResource communication;
        public OnlineMeetingsResource onlineMeetings;
        public PeopleResource people;
    }
}
