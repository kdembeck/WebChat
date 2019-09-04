using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class MessagingManager
    {
        private MessagingResource messagingResource;
        private HttpHelper httpHelper;

        internal MessagingManager(MessagingResource MessagingResource, HttpHelper HttpHelper)
        {
            messagingResource = MessagingResource;
            httpHelper = HttpHelper;
        }

        internal async Task Update(string messagingResourceUri)
        {
            await messagingResource.GetResource(messagingResourceUri, httpHelper);
        }

        public async Task sendMessage(string messageText)
        {   
            await httpHelper.HttpPostActionPlainText(httpHelper.ApplicationRootUri + messagingResource._links.sendMessage.href, messageText);
        }

        public async Task stopMessaging()
        { }

        public async Task addMessaging(string message, string operationId = "")
        {
            string addMessagingJson = JsonConvert.SerializeObject(new
            {
                message = "",
                operationId = operationId
            });

            await httpHelper.HttpPostAction(httpHelper.ApplicationRootUri + messagingResource._links.addMessaging.href, addMessagingJson);
        }

    }
}
