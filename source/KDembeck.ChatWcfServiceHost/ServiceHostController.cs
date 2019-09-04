using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using KDembeck.ChatEngine;

namespace KDembeck.ChatWcfServiceHost
{
    public partial class ServiceHostController : ServiceBase
    {
        private IChatEngine chatEngine;

        public ServiceHostController()
        {
            InitializeComponent();            
            this.ServiceName = "WebChatBackendService";            
            this.CanStop = true;            
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            chatEngine = new ChatEngine.ChatEngine();
            chatEngine.startEngine();
        }

        protected override void OnStop()
        {
            chatEngine.stopEngine();
            chatEngine = null;
        }
    }
}
