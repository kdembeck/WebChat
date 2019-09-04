using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace UcwaTools
{
    class Participant
    {
        public string ResourceString { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
        public async Task<string> GetResource(string resourceUri)
        {
            return ResourceString;
        }
        public void FillResourceValues(string resourceString)
        {   
            ResourceString = resourceString;

            try
            {
                dynamic resourceObject = JObject.Parse(ResourceString);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            if (ResourceString != null)
                return ResourceString;
            else
                return "";
        }
    }
}
