using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class ConversationManager
    {
        private ConversationResource conversationResource;
        private HttpHelper httpHelper;
        private MessagingManager messaging;
        public string threadId;
        public string conversationId;

        public MessagingManager Messaging { get { return messaging; } }

        internal ConversationManager(ConversationResource ConversationResource, HttpHelper HttpHelper)
        {
            conversationResource = ConversationResource;
            httpHelper = HttpHelper;
            threadId = conversationResource.threadId;
            //get the messaging resource and initialize the messaging manager
        }

        internal async Task Initialize()
        {
            MessagingResource messagingResource = new MessagingResource();
            await messagingResource.GetResource(conversationResource._links.messaging.href, httpHelper);
            messaging = new MessagingManager(messagingResource, httpHelper);
        }

        internal async Task Update(string conversationResourceUri)
        {
            await conversationResource.GetResource(conversationResourceUri, httpHelper);
            await messaging.Update(conversationResource._links.messaging.href); 
        }

        public async Task addParticipant(string to, string operationId = "")
        {
            //to = sip uri of contact
            string addParticipantJson = JsonConvert.SerializeObject(new
            {
                to = to,
                operationId = operationId
            });

            await httpHelper.HttpPostAction(httpHelper.ApplicationRootUri + conversationResource._links.addParticipant.href, addParticipantJson);
        }

        public async Task GetParticipants()
        {
            //returns a collection of participants
        }



    }
}
