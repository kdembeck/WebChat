using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.ChatEngine.Dashboard;

namespace KDembeck.ChatEngine
{
    public enum ChatEngineStatus { Started, Stopped, Starting, Stopping };
    public interface IChatEngine
    {
        event EventHandler<EventArgs> ChatEngineStarted; //should probably just use a state changed event and send the current ChatEngineStatus. If we're going to be hosting multiple services, might want to create our own eventargs here and passing some kind of id
        event EventHandler<EventArgs> ChatEngineStopped;
        event EventHandler<ChatTenantInitializedEventArgs> ChatTenantInitialized;
        ChatEngineStatus status { get; }
        IServiceDashboard ServiceDashboard { get; }
        IStatusDashboard StatusDashboard { get; }
        Task startEngine();
        Task stopEngine();
    }

    public class ChatTenantInitializedEventArgs
    {
        string tenantId;

        public ChatTenantInitializedEventArgs(string tenantId)
        {
            this.tenantId = tenantId;
        }
    }
}
