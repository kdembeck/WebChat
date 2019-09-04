using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public delegate void OnEventChannelListenerEventReceived(string sender, string eventResource);

    public class EventChannelListener
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OnEventChannelListenerEventReceived OnEventChannelListenerEventReceivedEvent;
        private string _eventChannelUri;
        private Task _taskEventChannelListener;
        private HttpHelper _httpHelper;

        //private CancellationTokenSource _cancellationTokenSource;

        public EventChannelListener(HttpHelper httpHelper)
        {
            log4net.Config.XmlConfigurator.Configure();

            _httpHelper = new HttpHelper();
            _httpHelper.ApplicationRootUri = httpHelper.ApplicationRootUri;
            _httpHelper.AuthenticationResult = httpHelper.AuthenticationResult;
        }

        //Need to find a way to cancel out of the task. It's currently on an infinite loop
        public async void Start(string eventChannelUri)
        {   
            _eventChannelUri = eventChannelUri;
            try
            {
                _taskEventChannelListener = Task.Run( () => StartEventListener());
                await _taskEventChannelListener.ContinueWith(deadListener =>
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
                while (!string.IsNullOrWhiteSpace(_eventChannelUri))
                {
                    log.Debug("Getting events at url: " + _eventChannelUri);
                    string eventsResource = await _httpHelper.HttpGetAction(_eventChannelUri);
                    if (eventsResource != "")
                    {
                        dynamic eventsResourceObject = JObject.Parse(eventsResource);

                        if (eventsResourceObject._links.next != null)
                        {
                            _eventChannelUri = _httpHelper.ApplicationRootUri + eventsResourceObject._links.next.href;
                        }
                        else if (eventsResourceObject._links.resync != null)
                        {
                            _eventChannelUri = _httpHelper.ApplicationRootUri + eventsResourceObject._links.resync.href;
                        }
                        else if (eventsResourceObject._links.resume != null)
                        {
                            _eventChannelUri = _httpHelper.ApplicationRootUri + eventsResourceObject._links.resume.href;
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
                log.Debug("Restarting event channel listener task. Event channel uri: " + _eventChannelUri);
                Start(_eventChannelUri);
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
                log.Debug("Batch events notifications received.");
                dynamic resultObject = JObject.Parse(eventNotifications);
                if (resultObject.sender != null)
                {
                    foreach (dynamic objSender in resultObject.sender)
                    {
                        sender = objSender.rel;
                        if (objSender.events != null)
                        {
                            foreach (dynamic objEvent in objSender.events)
                                if (OnEventChannelListenerEventReceivedEvent != null)
                                {   
                                    OnEventChannelListenerEventReceivedEvent.Invoke(sender, JsonConvert.SerializeObject(objEvent));
                                }
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
}
