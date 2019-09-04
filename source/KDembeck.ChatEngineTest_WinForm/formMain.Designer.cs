namespace KDembeck.ChatEngineTest_WinForm
{
    partial class formMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "User A",
            "Available"}, -1);
            this.btnQueue = new System.Windows.Forms.Button();
            this.txtChatUserEmail = new System.Windows.Forms.TextBox();
            this.txtChatUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboTenantDomain = new System.Windows.Forms.ComboBox();
            this.comboQueueName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAvailableAgents = new System.Windows.Forms.Label();
            this.lblUnavailableAgents = new System.Windows.Forms.Label();
            this.lblOfflineAgents = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.chatEngineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantDomainsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantDomainQueuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueAgentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblAgentsInSession = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.listAgentStatus = new System.Windows.Forms.ListView();
            this.agentName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.agentState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listSessionStatus = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblNumberOfActiveChatSessions = new System.Windows.Forms.Label();
            this.lblNumberOfWaitingChatSessions = new System.Windows.Forms.Label();
            this.Label100 = new System.Windows.Forms.Label();
            this.Label200 = new System.Windows.Forms.Label();
            this.lblEngineStatus = new System.Windows.Forms.Label();
            this.timeQueueStatus = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQueue
            // 
            this.btnQueue.Enabled = false;
            this.btnQueue.Location = new System.Drawing.Point(259, 555);
            this.btnQueue.Name = "btnQueue";
            this.btnQueue.Size = new System.Drawing.Size(117, 23);
            this.btnQueue.TabIndex = 4;
            this.btnQueue.Text = "Queue for Session";
            this.btnQueue.UseVisualStyleBackColor = true;
            this.btnQueue.Click += new System.EventHandler(this.btnQueue_Click);
            // 
            // txtChatUserEmail
            // 
            this.txtChatUserEmail.Enabled = false;
            this.txtChatUserEmail.Location = new System.Drawing.Point(18, 558);
            this.txtChatUserEmail.Name = "txtChatUserEmail";
            this.txtChatUserEmail.Size = new System.Drawing.Size(235, 20);
            this.txtChatUserEmail.TabIndex = 3;
            // 
            // txtChatUserName
            // 
            this.txtChatUserName.Enabled = false;
            this.txtChatUserName.Location = new System.Drawing.Point(18, 519);
            this.txtChatUserName.Name = "txtChatUserName";
            this.txtChatUserName.Size = new System.Drawing.Size(235, 20);
            this.txtChatUserName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 500);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Chat User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 542);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Chat User Email";
            // 
            // comboTenantDomain
            // 
            this.comboTenantDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTenantDomain.Enabled = false;
            this.comboTenantDomain.FormattingEnabled = true;
            this.comboTenantDomain.Location = new System.Drawing.Point(18, 50);
            this.comboTenantDomain.Name = "comboTenantDomain";
            this.comboTenantDomain.Size = new System.Drawing.Size(235, 21);
            this.comboTenantDomain.TabIndex = 0;
            this.comboTenantDomain.SelectedIndexChanged += new System.EventHandler(this.comboTenantDomain_SelectedIndexChanged);
            // 
            // comboQueueName
            // 
            this.comboQueueName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboQueueName.Enabled = false;
            this.comboQueueName.FormattingEnabled = true;
            this.comboQueueName.Location = new System.Drawing.Point(18, 90);
            this.comboQueueName.Name = "comboQueueName";
            this.comboQueueName.Size = new System.Drawing.Size(235, 21);
            this.comboQueueName.TabIndex = 1;
            this.comboQueueName.SelectedIndexChanged += new System.EventHandler(this.comboQueueName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tenant Domain";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Queue Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Available Agents: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Unavailable Agents: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Offline Agents: ";
            // 
            // lblAvailableAgents
            // 
            this.lblAvailableAgents.AutoSize = true;
            this.lblAvailableAgents.Location = new System.Drawing.Point(134, 139);
            this.lblAvailableAgents.Name = "lblAvailableAgents";
            this.lblAvailableAgents.Size = new System.Drawing.Size(13, 13);
            this.lblAvailableAgents.TabIndex = 15;
            this.lblAvailableAgents.Text = "0";
            // 
            // lblUnavailableAgents
            // 
            this.lblUnavailableAgents.AutoSize = true;
            this.lblUnavailableAgents.Location = new System.Drawing.Point(134, 157);
            this.lblUnavailableAgents.Name = "lblUnavailableAgents";
            this.lblUnavailableAgents.Size = new System.Drawing.Size(13, 13);
            this.lblUnavailableAgents.TabIndex = 16;
            this.lblUnavailableAgents.Text = "0";
            // 
            // lblOfflineAgents
            // 
            this.lblOfflineAgents.AutoSize = true;
            this.lblOfflineAgents.Location = new System.Drawing.Point(287, 139);
            this.lblOfflineAgents.Name = "lblOfflineAgents";
            this.lblOfflineAgents.Size = new System.Drawing.Size(13, 13);
            this.lblOfflineAgents.TabIndex = 17;
            this.lblOfflineAgents.Text = "0";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chatEngineToolStripMenuItem,
            this.manageToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(438, 24);
            this.menuStrip.TabIndex = 18;
            this.menuStrip.Text = "menuStrip1";
            // 
            // chatEngineToolStripMenuItem
            // 
            this.chatEngineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.chatEngineToolStripMenuItem.Name = "chatEngineToolStripMenuItem";
            this.chatEngineToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.chatEngineToolStripMenuItem.Text = "Chat Engine";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tenantDomainsToolStripMenuItem,
            this.tenantDomainQueuesToolStripMenuItem,
            this.queueAgentsToolStripMenuItem});
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.manageToolStripMenuItem.Text = "Manage";
            // 
            // tenantDomainsToolStripMenuItem
            // 
            this.tenantDomainsToolStripMenuItem.Name = "tenantDomainsToolStripMenuItem";
            this.tenantDomainsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.tenantDomainsToolStripMenuItem.Text = "Tenant Domains";
            // 
            // tenantDomainQueuesToolStripMenuItem
            // 
            this.tenantDomainQueuesToolStripMenuItem.Name = "tenantDomainQueuesToolStripMenuItem";
            this.tenantDomainQueuesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.tenantDomainQueuesToolStripMenuItem.Text = "Chat Queues";
            // 
            // queueAgentsToolStripMenuItem
            // 
            this.queueAgentsToolStripMenuItem.Name = "queueAgentsToolStripMenuItem";
            this.queueAgentsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.queueAgentsToolStripMenuItem.Text = "Queue Agents";
            // 
            // lblAgentsInSession
            // 
            this.lblAgentsInSession.AutoSize = true;
            this.lblAgentsInSession.Location = new System.Drawing.Point(287, 157);
            this.lblAgentsInSession.Name = "lblAgentsInSession";
            this.lblAgentsInSession.Size = new System.Drawing.Size(13, 13);
            this.lblAgentsInSession.TabIndex = 20;
            this.lblAgentsInSession.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(172, 157);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Agents in session:";
            // 
            // listAgentStatus
            // 
            this.listAgentStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.agentName,
            this.agentState});
            this.listAgentStatus.FullRowSelect = true;
            this.listAgentStatus.GridLines = true;
            this.listAgentStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listAgentStatus.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listAgentStatus.Location = new System.Drawing.Point(18, 173);
            this.listAgentStatus.Name = "listAgentStatus";
            this.listAgentStatus.Size = new System.Drawing.Size(400, 123);
            this.listAgentStatus.TabIndex = 21;
            this.listAgentStatus.UseCompatibleStateImageBehavior = false;
            this.listAgentStatus.View = System.Windows.Forms.View.Details;
            // 
            // agentName
            // 
            this.agentName.Text = "Agent Name";
            this.agentName.Width = 150;
            // 
            // agentState
            // 
            this.agentState.Text = "Availability";
            this.agentState.Width = 244;
            // 
            // listSessionStatus
            // 
            this.listSessionStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listSessionStatus.FullRowSelect = true;
            this.listSessionStatus.GridLines = true;
            this.listSessionStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listSessionStatus.Location = new System.Drawing.Point(18, 346);
            this.listSessionStatus.Name = "listSessionStatus";
            this.listSessionStatus.Size = new System.Drawing.Size(400, 123);
            this.listSessionStatus.TabIndex = 22;
            this.listSessionStatus.UseCompatibleStateImageBehavior = false;
            this.listSessionStatus.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Web User Name";
            this.columnHeader1.Width = 106;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "State";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "State Duration";
            this.columnHeader3.Width = 84;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Agent Name";
            this.columnHeader4.Width = 126;
            // 
            // lblNumberOfActiveChatSessions
            // 
            this.lblNumberOfActiveChatSessions.AutoSize = true;
            this.lblNumberOfActiveChatSessions.Location = new System.Drawing.Point(137, 331);
            this.lblNumberOfActiveChatSessions.Name = "lblNumberOfActiveChatSessions";
            this.lblNumberOfActiveChatSessions.Size = new System.Drawing.Size(13, 13);
            this.lblNumberOfActiveChatSessions.TabIndex = 26;
            this.lblNumberOfActiveChatSessions.Text = "0";
            // 
            // lblNumberOfWaitingChatSessions
            // 
            this.lblNumberOfWaitingChatSessions.AutoSize = true;
            this.lblNumberOfWaitingChatSessions.Location = new System.Drawing.Point(290, 330);
            this.lblNumberOfWaitingChatSessions.Name = "lblNumberOfWaitingChatSessions";
            this.lblNumberOfWaitingChatSessions.Size = new System.Drawing.Size(13, 13);
            this.lblNumberOfWaitingChatSessions.TabIndex = 25;
            this.lblNumberOfWaitingChatSessions.Text = "0";
            // 
            // Label100
            // 
            this.Label100.AutoSize = true;
            this.Label100.Location = new System.Drawing.Point(18, 330);
            this.Label100.Name = "Label100";
            this.Label100.Size = new System.Drawing.Size(107, 13);
            this.Label100.TabIndex = 24;
            this.Label100.Text = "Active Chat Sessions";
            // 
            // Label200
            // 
            this.Label200.AutoSize = true;
            this.Label200.Location = new System.Drawing.Point(172, 330);
            this.Label200.Name = "Label200";
            this.Label200.Size = new System.Drawing.Size(113, 13);
            this.Label200.TabIndex = 23;
            this.Label200.Text = "Waiting Chat Sessions";
            // 
            // lblEngineStatus
            // 
            this.lblEngineStatus.AutoSize = true;
            this.lblEngineStatus.Location = new System.Drawing.Point(306, 53);
            this.lblEngineStatus.Name = "lblEngineStatus";
            this.lblEngineStatus.Size = new System.Drawing.Size(83, 13);
            this.lblEngineStatus.TabIndex = 27;
            this.lblEngineStatus.Text = "Engine Stopped";
            // 
            // timeQueueStatus
            // 
            this.timeQueueStatus.Tick += new System.EventHandler(this.timeQueueStatus_Tick);
            // 
            // formMain
            // 
            this.AcceptButton = this.btnQueue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 593);
            this.Controls.Add(this.lblEngineStatus);
            this.Controls.Add(this.lblNumberOfActiveChatSessions);
            this.Controls.Add(this.lblNumberOfWaitingChatSessions);
            this.Controls.Add(this.Label100);
            this.Controls.Add(this.Label200);
            this.Controls.Add(this.listSessionStatus);
            this.Controls.Add(this.listAgentStatus);
            this.Controls.Add(this.lblAgentsInSession);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblOfflineAgents);
            this.Controls.Add(this.lblUnavailableAgents);
            this.Controls.Add(this.lblAvailableAgents);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboQueueName);
            this.Controls.Add(this.comboTenantDomain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtChatUserName);
            this.Controls.Add(this.txtChatUserEmail);
            this.Controls.Add(this.btnQueue);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "formMain";
            this.Text = "Chat Engine Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnQueue;
        private System.Windows.Forms.TextBox txtChatUserEmail;
        private System.Windows.Forms.TextBox txtChatUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboTenantDomain;
        private System.Windows.Forms.ComboBox comboQueueName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAvailableAgents;
        private System.Windows.Forms.Label lblUnavailableAgents;
        private System.Windows.Forms.Label lblOfflineAgents;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem chatEngineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenantDomainsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenantDomainQueuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queueAgentsToolStripMenuItem;
        private System.Windows.Forms.Label lblAgentsInSession;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView listAgentStatus;
        private System.Windows.Forms.ColumnHeader agentName;
        private System.Windows.Forms.ColumnHeader agentState;
        private System.Windows.Forms.ListView listSessionStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label lblNumberOfActiveChatSessions;
        private System.Windows.Forms.Label lblNumberOfWaitingChatSessions;
        private System.Windows.Forms.Label Label100;
        private System.Windows.Forms.Label Label200;
        private System.Windows.Forms.Label lblEngineStatus;
        private System.Windows.Forms.Timer timeQueueStatus;
    }
}

