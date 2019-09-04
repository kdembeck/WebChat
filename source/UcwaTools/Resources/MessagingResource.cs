using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class MessagingResource : IResource
    {
        public string ResourceString { get; set; }
        public string rel { get; set; }
        public string href { get; set; }

        public List<string> negotiatedMessageFormats;
        public string state;
        public MessagingLinks _links;      

        public MessagingResource()
        {          
            negotiatedMessageFormats = new List<string>();
            _links = new MessagingLinks();            
        }

        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";
            try
            {
                ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + resourceUri);
                FillResourceValues(ResourceString);
            }
            catch (Exception ex) { }
            return ResourceString;
        }

        public void FillResourceValues(string resourceString)
        {
            ResourceString = resourceString;

            try
            {
                dynamic resourceObject = JObject.Parse(ResourceString);

                rel = resourceObject.rel;
                href = resourceObject.href;

                if (resourceObject.negotiatedMessageFormats != null)
                {
                    foreach (string negotiatedMessageFormat in resourceObject.negotiatedMessageFormats)
                    {
                        negotiatedMessageFormats.Add(negotiatedMessageFormat);
                    }
                }
                state = resourceObject.state;

                if (resourceObject._links != null)
                {
                    if (resourceObject._links.addMessaging != null)
                        _links.addMessaging.href = resourceObject._links.addMessaging.href;
                    if (resourceObject._links.conversation != null)
                        _links.conversation.href = resourceObject._links.conversation.href;
                    if (resourceObject._links.self != null)
                        _links.self.href = resourceObject._links.self.href;
                    if (resourceObject._links.sendMessage != null)
                        _links.sendMessage.href = resourceObject._links.sendMessage.href;
                    if (resourceObject._links.setIsTyping != null)
                        _links.setIsTyping.href = resourceObject._links.setIsTyping.href;
                    if (resourceObject._links.stopMessaging != null)
                        _links.stopMessaging.href = resourceObject._links.stopMessaging.href;
                    if (resourceObject._links.typingParticipants != null)
                        _links.typingParticipants.href = resourceObject._links.typingParticipants.href;
                }

              
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

    internal struct MessagingLinks
    {
        public Link self;
        public Link addMessaging;
        public Link conversation;
        public Link sendMessage;
        public Link setIsTyping;
        public Link stopMessaging;
        public Link typingParticipants;
    }
}
