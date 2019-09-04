using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KDembeck.ChatEngine;
using KDembeck.ChatEngine.Data;
using KDembeck.ChatEngine.Dashboard;

namespace KDembeck.ChatEngineTest_WinForm
{
    public partial class formMain : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<formMessaging> messagingForms;
        private IChatEngine chatEngine;
        private IServiceDashboard serviceDashboard;
        private IStatusDashboard statusDashboard;
        private Dictionary<string, string> tenantIdsAndNames;
        private Dictionary<string, string> queueIdsAndNames;

        private string selectedQueueId;

        public formMain()
        {
            InitializeComponent();
            messagingForms = new List<formMessaging>();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.startToolStripMenuItem.Enabled = false;
            this.lblEngineStatus.Text = "Enging Starting...";

            if (chatEngine == null)
                chatEngine = new ChatEngine.ChatEngine();

            if (chatEngine.status == ChatEngineStatus.Stopped)
            {
                chatEngine.ChatEngineStarted += Handle_OnChatEngineStarted;
                chatEngine.startEngine();
            }
        }

        private async void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.lblEngineStatus.Text = "Engine Stopping...";
            this.timeQueueStatus.Stop();

            while (messagingForms.Count > 0)
            {
                formMessaging messagingForm = messagingForms.FirstOrDefault();
                if (messagingForm != null)
                {
                    messagingForm.Close();
                    messagingForms.Remove(messagingForm);
                }                
            }

            this.comboQueueName.Items.Clear();
            this.comboTenantDomain.Items.Clear();
            this.txtChatUserEmail.Text = string.Empty;
            this.txtChatUserName.Text = string.Empty;
            this.comboTenantDomain.Enabled = false;
            this.comboQueueName.Enabled = false;
            this.txtChatUserEmail.Enabled = false;
            this.txtChatUserName.Enabled = false;
            this.startToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Enabled = false;
            this.btnQueue.Enabled = false;

            if (chatEngine.status == ChatEngineStatus.Started)
            {
                await chatEngine.stopEngine();
                serviceDashboard = null;
                statusDashboard = null;
            }

            this.startToolStripMenuItem.Enabled = true;

            this.lblEngineStatus.Text = "Engine Stopped";
        }

        private void btnQueue_Click(object sender, EventArgs e)
        {
            string conversationId = string.Empty;
            if (serviceDashboard != null)
            {
                comboBoxItem item;
                item = (comboBoxItem)comboQueueName.SelectedItem;
                string selectedQueueId = item.Value;

                conversationId = serviceDashboard.queueNewMessagingSession(txtChatUserName.Text, txtChatUserEmail.Text, null, selectedQueueId);
                if (!string.IsNullOrEmpty(conversationId))
                {
                    formMessaging messagingForm = new formMessaging(conversationId);
                    messagingForm.sendChatMessage += Handle_WebChatUserSendMessage;
                    messagingForm.userEndedChatSession += Handle_WebChatUserLeftConversation;
                    messagingForms.Add(messagingForm);
                    messagingForm.Show();
                }
            }
        }

        private void comboTenantDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboQueueName.Items.Clear();
            selectedQueueId = string.Empty;
            comboBoxItem item = (comboBoxItem)comboTenantDomain.SelectedItem;
            string tenantId = item.Value;
            queueIdsAndNames = statusDashboard.getQueueIdsAndNamesForTenant(tenantId);

            foreach (KeyValuePair<string, string> queueIdName in queueIdsAndNames)
            {
                comboQueueName.Items.Add(new comboBoxItem(queueIdName.Value, queueIdName.Key));
            }
            comboQueueName.Update();
        }

        private void comboQueueName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxItem item;
            item = (comboBoxItem)comboQueueName.SelectedItem;
            selectedQueueId = item.Value;
        }

        private async void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chatEngine != null)
            {
                await chatEngine.stopEngine();
            }
        }

        private void Handle_OnChatEngineStarted(object sender, EventArgs e)
        {
            serviceDashboard = chatEngine.ServiceDashboard;            
            statusDashboard = chatEngine.StatusDashboard;
            serviceDashboard.ChatSessionChatMessageReceivedPlainTextEvent += Handle_OnChatSessionMessageReceived;
            serviceDashboard.ChatSessionStatusMessageReceivedEvent += Handle_OnChatSessionStatusMessageReceived;

            tenantIdsAndNames = statusDashboard.getTenantsIdsAndDomainNames();
            comboTenantDomain.Items.Clear();
            foreach (KeyValuePair<string, string> tenantIdName in tenantIdsAndNames)
            {
                comboTenantDomain.Items.Add(new comboBoxItem(tenantIdName.Value, tenantIdName.Key));
            }

            this.comboTenantDomain.Enabled = true;
            this.comboQueueName.Enabled = true;
            this.txtChatUserEmail.Enabled = true;
            this.txtChatUserName.Enabled = true;
            
            this.stopToolStripMenuItem.Enabled = true;
            this.btnQueue.Enabled = true;

            this.lblEngineStatus.Text = "Engine Started";
            this.timeQueueStatus.Interval = 1000;
            this.timeQueueStatus.Start();
        }

        private void Handle_OnChatSessionMessageReceived(object sender, ConversationMessageReceivedEventArgs e)
        {
            formMessaging messagingForm = messagingForms.Where(x => x.conversationId == e.conversationId).FirstOrDefault();
            if (messagingForm != null)
            {
                messagingForm.chatSessionMessageReceived(e.fromDisplayName, e.messageText);
            }
        }

        private void Handle_OnChatSessionStatusMessageReceived(object sender, ConversationStatusMessageReceivedEventArgs e)
        {
            formMessaging messagingForm = messagingForms.Where(x => x.conversationId == e.conversationId).FirstOrDefault();
            if (messagingForm != null)
            {
                messagingForm.chatSessionStatusMessageReceived(e.statusMessage);
            }
        }

        private void Handle_WebChatUserSendMessage(object sender, SendChatMessageEventArgs e)
        {   
            serviceDashboard.sendMessageToAgent(e.messageText, e.conversationId);
        }

        private void Handle_WebChatUserLeftConversation(object sender, WebUserLeftConversationEventArgs e)
        {
            messagingForms.RemoveAll(x => x.conversationId == e.conversationId);            
            serviceDashboard.webUserLeftConversation(e.conversationId);
        }        

        private class comboBoxItem
        {
            public string Name;
            public string Value;

            public comboBoxItem(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private void timeQueueStatus_Tick(object sender, EventArgs e)
        {
            timeQueueStatus.Stop();
            listAgentStatus.Items.Clear();
            listSessionStatus.Items.Clear();
            if (!string.IsNullOrEmpty(selectedQueueId))
            {
                updateQueueStatus();
            }
            else
            {
                lblAgentsInSession.Text = "0";
                lblAvailableAgents.Text = "0";
                lblOfflineAgents.Text = "0";
                lblUnavailableAgents.Text = "0";
                lblNumberOfActiveChatSessions.Text = "0";
                lblNumberOfWaitingChatSessions.Text = "0";
            }
            timeQueueStatus.Start();
        }

        private void updateQueueStatus()
        {
            QueueStatus queueStatus = statusDashboard.getQueueStatus(selectedQueueId);
            foreach (AgentStatus agentStatus in queueStatus.agentStatuses)
            {
                ListViewItem listViewItem = new ListViewItem(agentStatus.agentDisplayName);                
                listViewItem.SubItems.Add(agentStatus.agentState.ToString());
                listAgentStatus.Items.Add(listViewItem);
            }
            listAgentStatus.Update();

            lblNumberOfActiveChatSessions.Text = queueStatus.numberOfActiveSessions.ToString();
            if (queueStatus.numberOfActiveSessions > 0)
            {
                foreach (ChatSessionStatus chatSessionStatus in queueStatus.activeChatSessionStatuses)
                {
                    ListViewItem listViewItem = new ListViewItem(chatSessionStatus.webUserName);
                    listViewItem.SubItems.Add("Active");
                    TimeSpan duration = DateTime.Now - chatSessionStatus.messagingStartTime;
                    listViewItem.SubItems.Add(duration.ToString(@"hh\:mm\:ss"));
                    listViewItem.SubItems.Add(chatSessionStatus.invitedAgentDisplayName);
                    listSessionStatus.Items.Add(listViewItem);
                }
            }

            lblNumberOfWaitingChatSessions.Text = queueStatus.numberOfWaitingSessions.ToString();
            if (queueStatus.numberOfWaitingSessions > 0)
            {
                foreach (ChatSessionStatus chatSessionStatus in queueStatus.waitingChatSessionStatuses)
                {
                    ListViewItem listViewItem = new ListViewItem(chatSessionStatus.webUserName);
                    listViewItem.SubItems.Add("Waiting");
                    TimeSpan duration = DateTime.Now - chatSessionStatus.queuedTime;
                    listViewItem.SubItems.Add(duration.ToString(@"hh\:mm\:ss"));
                    listSessionStatus.Items.Add(listViewItem);
                }
            }
            listSessionStatus.Update();

            lblAgentsInSession.Text = queueStatus.numberOfAgentsInSession.ToString();
            lblAvailableAgents.Text = queueStatus.numberOfAgentsAvailable.ToString();
            lblOfflineAgents.Text = queueStatus.numberOfAgentsOffline.ToString();
            lblUnavailableAgents.Text = queueStatus.numberOfAgentsUnavailable.ToString();
        }
    }
}
