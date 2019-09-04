using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.EventChannel;
using Newtonsoft.Json.Linq;

namespace Test_WinForm
{
    public partial class formOnlineMeeting : Form
    {
        private IUcwaClient ucwaClient;

        private string startOnlineMeetingInvitationOperationId;
        private string addParticipantOnlineMeetingInvitationOperationId;
        private string addMessagingOperationId;
        private List<SentMessage> sentMessages;

        private IConversationResource conversation;
        private IMessagingResource messaging;
        private List<IParticipantResource> participants;
        private IParticipantResource localParticipant;

        public formOnlineMeeting(IUcwaClient ucwaClient)
        {
            this.ucwaClient = ucwaClient;
            ucwaClient.events.OnOnlineMeetingInvitationCompleted += Handle_OnOnlineMeetingInvitationCompleted;
            ucwaClient.events.OnParticipantInvitationCompleted += Handle_OnParticipantInvitationCompleted;
            ucwaClient.events.OnConversationUpdated += Handle_OnConversationUpdated;
            ucwaClient.events.OnMessagingUpdated += Handle_OnMessagingUpdated;
            ucwaClient.events.OnParticipantDeleted += Handle_OnParticipantDeleted;
            ucwaClient.events.OnParticipantAdded += Handle_OnParticipantAdded;
            ucwaClient.events.OnConversationDeleted += Handle_OnConversationDeleted;
            ucwaClient.events.OnMessageCompleted += Handle_OnMessageCompleted;
            UpdateMessageWindow = addIncomingMessageTextToMessageWindow;
            sentMessages = new List<SentMessage>();
            InitializeComponent();
        }

        #region Form event handlers

        private void formOnlineMeeting_Load(object sender, EventArgs e)
        {

        }

        private async void btnStartOnlineMeeting_Click(object sender, EventArgs e)
        {
            startOnlineMeetingInvitationOperationId = Guid.NewGuid().ToString();
            await ucwaClient.application.communication.startOnlineMeeting(txtSubject.Text, Importance.Normal, startOnlineMeetingInvitationOperationId);
        }

        private async void btnInviteParticipant_Click(object sender, EventArgs e)
        {
            addParticipantOnlineMeetingInvitationOperationId = Guid.NewGuid().ToString();
            string participantUri = txtInviteParticipant.Text;
            await conversation.addParticipant(participantUri, addParticipantOnlineMeetingInvitationOperationId);
        }

        private async void btnRemoveParticipant_Click(object sender, EventArgs e)
        {
            string participantToRemoveUri = txtRemoveParticipant.Text;
            participants = await conversation.getParticipants();
            IParticipantResource participantToRemove = participants.Where(x => x.uri == participantToRemoveUri).FirstOrDefault();
            if (participantToRemove != null)
            {
                await participantToRemove.eject();
            }
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            string messageText = txtSendMessage.Text;
            txtSendMessage.Text = "";
            if (messaging != null)
            {
                string sendMessageOperationId = Guid.NewGuid().ToString();
                await messaging.sendMessagePlainText(messageText, sendMessageOperationId);
                sentMessages.Add(new SentMessage(messageText, sendMessageOperationId));
                //string myDisplayName = localParticipant.name;
                //txtMessageWindow.Lines = Add<string>(txtMessageWindow.Lines, myDisplayName + ": " + messageText);
            }
        }

        private async void btnEndMeeting_Click(object sender, EventArgs e)
        {
            await conversation.Delete();
        }

        #endregion

        private Action<string> UpdateMessageWindow;

        private void addIncomingMessageTextToMessageWindow(string messageText)
        {
            if (txtMessageWindow.Lines.Count() > 0)
                messageText = "\r\n" + messageText;
            txtMessageWindow.Lines = Add<string>(txtMessageWindow.Lines, messageText);
        }

        private string decodeMessageText(string messageText)
        {
            //return messageText;
            messageText = messageText.Replace("data:text/plain;charset=utf-8,", string.Empty);
            if (messageText.Substring(messageText.Length - 6) == "%0d%0a")
                messageText = messageText.Remove(messageText.Length - 6);
            return HttpUtility.UrlDecode(messageText, Encoding.UTF8);

        }

        private T[] Add<T>(T[] array, T item)
        {
            T[] returnarray = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                returnarray[i] = array[i];
            }
            returnarray[array.Length] = item;
            return returnarray;
        }

        #region Ucwa client event handlers

        private async void Handle_OnOnlineMeetingInvitationCompleted(object sender, UcwaOnlineMeetingInvitationEventArgs e) 
        {
            if (e.onlineMeetingInvitation.operationId == startOnlineMeetingInvitationOperationId)
            {   
                conversation = await e.onlineMeetingInvitation.getConversation();
                messaging = await conversation.getMessaging();
                localParticipant = await conversation.getLocalParticipant();
            }
        }

        private async void Handle_MessagingInvitationCompleted(object sender, UcwaMessagingInvitationEventArgs e)
        {
            //just log something here to indicate that we're ready to start messaging
            //this is where we would start hunting for agents to invite
        }

        private async void Handle_OnParticipantInvitationCompleted(object sender, UcwaParticipantInvitationEventArgs e)
        {
            //check for our operationID then check for state failed
            if (e.participantInvitation.operationId == addParticipantOnlineMeetingInvitationOperationId)
            {
                if (e.participantInvitation.state == InvitationState.Connected)
                {                   
                    addMessagingOperationId = Guid.NewGuid().ToString();
                    await messaging.addMessaging(addMessagingOperationId);
                }
                else
                {
                    //invitation was declined
                }
            }
        }

        private void Handle_OnConversationUpdated(object sender, UcwaConversationEventArgs e)
        {
            if (conversation != null)
            {
                if (e.conversation.threadId == conversation.threadId)
                {
                    conversation = e.conversation;
                }
            }
        }

        private void Handle_OnConversationDeleted(object sender, UcwaConversationEventArgs e)
        {
            if (e.href == conversation._links.self.href)
            {   
                conversation = null;
                messaging = null;
                localParticipant = null;
                participants = null;    
            }
        }

        private void Handle_OnMessagingUpdated(object sender, UcwaMessagingEventArgs e)
        {
            if (messaging != null)
            {
                if (e.messaging._links.self.href == messaging._links.self.href)
                {   
                    messaging = e.messaging;
                }
            }
        }

        private async void Handle_OnParticipantDeleted(object sender, UcwaParticipantEventArgs e)
        {   
            //if (e.participant._links.conversation.href == conversation._links.self.href)
            //{
            //    //gotta check if it's for this conversation
            //    participants = await conversation.getParticipants();
            //}
        }

        private async void Handle_OnParticipantAdded(object sender, UcwaParticipantEventArgs e)
        {
            if (e.participant._links.conversation.href == conversation._links.self.href)
            {
                participants = await conversation.getParticipants();
            }
            else
            {

            }
        }
        
        private void Handle_OnMessageCompleted(object sender, UcwaMessageEventArgs e)
        {
            if (e.message._links.messaging.href == messaging._links.self.href)
            {
                if (e.message.direction == "Incoming")
                {   
                    string decodedMessageText = decodeMessageText(e.message.plainMessage);

                    string senderDisplayName = e.message._links.participant.title;                    
                    Invoke(UpdateMessageWindow, senderDisplayName + ": " + decodedMessageText);
                }
                else
                {
                    //we can only check for operationId here
                    string sentMessageOperationId = e.message.operationId;
                    string sentMessageText = sentMessages.Where(x => x.operationId == sentMessageOperationId).Select(x => x.messageText).FirstOrDefault();
                    sentMessages.RemoveAll(x => x.operationId == sentMessageOperationId);

                    //update our message window
                    string senderDisplayName = localParticipant.name;
                    Invoke(UpdateMessageWindow, senderDisplayName + ": " + sentMessageText);
                }
            }            
        }

        #endregion

        private void formOnlineMeeting_FormClosing(object sender, FormClosingEventArgs e)
        {
            ucwaClient.events.OnOnlineMeetingInvitationCompleted -= Handle_OnOnlineMeetingInvitationCompleted;
            ucwaClient.events.OnParticipantInvitationCompleted -= Handle_OnParticipantInvitationCompleted;
            ucwaClient.events.OnConversationUpdated -= Handle_OnConversationUpdated;
            ucwaClient.events.OnMessagingUpdated -= Handle_OnMessagingUpdated;
            ucwaClient.events.OnParticipantDeleted -= Handle_OnParticipantDeleted;
            ucwaClient.events.OnParticipantAdded -= Handle_OnParticipantAdded;
            ucwaClient.events.OnConversationDeleted -= Handle_OnConversationDeleted;
            ucwaClient.events.OnMessageCompleted -= Handle_OnMessageCompleted;
        }
    }

    internal class SentMessage
    {
        public string operationId;
        public string messageText;

        public SentMessage(string messageText, string operationId)
        {
            this.messageText = messageText;
            this.operationId = operationId;
        }
    }
}
