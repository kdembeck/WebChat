using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.EventChannel;
using KDembeck.UcwaWebApiClient.Resources;

namespace KDembeck.ChatEngine
{
    public class InstantMessagingUtility : IInstantMessagingUtility
    {
        private ICommunicationResource communication;
        private IEventHandler events;
        private List<string> startMessagingOperationIds;

        public InstantMessagingUtility(ICommunicationResource communication, IEventHandler events)
        {
            this.communication = communication;
            this.events = events;
            startMessagingOperationIds = new List<string>();
            events.OnMessagingInvitationCompleted += Handle_OnMessagingInvitationCompleted;
        }

        public async Task sendInstantMessageToAgent(string agentSipUri, string subject, string messageText)
        {
            string startMessagingOperationId = Guid.NewGuid().ToString();
            startMessagingOperationIds.Add(startMessagingOperationId);
            await communication.startMessaging(startMessagingOperationId, agentSipUri, subject, Importance.Normal, messageText);
        }

        public void drain()
        {
            events.OnMessagingInvitationCompleted -= Handle_OnMessagingInvitationCompleted;
        }

        private async void Handle_OnMessagingInvitationCompleted(object sender, UcwaMessagingInvitationEventArgs e)
        {   
            if (startMessagingOperationIds.RemoveAll(x => x == e.messagingInvitation.operationId) > 0)
            {
                IConversationResource conversation = await e.messagingInvitation.getConversationResource();
                await conversation.Delete();
            }
        }
    }
}
