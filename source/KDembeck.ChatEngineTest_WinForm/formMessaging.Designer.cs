namespace KDembeck.ChatEngineTest_WinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMessaging));
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSendMessageText = new System.Windows.Forms.TextBox();
            this.txtMessagingWindow = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(374, 378);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 66);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSendMessageText
            // 
            this.txtSendMessageText.Location = new System.Drawing.Point(12, 378);
            this.txtSendMessageText.Multiline = true;
            this.txtSendMessageText.Name = "txtSendMessageText";
            this.txtSendMessageText.Size = new System.Drawing.Size(356, 66);
            this.txtSendMessageText.TabIndex = 0;
            // 
            // txtMessagingWindow
            // 
            this.txtMessagingWindow.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessagingWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessagingWindow.Location = new System.Drawing.Point(12, 12);
            this.txtMessagingWindow.Name = "txtMessagingWindow";
            this.txtMessagingWindow.ReadOnly = true;
            this.txtMessagingWindow.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtMessagingWindow.Size = new System.Drawing.Size(437, 360);
            this.txtMessagingWindow.TabIndex = 3;
            this.txtMessagingWindow.TabStop = false;
            this.txtMessagingWindow.Text = "";
            // 
            // formMessaging
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 455);
            this.Controls.Add(this.txtMessagingWindow);
            this.Controls.Add(this.txtSendMessageText);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "formMessaging";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMessaging_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSendMessageText;
        private System.Windows.Forms.RichTextBox txtMessagingWindow;
    }
}