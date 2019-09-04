using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class MessageResource : IResource
    {
        public string ResourceString { get; set; }
        public string rel { get; set; }
        public string href { get; set; }

        public string direction;
        public string htmlMessage;
        public string operationId;
        public string status;
        public string plainMessage;
        public string timeStamp;
        public MessageLinks _links;
             
        public MessageResource(HttpHelper httpHelper, EventChannelListener eventChannelListener)
        {   
            _links = new MessageLinks();
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

                direction = resourceObject.direction;
                htmlMessage = resourceObject.htmlMessage;
                operationId = resourceObject.operationId;
                status = resourceObject.status;
                plainMessage = resourceObject.plainMessage;
                timeStamp = resourceObject.timeStamp;
                
                if (resourceObject._links != null)
                {
                    if (resourceObject._links.contact != null)
                        _links.contact.href = resourceObject._links.contact.href;
                    if (resourceObject._links.failedDeliveryParticipant != null)
                        _links.failedDeliveryParticipant.href = resourceObject._links.failedDeliveryParticipant.href;
                    if (resourceObject._links.messaging != null)
                        _links.messaging.href = resourceObject._links.messaging.href;
                    if (resourceObject._links.participant != null)
                        _links.participant.href = resourceObject._links.participant.href;
                    if (resourceObject._links.self != null)
                        _links.self.href = resourceObject._links.self.href;
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

    internal struct MessageLinks
    {
        public Link self;
        public Link contact;
        public Link failedDeliveryParticipant;
        public Link messaging;
        public Link participant;
    }
}
