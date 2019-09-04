using System;
using System.Windows.Forms;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace Test_WinForm
{
    public partial class formMessaging : Form
    {   
        public string screenName;
        public string recipientSipUri;
        public string messageSubject;
        public string messageText;
        public string importance;        
        public string operationId;
        public string screenText="";
        public string threadId;
        public string conversationId;
        private IUcwaClient ucwaClient;
        private IConversationResource ucwaConversation;
        private IMessagingInvitationResource messagingInvitation;
        
        private string invitationOperationId;
        
        public formMessaging(IUcwaClient ucwaClient)
        {
            InitializeComponent();
            this.ucwaClient = ucwaClient;
            
        }

        private void btnSendMessage1_Click(object sender, EventArgs e)
        {
            txtMessageSubject.Enabled = false;
            txtMessagingReceiptionSipUri.Enabled = false;            

            recipientSipUri = txtMessagingReceiptionSipUri.Text;
            messageSubject = txtMessageSubject.Text;
            messageText = txtMessageTextToSend1.Text;
            importance = "Normal";
            txtMessageTextToSend1.Text = "";
            this.Text = messageSubject;
            screenText += messageText + "\n";

            if (conversationId == null)
            {
                operationId = Guid.NewGuid().ToString();
                //ucwaApplication.Communication.startMessaging(recipientSipUri, messageSubject, importance, messageText, operationId);

                //ucwaApplication.Communication.OnMessagingInvitationCompletedEvent += HandleOnConversationCreatedEvent;
                //ucwaApplication.Communication.OnIncomingMessageReceivedEvent += HandleOnWebClientMessageReceivedFromSkypeUserEvent;
                //ucwaApplication.Communication.OnConversationDeletedEvent += HandleOnConversationDeletedEvent;
            }
            else {

                //ucwaApplication.CommunicationManager.SendMessageToSkypeUser(conversationId, messageText);
                //ConversationManager conversation = ucwaApplication.Communication.conversations.Where(x => x.conversationId == conversationId).FirstOrDefault();
                //conversation.Messaging.sendMessage(messageText);
            }
        }

        private void HandleOnWebClientMessageReceivedFromSkypeUserEvent(string ConversationId, string Message)
        {
            try
            {
                if (conversationId == ConversationId)
                {
                    //this message is for us                    
                }
            }
            catch (Exception ex) { }
        }

        private void HandleOnConversationDeletedEvent(string ConversationId)
        {
            if (threadId == ConversationId)
            {
                if (ucwaConversation != null)
                    ucwaConversation = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();

                //if (lblMessageScreen.Text != screenText)
                //    lblMessageScreen.Text = screenText;

                timer1.Start();
            }
            catch (Exception ex) { }
        }

        public void StartTimer()
        {
            try
            {
                timer1.Interval = 1000;
                timer1.Start();
            }
            catch (Exception ex) { }
        }

        private void formMessagingSession_Load(object sender, EventArgs e)
        {
            //ucwaApplication.CommunicationManager.EndConversation(conversationId);
        }

        private async void btnEndConversation_Click(object sender, EventArgs e)
        {
            if (ucwaConversation != null)
            {
                await ucwaConversation.Delete();
                ucwaConversation = null;
            }
        }

        private async void btnStartInvitation_Click(object sender, EventArgs e)
        {
            invitationOperationId = Guid.NewGuid().ToString();
            ucwaClient.events.OnMessagingInvitationStarted += Handle_OnMessagingInvitationStarted;
            ucwaClient.events.OnMessagingInvitationUpdated += Handle_OnMessagingInvitationUpdated;
            ucwaClient.events.OnMessagingInvitationCompleted += Handle_OnMessagingInvitationCompleted;
            await ucwaClient.application.communication.startMessaging(invitationOperationId, txtMessagingReceiptionSipUri.Text, txtMessageSubject.Text, Importance.Normal, txtMessageTextToSend1.Text);
        }

        private async void btnInviteParticipant_Click(object sender, EventArgs e)
        {
            //invite from the conversation resource
            
        }

        private void Handle_OnMessagingInvitationStarted(object sender, UcwaMessagingInvitationEventArgs e)
        {
            if (e.messagingInvitation.operationId == invitationOperationId)
            {
                ucwaClient.events.OnMessagingInvitationStarted -= Handle_OnMessagingInvitationStarted;
                messagingInvitation = e.messagingInvitation;
            }
        }

        private void Handle_OnMessagingInvitationUpdated(object sender, UcwaMessagingInvitationEventArgs e)
        {
            if (e.messagingInvitation.operationId == invitationOperationId)
            {
                messagingInvitation = e.messagingInvitation;
            }
        }

        private async void Handle_OnMessagingInvitationCompleted(object sender, UcwaMessagingInvitationEventArgs e)
        {              
            if (e.messagingInvitation.operationId == invitationOperationId)
            {   
                ucwaClient.events.OnMessagingInvitationUpdated -= Handle_OnMessagingInvitationUpdated;
                ucwaClient.events.OnMessagingInvitationCompleted -= Handle_OnMessagingInvitationCompleted;
                ucwaConversation = await e.messagingInvitation.getConversationResource();
            }                       
        }

        private void formMessaging_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ucwaConversation != null)
            {
                ucwaConversation.Delete();
                ucwaConversation = null;
            }
        }
    }
}
