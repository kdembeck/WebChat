namespace Test_WinForm
{
    partial class formOnlineMeeting
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
            this.btnStartOnlineMeeting = new System.Windows.Forms.Button();
            this.btnInviteParticipant = new System.Windows.Forms.Button();
            this.txtInviteParticipant = new System.Windows.Forms.TextBox();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtMessageWindow = new System.Windows.Forms.TextBox();
            this.txtParticipants = new System.Windows.Forms.TextBox();
            this.btnEndMeeting = new System.Windows.Forms.Button();
            this.btnRemoveParticipant = new System.Windows.Forms.Button();
            this.txtRemoveParticipant = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartOnlineMeeting
            // 
            this.btnStartOnlineMeeting.Location = new System.Drawing.Point(12, 12);
            this.btnStartOnlineMeeting.Name = "btnStartOnlineMeeting";
            this.btnStartOnlineMeeting.Size = new System.Drawing.Size(125, 23);
            this.btnStartOnlineMeeting.TabIndex = 0;
            this.btnStartOnlineMeeting.Text = "Start Online Meeting";
            this.btnStartOnlineMeeting.UseVisualStyleBackColor = true;
            this.btnStartOnlineMeeting.Click += new System.EventHandler(this.btnStartOnlineMeeting_Click);
            // 
            // btnInviteParticipant
            // 
            this.btnInviteParticipant.Location = new System.Drawing.Point(228, 116);
            this.btnInviteParticipant.Name = "btnInviteParticipant";
            this.btnInviteParticipant.Size = new System.Drawing.Size(141, 23);
            this.btnInviteParticipant.TabIndex = 1;
            this.btnInviteParticipant.Text = "Invite Participant";
            this.btnInviteParticipant.UseVisualStyleBackColor = true;
            this.btnInviteParticipant.Click += new System.EventHandler(this.btnInviteParticipant_Click);
            // 
            // txtInviteParticipant
            // 
            this.txtInviteParticipant.Location = new System.Drawing.Point(7, 118);
            this.txtInviteParticipant.Name = "txtInviteParticipant";
            this.txtInviteParticipant.Size = new System.Drawing.Size(217, 20);
            this.txtInviteParticipant.TabIndex = 2;
            this.txtInviteParticipant.Text = "sip:usera@webchat5.onmicrosoft.com";
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.Location = new System.Drawing.Point(7, 472);
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(472, 20);
            this.txtSendMessage.TabIndex = 3;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(485, 470);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(91, 23);
            this.btnSendMessage.TabIndex = 4;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtMessageWindow
            // 
            this.txtMessageWindow.Location = new System.Drawing.Point(201, 174);
            this.txtMessageWindow.Multiline = true;
            this.txtMessageWindow.Name = "txtMessageWindow";
            this.txtMessageWindow.Size = new System.Drawing.Size(375, 282);
            this.txtMessageWindow.TabIndex = 5;
            // 
            // txtParticipants
            // 
            this.txtParticipants.Location = new System.Drawing.Point(7, 174);
            this.txtParticipants.Multiline = true;
            this.txtParticipants.Name = "txtParticipants";
            this.txtParticipants.Size = new System.Drawing.Size(188, 282);
            this.txtParticipants.TabIndex = 6;
            // 
            // btnEndMeeting
            // 
            this.btnEndMeeting.Location = new System.Drawing.Point(143, 12);
            this.btnEndMeeting.Name = "btnEndMeeting";
            this.btnEndMeeting.Size = new System.Drawing.Size(137, 23);
            this.btnEndMeeting.TabIndex = 7;
            this.btnEndMeeting.Text = "End Meeting";
            this.btnEndMeeting.UseVisualStyleBackColor = true;
            this.btnEndMeeting.Click += new System.EventHandler(this.btnEndMeeting_Click);
            // 
            // btnRemoveParticipant
            // 
            this.btnRemoveParticipant.Location = new System.Drawing.Point(228, 145);
            this.btnRemoveParticipant.Name = "btnRemoveParticipant";
            this.btnRemoveParticipant.Size = new System.Drawing.Size(141, 23);
            this.btnRemoveParticipant.TabIndex = 8;
            this.btnRemoveParticipant.Text = "Remove Participant";
            this.btnRemoveParticipant.UseVisualStyleBackColor = true;
            this.btnRemoveParticipant.Click += new System.EventHandler(this.btnRemoveParticipant_Click);
            // 
            // txtRemoveParticipant
            // 
            this.txtRemoveParticipant.Location = new System.Drawing.Point(7, 147);
            this.txtRemoveParticipant.Name = "txtRemoveParticipant";
            this.txtRemoveParticipant.Size = new System.Drawing.Size(217, 20);
            this.txtRemoveParticipant.TabIndex = 9;
            this.txtRemoveParticipant.Text = "sip:usera@webchat5.onmicrosoft.com";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(12, 63);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(265, 20);
            this.txtSubject.TabIndex = 10;
            this.txtSubject.Text = "Test Online Meeting";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Subject";
            // 
            // formOnlineMeeting
            // 
            this.AcceptButton = this.btnSendMessage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 511);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtRemoveParticipant);
            this.Controls.Add(this.btnRemoveParticipant);
            this.Controls.Add(this.btnEndMeeting);
            this.Controls.Add(this.txtParticipants);
            this.Controls.Add(this.txtMessageWindow);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.txtInviteParticipant);
            this.Controls.Add(this.btnInviteParticipant);
            this.Controls.Add(this.btnStartOnlineMeeting);
            this.Name = "formOnlineMeeting";
            this.Text = "formOnlineMeeting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formOnlineMeeting_FormClosing);
            this.Load += new System.EventHandler(this.formOnlineMeeting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartOnlineMeeting;
        private System.Windows.Forms.Button btnInviteParticipant;
        private System.Windows.Forms.TextBox txtInviteParticipant;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox txtMessageWindow;
        private System.Windows.Forms.TextBox txtParticipants;
        private System.Windows.Forms.Button btnEndMeeting;
        private System.Windows.Forms.Button btnRemoveParticipant;
        private System.Windows.Forms.TextBox txtRemoveParticipant;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label1;
    }
}