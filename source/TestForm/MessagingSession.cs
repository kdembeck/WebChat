using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.Utilities;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace Test_WinForm
{
    public class MessagingSession
    {
        public string startMessagingOperationId { get; private set; }
        public string contactUri { get; set; }
        CommunicationResource communication;
        public ConversationResource conversation { get; set; }
        public MessagingResource messaging { get; set; }

        public MessagingSession(CommunicationResource CommunicationResource)
        {
            communication = CommunicationResource;
        }

        public async Task startMessaging()
        {
            startMessagingOperationId = Guid.NewGuid().ToString();
            await communication.startMessaging(startMessagingOperationId);
        }

        public async Task addParticipant(string contactUri)
        {
            //this.contactUri = contactUri;
            await conversation.addParticipant(contactUri);
            
        }

        public async Task sendMessage(string messageText)
        {
            if (messaging != null)
                await messaging.sendMessagePlainText(messageText);
        }

        public async Task stopMessaging()
        {
            if (messaging != null)
                await messaging.stopMessaging();
        }

        public void Handle_ParticipantAdded(object sender, UcwaClientEventArgs eventArgs)
        {

        }

        public void Handle_ConversationAdded(object sender, UcwaClientEventArgs eventArgs)
        {

        }
    }
}
