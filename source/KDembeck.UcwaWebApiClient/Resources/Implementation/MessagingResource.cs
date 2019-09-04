using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Dynamic;
using KDembeck.UcwaWebApiClient.Utilities;


namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MessagingResource : ResourceBase, IMessagingResource
    {
        public List<MessageFormat> negotiatedMessageFormats { get; set; }
        public string state { get; set; }
        public MessagingLinks _links { get; set; }

        private void initializeProperties()
        {
            negotiatedMessageFormats = new List<MessageFormat>();
            state = null;
            _links = new MessagingLinks();
        }

        public MessagingResource()
        {
            initializeProperties();
        }

        public MessagingResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IMessagingResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IMessagingResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task addMessaging(string operationId = null)
        {
            if (httpUtility != null && _links.addMessaging != null)
            {
                dynamic addMessagingSettings = new ExpandoObject();
               
                if (operationId != null)
                    addMessagingSettings.operationId = operationId;

                string addMessagingJson = JsonConvert.SerializeObject(addMessagingSettings);
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.addMessaging.href, addMessagingJson);                
            }
        }
        
        public async Task<IConversationResource> getConversation()
        {
            if (httpUtility != null && _links.conversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else return null;
        }

        public async Task sendMessageHtml(string htmlMessage)
        {
            if (httpUtility != null && _links.sendMessage != null)
            {
                await httpUtility.httpPostTextHtml(httpUtility.baseUrl + _links.sendMessage.href, htmlMessage);
            }
        }

        public async Task sendMessageHtml(string htmlMessage, string operationId)
        {
            if (httpUtility != null && _links.sendMessage != null)
            {
                await httpUtility.httpPostTextHtml(httpUtility.baseUrl + _links.sendMessage.href + "?operationId=" + operationId, htmlMessage);
            }
        }

        public async Task sendMessagePlainText(string message)
        {
            if (httpUtility != null && _links.sendMessage != null)
            {
                await httpUtility.httpPostPlainText(httpUtility.baseUrl + _links.sendMessage.href, message);
            }
        }

        public async Task sendMessagePlainText(string message, string operationId)
        {
            if (httpUtility != null && _links.sendMessage != null)
            {
                await httpUtility.httpPostPlainText(httpUtility.baseUrl + _links.sendMessage.href + "?operationId=" + operationId, message);
            }
        }

        public async Task setIsTyping()
        {
            if (httpUtility != null && _links.setIsTyping != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.setIsTyping.href);
            }
        }

        public async Task stopMessaging()
        {
            if (httpUtility != null && _links.stopMessaging != null)
            {
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.stopMessaging.href);
            }
        }

        public async Task getTypingParticipants()
        {
            throw new NotImplementedException();
            //some kind of Participants resource? 
        }
    }
}
