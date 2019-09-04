using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.EventChannel
{
    public interface IEventChannelListener
    {
        event EventHandler<EventChannelListenerEventArgs> OnEventChannelListenerEventReceived;
        Task Start(string eventChannelUri);
    }
}
