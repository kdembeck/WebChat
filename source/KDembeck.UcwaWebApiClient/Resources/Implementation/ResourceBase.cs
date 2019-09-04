using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public abstract class ResourceBase : IResourceBase
    {   
        public IHttpUtility httpUtility { get; set; }
        public string resourceString { get; set; }
        //public string rel { get; set; }
        //public string href { get; set; }
        public async Task Get(string ResourceUrl)
        {
            if (httpUtility != null)
            {
                string resourceJsonString = await httpUtility.httpGetJson(ResourceUrl);
                resourceString = resourceJsonString;
                if (resourceJsonString != "")
                    JsonConvert.PopulateObject(resourceJsonString, this);
            }
        }
    }
}
