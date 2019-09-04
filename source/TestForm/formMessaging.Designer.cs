namespace Test_WinForm
{
    partial class formMessaging
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
            "{{time}}",
            "{{sender}}",
            "{{to}}",
            "{{direction}}",
            "{{message text}}"}, -1);
            this.label8 = new System.Windows.Forms.Label();
            this.txtMessageTextToSend1 = new System.Windows.Forms.TextBox();
            this.btnSendMessage1 = new System.Windows.Forms.Button();
            this.lblMessageRecipient = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMessageSubject = new System.Windows.Forms.TextBox();
            this.txtMessagingReceiptionSipUri = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.listviewMessagesScreen = new System.Windows.Forms.ListView();
            this.colMessageTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessageSender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessageTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessageDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessageText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnEndConversation = new System.Windows.Forms.Button();
            this.btnStartInvitation = new System.Windows.Forms.Button();
            this.btnInviteParticipant = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Send IM To";
            // 
            // txtMessageTextToSend1
            // 
            this.txtMessageTextToSend1.Location = new System.Drawing.Point(78, 363);
            this.txtMessageTextToSend1.Name = "txtMessageTextToSend1";
            this.txtMessageTextToSend1.Size = new System.Drawing.Size(497, 20);
            this.txtMessageTextToSend1.TabIndex = 28;
            this.txtMessageTextToSend1.Text = "Test first message";
            // 
            // btnSendMessage1
            // 
            this.btnSendMessage1.Location = new System.Drawing.Point(12, 361);
            this.btnSendMessage1.Name = "btnSendMessage1";
            this.btnSendMessage1.Size = new System.Drawing.Size(60, 23);
            this.btnSendMessage1.TabIndex = 27;
            this.btnSendMessage1.Text = "Send";
            this.btnSendMessage1.UseVisualStyleBackColor = true;
            this.btnSendMessage1.Click += new System.EventHandler(this.btnSendMessage1_Click);
            // 
            // lblMessageRecipient
            // 
            this.lblMessageRecipient.AutoSize = true;
            this.lblMessageRecipient.Location = new System.Drawing.Point(55, 1);
            this.lblMessageRecipient.Name = "lblMessageRecipient";
            this.lblMessageRecipient.Size = new System.Drawing.Size(0, 13);
            this.lblMessageRecipient.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Subject";
            // 
            // txtMessageSubject
            // 
            this.txtMessageSubject.Location = new System.Drawing.Point(78, 46);
            this.txtMessageSubject.Name = "txtMessageSubject";
            this.txtMessageSubject.Size = new System.Drawing.Size(203, 20);
            this.txtMessageSubject.TabIndex = 34;
            this.txtMessageSubject.Text = "Test Message Session";
            // 
            // txtMessagingReceiptionSipUri
            // 
            this.txtMessagingReceiptionSipUri.Location = new System.Drawing.Point(78, 19);
            this.txtMessagingReceiptionSipUri.Name = "txtMessagingReceiptionSipUri";
            this.txtMessagingReceiptionSipUri.Size = new System.Drawing.Size(203, 20);
            this.txtMessagingReceiptionSipUri.TabIndex = 33;
            this.txtMessagingReceiptionSipUri.Text = "sip:usera@webchat5.onmicrosoft.com";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(419, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 39;
            // 
            // listviewMessagesScreen
            // 
            this.listviewMessagesScreen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessageTime,
            this.colMessageSender,
            this.colMessageTo,
            this.colMessageDirection,
            this.colMessageText});
            this.listviewMessagesScreen.GridLines = true;
            this.listviewMessagesScreen.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listviewMessagesScreen.Location = new System.Drawing.Point(12, 78);
            this.listviewMessagesScreen.Name = "listviewMessagesScreen";
            this.listviewMessagesScreen.Size = new System.Drawing.Size(563, 279);
            this.listviewMessagesScreen.TabIndex = 41;
            this.listviewMessagesScreen.UseCompatibleStateImageBehavior = false;
            this.listviewMessagesScreen.View = System.Windows.Forms.View.Details;
            // 
            // colMessageTime
            // 
            this.colMessageTime.Text = "time";
            this.colMessageTime.Width = 100;
            // 
            // colMessageSender
            // 
            this.colMessageSender.Text = "sender";
            this.colMessageSender.Width = 100;
            // 
            // colMessageTo
            // 
            this.colMessageTo.Text = "to";
            this.colMessageTo.Width = 100;
            // 
            // colMessageDirection
            // 
            this.colMessageDirection.Text = "direction";
            this.colMessageDirection.Width = 100;
            // 
            // colMessageText
            // 
            this.colMessageText.Text = "message";
            this.colMessageText.Width = 500;
            // 
            // btnEndConversation
            // 
            this.btnEndConversation.Location = new System.Drawing.Point(454, 19);
            this.btnEndConversation.Name = "btnEndConversation";
            this.btnEndConversation.Size = new System.Drawing.Size(121, 23);
            this.btnEndConversation.TabIndex = 42;
            this.btnEndConversation.Text = "End Conversation";
            this.btnEndConversation.UseVisualStyleBackColor = true;
            this.btnEndConversation.Click += new System.EventHandler(this.btnEndConversation_Click);
            // 
            // btnStartInvitation
            // 
            this.btnStartInvitation.Location = new System.Drawing.Point(335, 19);
            this.btnStartInvitation.Name = "btnStartInvitation";
            this.btnStartInvitation.Size = new System.Drawing.Size(113, 23);
            this.btnStartInvitation.TabIndex = 43;
            this.btnStartInvitation.Text = "Start Invitation";
            this.btnStartInvitation.UseVisualStyleBackColor = true;
            this.btnStartInvitation.Click += new System.EventHandler(this.btnStartInvitation_Click);
            // 
            // btnInviteParticipant
            // 
            this.btnInviteParticipant.Location = new System.Drawing.Point(335, 43);
            this.btnInviteParticipant.Name = "btnInviteParticipant";
            this.btnInviteParticipant.Size = new System.Drawing.Size(113, 23);
            this.btnInviteParticipant.TabIndex = 44;
            this.btnInviteParticipant.Text = "Invite Participant";
            this.btnInviteParticipant.UseVisualStyleBackColor = true;
            this.btnInviteParticipant.Click += new System.EventHandler(this.btnInviteParticipant_Click);
            // 
            // formMessaging
            // 
            this.AcceptButton = this.btnSendMessage1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 395);
            this.Controls.Add(this.btnInviteParticipant);
            this.Controls.Add(this.btnStartInvitation);
            this.Controls.Add(this.btnEndConversation);
            this.Controls.Add(this.listviewMessagesScreen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMessageSubject);
            this.Controls.Add(this.txtMessagingReceiptionSipUri);
            this.Controls.Add(this.lblMessageRecipient);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtMessageTextToSend1);
            this.Controls.Add(this.btnSendMessage1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "formMessaging";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Skype for Business Online Conversation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMessaging_FormClosing);
            this.Load += new System.EventHandler(this.formMessagingSession_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMessageTextToSend1;
        private System.Windows.Forms.Button btnSendMessage1;
        private System.Windows.Forms.Label lblMessageRecipient;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMessageSubject;
        private System.Windows.Forms.TextBox txtMessagingReceiptionSipUri;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listviewMessagesScreen;
        private System.Windows.Forms.ColumnHeader colMessageTime;
        private System.Windows.Forms.ColumnHeader colMessageSender;
        private System.Windows.Forms.ColumnHeader colMessageDirection;
        private System.Windows.Forms.ColumnHeader colMessageText;
        private System.Windows.Forms.ColumnHeader colMessageTo;
        private System.Windows.Forms.Button btnEndConversation;
        private System.Windows.Forms.Button btnStartInvitation;
        private System.Windows.Forms.Button btnInviteParticipant;
    }
}