//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using UcwaTools.Utilities;

//namespace UcwaTools
//{
//    internal class EventChannelEventResource : IResource
//    {
//        public string ResourceString { get; set; }

//        public string type;
//        public EventChannelEventLink link;
//        public string _embedded;    

//        public EventChannelEventResource()
//        {
//            link = new EventChannelEventLink();
//        }

//        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
//        {
//            ResourceString = "";
//            try
//            {
//                ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + resourceUri);
//                FillResourceValues(ResourceString);
//            }
//            catch (Exception ex) { }
//            return ResourceString;
//        }

//        public void FillResourceValues(string resourceString)
//        {
//            ResourceString = resourceString;
//            dynamic resourceObject = JObject.Parse(ResourceString);  
                      
//            try
//            {
//                type = resourceObject.type;

//                if (resourceObject.link != null)
//                {
//                    link.rel = resourceObject.link.rel;
//                    link.href = resourceObject.link.href;
//                }

//                if (resourceObject._embedded != null)
//                {
//                    _embedded = JsonConvert.SerializeObject(resourceObject._embedded);
//                }
//            }
//            catch (Exception ex) { }
//        }
//    }

//    internal struct EventChannelEventLink
//    {
//        public string rel;
//        public string href;
//    }
//}
