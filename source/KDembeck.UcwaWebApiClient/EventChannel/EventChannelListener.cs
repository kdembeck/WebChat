using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.EventChannel
{
    internal class EventChannelListener : IEventChannelListener
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public event EventHandler<EventChannelListenerEventArgs> OnEventChannelListenerEventReceived;
        private string eventChannelUri;        
        private IHttpUtility httpUtility;
        //private CancellationTokenSource _cancellationTokenSource;

        public EventChannelListener(IHttpUtility httpUtil)
        {
            log4net.Config.XmlConfigurator.Configure();

            httpUtility = httpUtil;
            httpUtility.baseUrl = httpUtil.baseUrl;
            httpUtility.authenticationResult = httpUtil.authenticationResult;
        }

        //Need to find a way to cancel out of the task. It's currently on an infinite loop
        public async Task Start(string eventChannelUri)
        {
            this.eventChannelUri = eventChannelUri;
            try
            {
                Task taskEventChannelListener = Task.Run(() => StartEventListener());
                await taskEventChannelListener.ContinueWith(deadListener =>
                {
                    Handle_OnEventChannelNeedsResartEvent();
                });
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Error("Error occurred in Start. Error message: " + exceptionMessage);
            }
        }

        private async Task StartEventListener()
        {
            try
            {
                while (!string.IsNullOrWhiteSpace(eventChannelUri))
                {
                    //log.Debug("Getting events at url: " + _eventChannelUri);
                    string eventsResource = await httpUtility.httpGetJson(eventChannelUri);
                    if (eventsResource != "")
                    {
                        dynamic eventsResourceObject = JObject.Parse(eventsResource);

                        if (eventsResourceObject._links.next != null)
                        {
                            eventChannelUri = httpUtility.baseUrl + eventsResourceObject._links.next.href;
                        }
                        else if (eventsResourceObject._links.resync != null)
                        {
                            eventChannelUri = httpUtility.baseUrl + eventsResourceObject._links.resync.href;
                        }
                        else if (eventsResourceObject._links.resume != null)
                        {
                            eventChannelUri = httpUtility.baseUrl + eventsResourceObject._links.resume.href;
                        }
                        else
                        {
                            //uncommenting this stops the infinite loop. The app will then call restart infinitely with an empty string as the _eventChannelUri
                            //_eventChannelUri = "";
                        }

                        Handle_OnBatchEventsNotificationsReceivedEvent(eventsResource);
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Error("Error occurred in StartEventListener. Error message: " + exceptionMessage);
            }
        }

        private void Handle_OnEventChannelNeedsResartEvent()
        {
            try
            {
                log.Debug("Restarting event channel listener task. Event channel uri: " + eventChannelUri);
                Start(eventChannelUri);
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Error("Error occurred in Handle_OnEventChannelNeedsResartEvent. Error message: " + exceptionMessage);
            }
        }

        private void Handle_OnBatchEventsNotificationsReceivedEvent(string eventNotifications)
        {
            string sender = "";
            try
            {
                //log.Debug("Batch events notifications received.");
                dynamic resultObject = JObject.Parse(eventNotifications);
                if (resultObject.sender != null)
                {
                    foreach (dynamic objSender in resultObject.sender)
                    {
                        sender = objSender.rel;
                        if (objSender.events != null)
                        {
                            foreach (dynamic objEvent in objSender.events)
                                OnEventChannelListenerEventReceived?.Invoke(this, new EventChannelListenerEventArgs(objSender, objEvent));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Error("Error occurred in Handle_OnBatchEventsNotificationsReceivedEvent. Error message: " + exceptionMessage);
            }
        }
    }

    public class EventChannelListenerEventArgs : EventArgs
    {
        public dynamic sender;
        public dynamic eventResource;

        public EventChannelListenerEventArgs(dynamic sender, dynamic eventResource)
        {
            this.sender = sender;
            this.eventResource = eventResource;            
        }
    }
}
