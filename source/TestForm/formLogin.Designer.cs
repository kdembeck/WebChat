namespace Test_WinForm
{
    partial class formLogin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUcwaAppRedirectUri = new System.Windows.Forms.TextBox();
            this.txtUcwaAppTenant = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUcwaAppResourceId = new System.Windows.Forms.TextBox();
            this.txtUcwaAppClientId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.buttonLogon = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboUcwaAppCulture = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUcwaAppEndpointId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUcwaAppUserAgent = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listSupportedMessageFormats = new System.Windows.Forms.ListBox();
            this.listSupportedModalities = new System.Windows.Forms.ListBox();
            this.txtUcwaInactiveTimeout = new System.Windows.Forms.TextBox();
            this.txtUcwaVoipFallbackToPhoneAudioTimeOut = new System.Windows.Forms.TextBox();
            this.txtUcwaPhoneNumber = new System.Windows.Forms.TextBox();
            this.comboUcwaSignInAs = new System.Windows.Forms.ComboBox();
            this.comboUcwaAudioPreference = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblLoginStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUcwaAppRedirectUri);
            this.groupBox1.Controls.Add(this.txtUcwaAppTenant);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUcwaAppResourceId);
            this.groupBox1.Controls.Add(this.txtUcwaAppClientId);
            this.groupBox1.Location = new System.Drawing.Point(13, 347);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 208);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Azure AD Application Settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Redirect URI";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Tenant";
            // 
            // txtUcwaAppRedirectUri
            // 
            this.txtUcwaAppRedirectUri.Location = new System.Drawing.Point(19, 169);
            this.txtUcwaAppRedirectUri.Name = "txtUcwaAppRedirectUri";
            this.txtUcwaAppRedirectUri.Size = new System.Drawing.Size(216, 20);
            this.txtUcwaAppRedirectUri.TabIndex = 25;
            this.txtUcwaAppRedirectUri.Text = "https://webchat4.onmicrosoft.com/WebChat";
            // 
            // txtUcwaAppTenant
            // 
            this.txtUcwaAppTenant.Location = new System.Drawing.Point(19, 126);
            this.txtUcwaAppTenant.Name = "txtUcwaAppTenant";
            this.txtUcwaAppTenant.Size = new System.Drawing.Size(216, 20);
            this.txtUcwaAppTenant.TabIndex = 24;
            this.txtUcwaAppTenant.Text = "webchat5.onmicrosoft.com";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Resource ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Client ID";
            // 
            // txtUcwaAppResourceId
            // 
            this.txtUcwaAppResourceId.Location = new System.Drawing.Point(19, 85);
            this.txtUcwaAppResourceId.Name = "txtUcwaAppResourceId";
            this.txtUcwaAppResourceId.Size = new System.Drawing.Size(214, 20);
            this.txtUcwaAppResourceId.TabIndex = 21;
            this.txtUcwaAppResourceId.Text = "00000004-0000-0ff1-ce00-000000000000";
            // 
            // txtUcwaAppClientId
            // 
            this.txtUcwaAppClientId.Location = new System.Drawing.Point(19, 41);
            this.txtUcwaAppClientId.Name = "txtUcwaAppClientId";
            this.txtUcwaAppClientId.Size = new System.Drawing.Size(216, 20);
            this.txtUcwaAppClientId.TabIndex = 20;
            this.txtUcwaAppClientId.Text = "d9512fb5-55a7-4597-8be0-39f070508681";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(142, 64);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(259, 20);
            this.txtPassword.TabIndex = 34;
            this.txtPassword.Text = "D3mb3ck4113";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(143, 25);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(258, 20);
            this.txtUsername.TabIndex = 33;
            this.txtUsername.Text = "webchatservice@webchat5.onmicrosoft.com";
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // buttonLogon
            // 
            this.buttonLogon.Location = new System.Drawing.Point(238, 90);
            this.buttonLogon.Name = "buttonLogon";
            this.buttonLogon.Size = new System.Drawing.Size(75, 23);
            this.buttonLogon.TabIndex = 31;
            this.buttonLogon.Text = "Logon";
            this.buttonLogon.UseVisualStyleBackColor = true;
            this.buttonLogon.Click += new System.EventHandler(this.buttonLogon_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboUcwaAppCulture);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtUcwaAppEndpointId);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtUcwaAppUserAgent);
            this.groupBox2.Location = new System.Drawing.Point(279, 347);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 208);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UCWA Application Settings";
            // 
            // comboUcwaAppCulture
            // 
            this.comboUcwaAppCulture.FormattingEnabled = true;
            this.comboUcwaAppCulture.Location = new System.Drawing.Point(18, 41);
            this.comboUcwaAppCulture.Name = "comboUcwaAppCulture";
            this.comboUcwaAppCulture.Size = new System.Drawing.Size(216, 21);
            this.comboUcwaAppCulture.TabIndex = 51;
            this.comboUcwaAppCulture.Text = "en-US";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "Culture";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Endpoint Id";
            // 
            // txtUcwaAppEndpointId
            // 
            this.txtUcwaAppEndpointId.Location = new System.Drawing.Point(18, 81);
            this.txtUcwaAppEndpointId.Name = "txtUcwaAppEndpointId";
            this.txtUcwaAppEndpointId.Size = new System.Drawing.Size(216, 20);
            this.txtUcwaAppEndpointId.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 46;
            this.label7.Text = "User Agent";
            // 
            // txtUcwaAppUserAgent
            // 
            this.txtUcwaAppUserAgent.Location = new System.Drawing.Point(18, 122);
            this.txtUcwaAppUserAgent.Name = "txtUcwaAppUserAgent";
            this.txtUcwaAppUserAgent.Size = new System.Drawing.Size(216, 20);
            this.txtUcwaAppUserAgent.TabIndex = 45;
            this.txtUcwaAppUserAgent.Text = "WebChatService";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listSupportedMessageFormats);
            this.groupBox3.Controls.Add(this.listSupportedModalities);
            this.groupBox3.Controls.Add(this.txtUcwaInactiveTimeout);
            this.groupBox3.Controls.Add(this.txtUcwaVoipFallbackToPhoneAudioTimeOut);
            this.groupBox3.Controls.Add(this.txtUcwaPhoneNumber);
            this.groupBox3.Controls.Add(this.comboUcwaSignInAs);
            this.groupBox3.Controls.Add(this.comboUcwaAudioPreference);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Location = new System.Drawing.Point(12, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(524, 214);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Skype for Business Endpoint Settings";
            // 
            // listSupportedMessageFormats
            // 
            this.listSupportedMessageFormats.FormattingEnabled = true;
            this.listSupportedMessageFormats.Items.AddRange(new object[] {
            "Html",
            "Plain"});
            this.listSupportedMessageFormats.Location = new System.Drawing.Point(268, 89);
            this.listSupportedMessageFormats.Name = "listSupportedMessageFormats";
            this.listSupportedMessageFormats.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listSupportedMessageFormats.Size = new System.Drawing.Size(234, 43);
            this.listSupportedMessageFormats.TabIndex = 24;
            // 
            // listSupportedModalities
            // 
            this.listSupportedModalities.FormattingEnabled = true;
            this.listSupportedModalities.Items.AddRange(new object[] {
            "Audio",
            "Messaging",
            "PanoramicVideo",
            "PhoneAudio",
            "Video"});
            this.listSupportedModalities.Location = new System.Drawing.Point(268, 151);
            this.listSupportedModalities.Name = "listSupportedModalities";
            this.listSupportedModalities.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listSupportedModalities.Size = new System.Drawing.Size(234, 43);
            this.listSupportedModalities.TabIndex = 23;
            // 
            // txtUcwaInactiveTimeout
            // 
            this.txtUcwaInactiveTimeout.Location = new System.Drawing.Point(21, 132);
            this.txtUcwaInactiveTimeout.Name = "txtUcwaInactiveTimeout";
            this.txtUcwaInactiveTimeout.Size = new System.Drawing.Size(237, 20);
            this.txtUcwaInactiveTimeout.TabIndex = 22;
            // 
            // txtUcwaVoipFallbackToPhoneAudioTimeOut
            // 
            this.txtUcwaVoipFallbackToPhoneAudioTimeOut.Location = new System.Drawing.Point(268, 46);
            this.txtUcwaVoipFallbackToPhoneAudioTimeOut.Name = "txtUcwaVoipFallbackToPhoneAudioTimeOut";
            this.txtUcwaVoipFallbackToPhoneAudioTimeOut.Size = new System.Drawing.Size(234, 20);
            this.txtUcwaVoipFallbackToPhoneAudioTimeOut.TabIndex = 21;
            // 
            // txtUcwaPhoneNumber
            // 
            this.txtUcwaPhoneNumber.Location = new System.Drawing.Point(21, 174);
            this.txtUcwaPhoneNumber.Name = "txtUcwaPhoneNumber";
            this.txtUcwaPhoneNumber.Size = new System.Drawing.Size(237, 20);
            this.txtUcwaPhoneNumber.TabIndex = 20;
            // 
            // comboUcwaSignInAs
            // 
            this.comboUcwaSignInAs.FormattingEnabled = true;
            this.comboUcwaSignInAs.Items.AddRange(new object[] {
            "Online",
            "Away",
            "BeRightBack",
            "Busy",
            "DoNotDisturb",
            "Offwork",
            "Online"});
            this.comboUcwaSignInAs.Location = new System.Drawing.Point(21, 46);
            this.comboUcwaSignInAs.Name = "comboUcwaSignInAs";
            this.comboUcwaSignInAs.Size = new System.Drawing.Size(237, 21);
            this.comboUcwaSignInAs.TabIndex = 16;
            this.comboUcwaSignInAs.Text = "Online";
            // 
            // comboUcwaAudioPreference
            // 
            this.comboUcwaAudioPreference.FormattingEnabled = true;
            this.comboUcwaAudioPreference.Items.AddRange(new object[] {
            "PhoneAudio",
            "VoipAudio"});
            this.comboUcwaAudioPreference.Location = new System.Drawing.Point(21, 89);
            this.comboUcwaAudioPreference.Name = "comboUcwaAudioPreference";
            this.comboUcwaAudioPreference.Size = new System.Drawing.Size(237, 21);
            this.comboUcwaAudioPreference.TabIndex = 13;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 158);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(78, 13);
            this.label17.TabIndex = 12;
            this.label17.Text = "Phone Number";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Audio Preference";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(265, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Supported Modalities";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 116);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Inactive Timeout";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Sign In As";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(264, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Supported Message Formats";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(265, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(193, 13);
            this.label18.TabIndex = 4;
            this.label18.Text = "Voip Fallback to Phone Audio Time Out";
            // 
            // lblLoginStatus
            // 
            this.lblLoginStatus.Location = new System.Drawing.Point(319, 95);
            this.lblLoginStatus.Name = "lblLoginStatus";
            this.lblLoginStatus.Size = new System.Drawing.Size(82, 23);
            this.lblLoginStatus.TabIndex = 52;
            this.lblLoginStatus.Text = "LoginStatus";
            this.lblLoginStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // formLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 570);
            this.Controls.Add(this.lblLoginStatus);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.buttonLogon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "formLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log in to Skype for Business";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.formStart_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUcwaAppRedirectUri;
        private System.Windows.Forms.TextBox txtUcwaAppTenant;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUcwaAppResourceId;
        private System.Windows.Forms.TextBox txtUcwaAppClientId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button buttonLogon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUcwaAppEndpointId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUcwaAppUserAgent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboUcwaSignInAs;
        private System.Windows.Forms.ComboBox comboUcwaAudioPreference;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtUcwaInactiveTimeout;
        private System.Windows.Forms.TextBox txtUcwaVoipFallbackToPhoneAudioTimeOut;
        private System.Windows.Forms.TextBox txtUcwaPhoneNumber;
        private System.Windows.Forms.ComboBox comboUcwaAppCulture;
        private System.Windows.Forms.Label lblLoginStatus;
        private System.Windows.Forms.ListBox listSupportedMessageFormats;
        private System.Windows.Forms.ListBox listSupportedModalities;
    }
}