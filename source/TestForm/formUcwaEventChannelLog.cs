using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace Test_WinForm
{
    public partial class formUcwaEventChannelLog : Form
    {

        public Queue<string[]> _eventScreenUpdates;
        private Queue<string[]> _applicationLogUpdates;

        public formUcwaEventChannelLog()
        {
            InitializeComponent();
        }

        private void formLogging_Load(object sender, EventArgs e)
        {
            _eventScreenUpdates = new Queue<string[]>();
            _applicationLogUpdates = new Queue<string[]>();

            timerScreenRefresh.Interval = 1000;
            timerScreenRefresh.Start();
        }

        private void timerScreenRefresh_Tick(object sender, EventArgs e)
        {
            timerScreenRefresh.Stop();

            try
            {
                //Object lockObject = new Object();
                //lock (lockObject)
                //{
                while (_eventScreenUpdates.Count > 0)
                {
                    string[] eventRow = _eventScreenUpdates.Dequeue();
                    ListViewItem newListViewItem = new ListViewItem(eventRow);
                    listviewEventView.Items.Insert(0, newListViewItem);
                }
                Application.DoEvents();
                //}
            }
            catch { }

            timerScreenRefresh.Start();
        }

        public void Handle_UcwaEventReceived(object sender, EventChannelListenerEventArgs eventArgs)
        {
            try
            {
                string eventTime = "", eventSender = "", eventRel = "", eventType = "", eventHref = "", event_embedded = "";



                dynamic eventObject = eventArgs.eventResource;

                eventTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                eventSender = eventArgs.sender.rel;
                if (eventObject.link != null)
                {
                    eventRel = eventObject.link.rel;
                    eventHref = eventObject.link.href;
                }
                eventType = eventObject.type;
                event_embedded = JsonConvert.SerializeObject(eventObject._embedded);

                Object lockObject = new Object();
                lock (lockObject)
                {
                    _eventScreenUpdates.Enqueue(new string[] { eventTime, eventSender, eventRel, eventType, eventHref, event_embedded });
                }
            }
            catch (Exception ex) { throw ex;  }
        }

        public void Handle_ApplicationLogReceived(string logTime, string logType, string logMessage)
        {

        }

        private void formUcwaEventChannelLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
