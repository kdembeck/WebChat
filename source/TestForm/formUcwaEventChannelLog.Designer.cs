namespace Test_WinForm
{
    partial class formUcwaEventChannelLog
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "{{time}}",
            "{{sender}}",
            "{{rel}}",
            "{{href}}",
            "{{type}}",
            "{{ event._embedded JSON object string }}"}, -1);
            this.timerScreenRefresh = new System.Windows.Forms.Timer(this.components);
            this.listviewEventView = new System.Windows.Forms.ListView();
            this.eventTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventSender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventLinkRel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventLinkHref = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventEmbedded = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // timerScreenRefresh
            // 
            this.timerScreenRefresh.Tick += new System.EventHandler(this.timerScreenRefresh_Tick);
            // 
            // listviewEventView
            // 
            this.listviewEventView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.eventTime,
            this.eventSender,
            this.eventLinkRel,
            this.eventType,
            this.eventLinkHref,
            this.eventEmbedded});
            this.listviewEventView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listviewEventView.FullRowSelect = true;
            this.listviewEventView.GridLines = true;
            this.listviewEventView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewEventView.HideSelection = false;
            this.listviewEventView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listviewEventView.Location = new System.Drawing.Point(0, 0);
            this.listviewEventView.MultiSelect = false;
            this.listviewEventView.Name = "listviewEventView";
            this.listviewEventView.Size = new System.Drawing.Size(752, 536);
            this.listviewEventView.TabIndex = 35;
            this.listviewEventView.UseCompatibleStateImageBehavior = false;
            this.listviewEventView.View = System.Windows.Forms.View.Details;
            // 
            // eventTime
            // 
            this.eventTime.Text = "time";
            this.eventTime.Width = 150;
            // 
            // eventSender
            // 
            this.eventSender.Text = "sender";
            this.eventSender.Width = 100;
            // 
            // eventLinkRel
            // 
            this.eventLinkRel.Text = "link.rel";
            this.eventLinkRel.Width = 100;
            // 
            // eventType
            // 
            this.eventType.Text = "type";
            this.eventType.Width = 100;
            // 
            // eventLinkHref
            // 
            this.eventLinkHref.Text = "link.href";
            this.eventLinkHref.Width = 100;
            // 
            // eventEmbedded
            // 
            this.eventEmbedded.Text = "_embedded";
            this.eventEmbedded.Width = 500;
            // 
            // formUcwaEventChannelLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 536);
            this.Controls.Add(this.listviewEventView);
            this.Name = "formUcwaEventChannelLog";
            this.Text = "Skype for Business UCWA Test Application Event Channel Logging";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formUcwaEventChannelLog_FormClosing);
            this.Load += new System.EventHandler(this.formLogging_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerScreenRefresh;
        private System.Windows.Forms.ListView listviewEventView;
        private System.Windows.Forms.ColumnHeader eventTime;
        private System.Windows.Forms.ColumnHeader eventSender;
        private System.Windows.Forms.ColumnHeader eventLinkRel;
        private System.Windows.Forms.ColumnHeader eventType;
        private System.Windows.Forms.ColumnHeader eventLinkHref;
        private System.Windows.Forms.ColumnHeader eventEmbedded;
    }
}