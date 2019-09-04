using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KDembeck.UcwaWebApiClient;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace Test_WinForm
{
    public partial class formMain : Form
    {   
        private IUcwaClient ucwaClient;

        formUcwaEventChannelLog loggingForm;

        public bool LoggedIn=false;

        //public Queue<string[]> _eventScreenUpdates;

        //public System.Timers.Timer screenUpdateTimer;
        string[] contacts = { "sip:usera@webchat4.onmicrosoft.com", "sip:userb@webchat4.onmicrosoft.com" };
        formMessaging[] messagingSessions;

        List<ContactResource> contactResources;

        public formMain()
        {
            InitializeComponent();
        }

        private async Task initializeContactListView()
        {
            listviewContactList.Items.Clear();
            listviewContactList.Groups.Clear();
            comboAddRemoveContact.Items.Clear();
            comboAddRemoveContactFromContactGroup.Items.Clear();
            contactResources = new List<ContactResource>();

            foreach (string contactSipUri in contacts)
            {                
                ISearchResource searchResults = await ucwaClient.application.people.search(contactSipUri);                
                List<ContactResource> searchContactResources = searchResults.contacts;
                
                if (searchContactResources.Count > 0)
                {
                    IPresenceSubscriptionsResource presenceSubscriptions = await ucwaClient.application.people.getPresenceSubscriptions();
                    List<string> contactsUriList = new List<string>();
                    foreach (ContactResource contact in searchContactResources)
                    {
                        contactResources.Add(contact);
                        contactsUriList.Add( contact.uri.ToLower());
                        IContactPresenceResource contactPresence = await contact.getContactPresence();
                        string availability = contactPresence.availability.ToString();
                        string lastActive = contactPresence.lastActive;
                        string activity = contactPresence.activity;
                        ListViewItem newContactItem = new ListViewItem(new string[] { contact.uri.ToLower(), availability, activity, lastActive });
                        listviewContactList.Items.Add(newContactItem);
                    }

                    await presenceSubscriptions.newPresenceSubscription(30, contactsUriList);

                    //subscribe to presenceSubscriptionUpdated so we can extend the subscription
                    //subscribe to contactPresenceUpdated so we can track updates to presence
                }
            }

            listviewContactList.Update();
        }

        private async Task initializeMeProperties()
        {
            while (ucwaClient.application.me._links.makeMeAvailable != null)
                await Task.Delay(1000);
            string displayName = ucwaClient.application.me.name;
            IPresenceResource myPresence = await ucwaClient.application.me.getPresence();
            string myAvailability = myPresence.availability.ToString();
            INoteResource note = await ucwaClient.application.me.getNote();
            string myNote = note.message;
            ILocationResource location = await ucwaClient.application.me.getLocation();
            string myLocation = location.location;
            comboMyAvailability.Text = myAvailability;
            txtMyLocation.Text = myLocation;
            txtMyNote.Text = myNote;
            lblMyDisplayName.Text = displayName;
        }

        private void Handle_OnContactPresenceUpdated(object sender, UcwaContactPresenceEventArgs e)
        {
            ContactResource contact = contactResources.Where(x => x.uri == e.contact.uri).FirstOrDefault();
            if (contact != null)
            {   
                string availability = e.contactPresence.availability.ToString();
                string lastActive = e.contactPresence.lastActive;
                string activity = e.contactPresence.activity;

                Action<string, string, string, string> UpdateListviewDelegate = updateListview;
                Invoke(UpdateListviewDelegate, contact.uri, availability, activity, lastActive);
            }
        }

        private void updateListview(string contactUri, string availability, string activity, string lastActive)
        {
            ListViewItem listViewItem = listviewContactList.FindItemWithText(contactUri);
            listViewItem.SubItems[1].Text = availability;
            listViewItem.SubItems[2].Text = activity;
            listViewItem.SubItems[3].Text = lastActive;
            listviewContactList.Update();
        }

        private void btnStartNewImSession_Click(object sender, EventArgs e)
        {
            formMessaging messageForm = new formMessaging(ucwaClient);
            messageForm.Visible = true;
        }

        private async void formUcwaTester_Load(object sender, EventArgs e)
        {
            loggingForm = new formUcwaEventChannelLog();


            ucwaClient = new UcwaClient();
            formLogin loginForm = new formLogin(ucwaClient);

            DialogResult result = loginForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoggedIn = true;

                loggingForm.Show();
                ucwaClient.events.OnEventReceived += loggingForm.Handle_UcwaEventReceived;
                ucwaClient.events.OnContactPresenceUpdated += Handle_OnContactPresenceUpdated;
                
                await initializeContactListView();
                await initializeMeProperties();                
            }
            else if (result == DialogResult.Abort)
            {
                MessageBox.Show(this, "Error starting application. Application exiting.", "Error starting application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                Application.Exit();
            }
        }

        private void showUCWAEventChannelLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loggingForm.Visible)
                loggingForm.Show();
        }

        private void btnStartOnlineMeeting_Click(object sender, EventArgs e)
        {
            formOnlineMeeting onlineMeetingForm = new formOnlineMeeting(ucwaClient);
            onlineMeetingForm.Visible = true;
        }

        private void comboAddRemoveContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            //populate the contact groups list with the groups that this contact is a member of
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            //get the contactGroup id that was selected
            //add the contactUri to the contact group we selected
            //subscribe to the contact's presence
        }

        private void btnRemoveContact_Click(object sender, EventArgs e)
        {
            //get the contact group that was selected
            //remove the contact from teh contact group
            //unsubscribe from the contact's presence
        }

        private async void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LoggedIn)
            {
                await ucwaClient.application.signOut();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
