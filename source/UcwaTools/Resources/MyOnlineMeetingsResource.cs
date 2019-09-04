using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class MyOnlineMeetingsResource
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string ResourceString { get; set; }

        public MyOnlineMeetingsLinks _links;

        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";            
            ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + resourceUri);
            FillResourceValues(ResourceString);            
            return ResourceString;
        }

        public void FillResourceValues(string resourceString)
        {
            JsonConvert.PopulateObject(resourceString, this);
        }
    }

    internal struct MyOnlineMeetingsLinks
    {
        public Link self;
        public Link myAssignedOnlineMeeting;
        public Link myOnlineMeeting;
    }
}
