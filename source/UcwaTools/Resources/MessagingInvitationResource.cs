using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class MessagingInvitationResource
    {
        public string ResourceString { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
        public string customContent;
        public string direction;
        public string importance;
        public string message;
        public string operationId;
        public string state;
        public string subject;
        public string threadId;
        public string to;
        public MessagingInvitationLinks _links;

        public MessagingInvitationResource()
        {   
            _links = new MessagingInvitationLinks();
        }

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

        //public override string ToString()
        //{
        //    if (ResourceString != null)
        //        return ResourceString;
        //    else
        //        return "";
        //}
    }

    internal struct MessagingInvitationLinks
    {
        public Link self;
        public Link accept;
        public Link acceptedByContact;
        public Link cancel;
        public Link conversation;
        public Link decline;
        public Link derivedMessaging;
        public Link from;
        public Link messaging;
        public Link onBehalfOf;
        public Link to;
        public Link acceptedByParticipant;        
    }
}
