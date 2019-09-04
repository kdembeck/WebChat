using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KDembeck.ChatEngineTest_WinForm
{
    public partial class formMessaging : Form
    {
        public string conversationId { get; private set; }     
        public event EventHandler<SendChatMessageEventArgs> sendChatMessage;
        public event EventHandler<WebUserLeftConversationEventArgs> userEndedChatSession;

        public formMessaging()
        {
            InitializeComponent();
        }

        public formMessaging(string conversationId)
        {
            InitializeComponent();
            this.conversationId = conversationId;
        }

        public void chatSessionMessageReceived(string senderDisplayName, string messageText)
        {
            Action<string, string> UpdateChatWindowDelegate = updateChatWindowMessageReceived;
            Invoke(UpdateChatWindowDelegate, senderDisplayName, messageText);
        }

        public void chatSessionStatusMessageReceived(string messageText)
        {
            Action<string> UpdateChatWindowDelegate = updateChatWindowStatusMessageReceived;
            Invoke(UpdateChatWindowDelegate, messageText);
        }

        private void updateChatWindowMessageReceived(string senderDisplayName, string messageText)
        {     
            txtMessagingWindow.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
            txtMessagingWindow.AppendText(senderDisplayName + " says: ");
            txtMessagingWindow.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
            txtMessagingWindow.AppendText(messageText + "\n\n");
        }

        private void updateChatWindowStatusMessageReceived(string messageText)
        {
            txtMessagingWindow.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);            
            txtMessagingWindow.AppendText(messageText + "\n\n");
        }

        private void updateChatWindowStatusMessage(string messageText)
        {   
            txtMessagingWindow.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
            txtMessagingWindow.AppendText(messageText + "\n\n");
            txtMessagingWindow.Update();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string messageText = txtSendMessageText.Text;
            txtSendMessageText.Text = string.Empty;
            sendChatMessage?.Invoke(this, new SendChatMessageEventArgs(messageText, conversationId));
        }

        private void formMessaging_FormClosing(object sender, FormClosingEventArgs e)
        {
            userEndedChatSession?.Invoke(this, new WebUserLeftConversationEventArgs(conversationId));
        }
    }

    public class SendChatMessageEventArgs : EventArgs
    {
        public string messageText;
        public string conversationId;

        public SendChatMessageEventArgs(string messageText, string conversationId)
        {
            this.messageText = messageText;
            this.conversationId = conversationId;            
        }
    }

    public class WebUserLeftConversationEventArgs : EventArgs
    {
        public string conversationId;

        public WebUserLeftConversationEventArgs(string conversationId)
        {
            this.conversationId = conversationId;            
        }
    }
}
