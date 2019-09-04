using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class OnlineMeetingExtensionResource
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string ResourceString { get; set; }

        string id;
        string type;
        OnlineMeetingExtensionLinks _links;

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

    internal struct OnlineMeetingExtensionLinks
    {
        Link self;
    }
}
