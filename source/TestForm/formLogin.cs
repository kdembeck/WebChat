using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using UcwaTools;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.Resources;

namespace Test_WinForm
{
    public partial class formLogin : Form
    {
        public IUcwaClient ucwaClient;

        public formLogin()
        {
            InitializeComponent();
        }

        public formLogin(IUcwaClient UcwaClient)
        {
            InitializeComponent();
            ucwaClient = UcwaClient;
        }

        private void formStart_Load(object sender, EventArgs e)
        {
            txtUcwaAppEndpointId.Text = Guid.NewGuid().ToString();
            lblLoginStatus.Text = "";
        }

        private async void buttonLogon_Click(object sender, EventArgs e)
        {
            try
            {   
                lblLoginStatus.Text = "Logging in";
                this.Text = "Logging in to Skype for Business...";

                List<MessageFormat> messageFormats = new List<MessageFormat>();
                messageFormats.Add(MessageFormat.Plain);

                List<ModalityType> modalities = new List<ModalityType>();
                modalities.Add(ModalityType.Messaging);

                if (await ucwaClient.loginAs(
                    txtUsername.Text,
                    txtPassword.Text,
                    txtUcwaAppTenant.Text,
                    txtUcwaAppClientId.Text,                    
                    "https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root",
                    "https://login.microsoftonline.com",
                    txtUcwaAppUserAgent.Text,
                    txtUcwaAppEndpointId.Text,
                    comboUcwaAppCulture.Text,
                    null,
                    null,
                    null,
                    null,
                    PreferredAvailability.Online,
                    messageFormats,
                    modalities,
                    null
                    ))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    lblLoginStatus.Text = "Login failed";
                    this.Text = "Log in to Skype for Business";
                }
            }
            catch (Exception ex)
            {
                lblLoginStatus.Text = "Login failed";
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                    errorMessage += "\nInnerException message " + ex.InnerException.Message;

                MessageBox.Show(this, "An unhandled error occurred while logging in.\nException message: " + errorMessage, "Error");
                this.DialogResult = DialogResult.Abort;
            }
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
