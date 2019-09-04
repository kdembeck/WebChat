namespace Test_WinForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Other", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "usera@webchat.onmicrosoft.com",
            "Online",
            "In a meeting",
            "{{last active}}"}, -1);
            this.tabcontrolMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboAddRemoveContactFromContactGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMyNote = new System.Windows.Forms.TextBox();
            this.btnStartOnlineMeeting = new System.Windows.Forms.Button();
            this.txtMyLocation = new System.Windows.Forms.TextBox();
            this.comboMyAvailability = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblMyDisplayName = new System.Windows.Forms.Label();
            this.comboAddRemoveContact = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoveContact = new System.Windows.Forms.Button();
            this.btnAddContact = new System.Windows.Forms.Button();
            this.listviewContactList = new System.Windows.Forms.ListView();
            this.contactName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.presenceAvailability = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.presenceActivity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.presenceLastActive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label7 = new System.Windows.Forms.Label();
            this.btnStartNewImSession = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabcontrolMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabcontrolMain
            // 
            this.tabcontrolMain.Controls.Add(this.tabPage1);
            this.tabcontrolMain.Controls.Add(this.tabPage2);
            this.tabcontrolMain.Location = new System.Drawing.Point(12, 27);
            this.tabcontrolMain.Name = "tabcontrolMain";
            this.tabcontrolMain.SelectedIndex = 0;
            this.tabcontrolMain.Size = new System.Drawing.Size(723, 456);
            this.tabcontrolMain.TabIndex = 85;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboAddRemoveContactFromContactGroup);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtMyNote);
            this.tabPage1.Controls.Add(this.btnStartOnlineMeeting);
            this.tabPage1.Controls.Add(this.txtMyLocation);
            this.tabPage1.Controls.Add(this.comboMyAvailability);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.lblMyDisplayName);
            this.tabPage1.Controls.Add(this.comboAddRemoveContact);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnRemoveContact);
            this.tabPage1.Controls.Add(this.btnAddContact);
            this.tabPage1.Controls.Add(this.listviewContactList);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.btnStartNewImSession);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(715, 430);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Contacts";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // comboAddRemoveContactFromContactGroup
            // 
            this.comboAddRemoveContactFromContactGroup.AllowDrop = true;
            this.comboAddRemoveContactFromContactGroup.FormattingEnabled = true;
            this.comboAddRemoveContactFromContactGroup.Items.AddRange(new object[] {
            "ContactGroup1",
            "ContactGroup2"});
            this.comboAddRemoveContactFromContactGroup.Location = new System.Drawing.Point(501, 155);
            this.comboAddRemoveContactFromContactGroup.Name = "comboAddRemoveContactFromContactGroup";
            this.comboAddRemoveContactFromContactGroup.Size = new System.Drawing.Size(195, 21);
            this.comboAddRemoveContactFromContactGroup.TabIndex = 117;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 112;
            this.label1.Text = "Note";
            // 
            // txtMyNote
            // 
            this.txtMyNote.Location = new System.Drawing.Point(275, 51);
            this.txtMyNote.Name = "txtMyNote";
            this.txtMyNote.Size = new System.Drawing.Size(207, 20);
            this.txtMyNote.TabIndex = 111;
            // 
            // btnStartOnlineMeeting
            // 
            this.btnStartOnlineMeeting.Location = new System.Drawing.Point(349, 389);
            this.btnStartOnlineMeeting.Name = "btnStartOnlineMeeting";
            this.btnStartOnlineMeeting.Size = new System.Drawing.Size(133, 23);
            this.btnStartOnlineMeeting.TabIndex = 109;
            this.btnStartOnlineMeeting.Text = "Online Meeting";
            this.btnStartOnlineMeeting.UseVisualStyleBackColor = true;
            this.btnStartOnlineMeeting.Click += new System.EventHandler(this.btnStartOnlineMeeting_Click);
            // 
            // txtMyLocation
            // 
            this.txtMyLocation.Location = new System.Drawing.Point(84, 51);
            this.txtMyLocation.Name = "txtMyLocation";
            this.txtMyLocation.Size = new System.Drawing.Size(164, 20);
            this.txtMyLocation.TabIndex = 107;
            // 
            // comboMyAvailability
            // 
            this.comboMyAvailability.FormattingEnabled = true;
            this.comboMyAvailability.Location = new System.Drawing.Point(84, 23);
            this.comboMyAvailability.Name = "comboMyAvailability";
            this.comboMyAvailability.Size = new System.Drawing.Size(164, 21);
            this.comboMyAvailability.TabIndex = 106;
            this.comboMyAvailability.Text = "Online";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(30, 54);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 13);
            this.label20.TabIndex = 105;
            this.label20.Text = "Location";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(25, 26);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 13);
            this.label19.TabIndex = 104;
            this.label19.Text = "Availability";
            // 
            // lblMyDisplayName
            // 
            this.lblMyDisplayName.AutoSize = true;
            this.lblMyDisplayName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyDisplayName.Location = new System.Drawing.Point(478, 16);
            this.lblMyDisplayName.Name = "lblMyDisplayName";
            this.lblMyDisplayName.Size = new System.Drawing.Size(201, 23);
            this.lblMyDisplayName.TabIndex = 103;
            this.lblMyDisplayName.Text = "Webchat Administrator";
            // 
            // comboAddRemoveContact
            // 
            this.comboAddRemoveContact.FormattingEnabled = true;
            this.comboAddRemoveContact.Items.AddRange(new object[] {
            "ContactGroup1",
            "ContactGroup2"});
            this.comboAddRemoveContact.Location = new System.Drawing.Point(501, 128);
            this.comboAddRemoveContact.Name = "comboAddRemoveContact";
            this.comboAddRemoveContact.Size = new System.Drawing.Size(195, 21);
            this.comboAddRemoveContact.TabIndex = 100;
            this.comboAddRemoveContact.SelectedIndexChanged += new System.EventHandler(this.comboAddRemoveContact_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(498, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 97;
            this.label3.Text = "Add/Remove Contact";
            // 
            // btnRemoveContact
            // 
            this.btnRemoveContact.Location = new System.Drawing.Point(501, 178);
            this.btnRemoveContact.Name = "btnRemoveContact";
            this.btnRemoveContact.Size = new System.Drawing.Size(92, 23);
            this.btnRemoveContact.TabIndex = 96;
            this.btnRemoveContact.Text = "Remove";
            this.btnRemoveContact.UseVisualStyleBackColor = true;
            this.btnRemoveContact.Click += new System.EventHandler(this.btnRemoveContact_Click);
            // 
            // btnAddContact
            // 
            this.btnAddContact.Location = new System.Drawing.Point(604, 179);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(92, 23);
            this.btnAddContact.TabIndex = 92;
            this.btnAddContact.Text = "Add";
            this.btnAddContact.UseVisualStyleBackColor = true;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);
            // 
            // listviewContactList
            // 
            this.listviewContactList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.contactName,
            this.presenceAvailability,
            this.presenceActivity,
            this.presenceLastActive});
            this.listviewContactList.FullRowSelect = true;
            this.listviewContactList.GridLines = true;
            listViewGroup1.Header = "Other";
            listViewGroup1.Name = "listViewGroup1";
            this.listviewContactList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            listViewItem1.Group = listViewGroup1;
            this.listviewContactList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listviewContactList.Location = new System.Drawing.Point(25, 111);
            this.listviewContactList.Name = "listviewContactList";
            this.listviewContactList.Size = new System.Drawing.Size(457, 272);
            this.listviewContactList.TabIndex = 88;
            this.listviewContactList.UseCompatibleStateImageBehavior = false;
            this.listviewContactList.View = System.Windows.Forms.View.Details;
            // 
            // contactName
            // 
            this.contactName.Text = "name";
            this.contactName.Width = 194;
            // 
            // presenceAvailability
            // 
            this.presenceAvailability.Text = "availability";
            this.presenceAvailability.Width = 70;
            // 
            // presenceActivity
            // 
            this.presenceActivity.Text = "activity";
            this.presenceActivity.Width = 100;
            // 
            // presenceLastActive
            // 
            this.presenceLastActive.Text = "last active";
            this.presenceLastActive.Width = 120;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 86;
            this.label7.Text = "Subscribed Contacts";
            // 
            // btnStartNewImSession
            // 
            this.btnStartNewImSession.Location = new System.Drawing.Point(220, 389);
            this.btnStartNewImSession.Name = "btnStartNewImSession";
            this.btnStartNewImSession.Size = new System.Drawing.Size(123, 23);
            this.btnStartNewImSession.TabIndex = 85;
            this.btnStartNewImSession.Text = "IM Conversation";
            this.btnStartNewImSession.UseVisualStyleBackColor = true;
            this.btnStartNewImSession.Click += new System.EventHandler(this.btnStartNewImSession_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(715, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "My Information";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox8);
            this.groupBox1.Controls.Add(this.textBox9);
            this.groupBox1.Controls.Add(this.textBox10);
            this.groupBox1.Controls.Add(this.textBox11);
            this.groupBox1.Controls.Add(this.textBox12);
            this.groupBox1.Controls.Add(this.textBox13);
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(14, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 248);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Presence Information";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(444, 134);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(176, 20);
            this.textBox8.TabIndex = 27;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(444, 82);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(176, 20);
            this.textBox9.TabIndex = 26;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(444, 108);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(176, 20);
            this.textBox10.TabIndex = 25;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(444, 159);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(176, 20);
            this.textBox11.TabIndex = 24;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(444, 56);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(176, 20);
            this.textBox12.TabIndex = 23;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(444, 30);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(176, 20);
            this.textBox13.TabIndex = 22;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(107, 134);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(176, 20);
            this.textBox7.TabIndex = 21;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(107, 82);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(176, 20);
            this.textBox6.TabIndex = 20;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(107, 108);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(176, 20);
            this.textBox5.TabIndex = 19;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(107, 159);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(176, 20);
            this.textBox4.TabIndex = 18;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(107, 185);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(176, 20);
            this.textBox3.TabIndex = 17;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(107, 56);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(176, 20);
            this.textBox2.TabIndex = 16;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(176, 20);
            this.textBox1.TabIndex = 15;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(545, 205);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 14;
            this.button9.Text = "Refresh";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(464, 205);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "Update";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(331, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(107, 13);
            this.label17.TabIndex = 12;
            this.label17.Text = "Work Phone Number";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(418, 159);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Uri";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(77, 115);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Title";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(335, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Other Phone Number";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(25, 137);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Office Location";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(330, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Mobile Phone Number";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(339, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Home Phone Numer";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(377, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Endpoint Uri";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Email Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Department";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Company";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Availability";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Name";
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 496);
            this.Controls.Add(this.tabcontrolMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "formMain";
            this.Text = "Skype for Business UCWA Test Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formUcwaTester_Load);
            this.tabcontrolMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabcontrolMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblMyDisplayName;
        private System.Windows.Forms.ComboBox comboAddRemoveContact;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRemoveContact;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.ListView listviewContactList;
        private System.Windows.Forms.ColumnHeader contactName;
        private System.Windows.Forms.ColumnHeader presenceAvailability;
        private System.Windows.Forms.ColumnHeader presenceActivity;
        private System.Windows.Forms.ColumnHeader presenceLastActive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnStartNewImSession;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMyLocation;
        private System.Windows.Forms.ComboBox comboMyAvailability;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnStartOnlineMeeting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMyNote;
        private System.Windows.Forms.ComboBox comboAddRemoveContactFromContactGroup;
    }
}

