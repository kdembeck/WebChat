using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.ChatEngine
{
    public enum TenantState { Starting, Started, Stopping, Stopped };
    public interface IChatTenant
    {        
        event EventHandler<TenantStateChangedEventArgs> TenantStateChanged;
        event EventHandler<TenantLoginFailedEventArgs> TenantLoginFailed;
        event EventHandler<TenantInitializedEventArgs> InitializedEvent;

        string tenantDomainName { get; }
        string tenantId { get; }
        bool initialized { get; }
        TenantState state { get; }
        IChatQueueManager chatQueueManager { get; }
        IChatAgentManager chatAgentManager { get; }
        IChatCommunicationProxy chatCommunicationProxy { get; set; }
        Task Start(string autoDiscoverServiceRootUrl, string microsoftLoginBaseUrl);              
        Task Drain();
    }

    public class TenantStateChangedEventArgs : EventArgs
    {
        public string tenantId;
        public string tenantDomain;
        public TenantState state;

        public TenantStateChangedEventArgs(string tenantId, string tenantDomain, TenantState state)
        {
            this.tenantDomain = tenantDomain;
            this.tenantId = tenantId;
            this.state = state;
        }
    }

    public class TenantLoginFailedEventArgs : EventArgs
    {
        public string tenantId;
        public string tenantDomain;
        public TenantLoginFailedEventArgs(string tenantId, string tenantDomain) 
        {
            this.tenantDomain = tenantDomain;
            this.tenantId = tenantId;
        }
    }

    public class TenantInitializedEventArgs : EventArgs
    {
        public string tenantId;
        public string tenantDomainName;

        public TenantInitializedEventArgs(string tenantId, string tenantDomainName)
        {
            this.tenantId = tenantId;
            this.tenantDomainName = tenantDomainName;
        }
    }

    public class TenantEventArgs : EventArgs
    {
        public string tenantId;
        public string tenantDomainName;

        public TenantEventArgs(string tenantId, string tenantDomainName)
        {
            this.tenantId = tenantId;
            this.tenantDomainName = tenantDomainName;
        }
    }
}
