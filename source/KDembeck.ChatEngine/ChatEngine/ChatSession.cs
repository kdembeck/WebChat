using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Net;
using KDembeck.UcwaWebApiClient.Resources;
using KDembeck.UcwaWebApiClient.EventChannel;

namespace KDembeck.ChatEngine
{       
    public class ChatSession : IChatSession
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const int PARTICIPANT_INVITATION_TIMEOUT = 9000;

        //public event EventHandler<ChatSessionInitializedEventArgs> ChatSessionInitialized;
        public event EventHandler<ConversationEndedEventArgs> ChatSessionEnded;
        public event EventHandler<OutgoingConversationInvitationEventArgs> OutgoingConversationInvitationStarted;
        public event EventHandler<OutgoingConversationInvitationEventArgs> OutgoingConversationInvitationAccepted;
        public event EventHandler<OutgoingConversationInvitationEventArgs> OutgoingConversationInvitationDeclined;
        public event EventHandler<ChatSessionParticipantEventArgs> ChatSessionParticipantAdded;
        public event EventHandler<ChatSessionParticipantEventArgs> ChatSessionParticipantDeleted;
        public event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedPlainText;
        public event EventHandler<ConversationMessageReceivedEventArgs> ChatSessionChatMessageReceivedHtml;
        public event EventHandler<ConversationStatusMessageReceivedEventArgs> ChatSessionStatusMessageReceived;
        public event EventHandler<WebUserLeftConversationEventArgs> WebUserLeftConversationEvent;

        //public event EventHandler<EventArgs> AgentInitiatedConversationEnd;

        //private IChatAgent invitedChatAgent;
        private string invitedAgentSipUri;
        private IEventHandler ucwaEvents;
        private ICommunicationResource ucwaCommunication;
        private IConversationResource ucwaConversation;
        private IMessagingResource ucwaMessaging;
        private List<IParticipantResource> ucwaParticipants;
        private IParticipantResource ucwaLocalParticipant;
        private List<SentMessage> sentMessages;
        private Timer participantInvitationTimer;
        private IParticipantInvitationResource participantInvitation;
        
        private bool subscribedToParticipantAddedEvent = false;
        private bool subscribedToParticipantUpdatedDeletedEvents = false;
        private bool subscribedToParticipantInvitationStartedEvent = false;
        private bool subscribedToParticipantInvitationUpdatedEvent = false;
        private bool subscribedToParticipantInvitationCompletedEvent = false;
        private bool subscribedToOnlineMeetingInvitationStartedEvent = false;
        private bool subscribedToMessagingInvitationCompletedEvent = false;
        private bool subscribedToMessagingUpdatedEvent = false;
        private bool subscribedToConversationUpdatedEvent = false;
        private bool subscribedToOnlineMeetingInvitationCompletedEvent = false;
        private bool subscribedToConversationAddedEvent = false;
        private bool subscribedToMessageCompletedEvent = false;
        private bool webUserIsParticipant = true;

        private bool onlineMeetingStarted = false;  //what am i using these for again? 
        private bool messagingStarted = false;

        private string startOnlineMeetingInvitationOperationId;
        private string participantInvitationOperationId;
        private string addMessagingOperationId;
        private string sendMessageOperationId;

        public ChatSessionState sessionState { get; private set; }
        public string invitedAgentDisplayName { get; private set; } 
        public List<string> participantDisplayNames { get; private set; }
        public string queueId { get; private set; }
        public string queueName { get; private set; }
        public string webUserName { get; private set; }
        public string webUserEmail { get; private set; }
        public string extendedData { get; private set; }
        public bool initialized { get; private set; }
        public string conversationId { get; private set; }
        public string threadId { get; private set; }        
        public DateTime queuedTime { get; set; }
        public DateTime messagingStartTime { get; private set; }
        public DateTime messagingEndTime { get; private set; }
        public MessageHistory messageHistory { get; private set; }

        public ChatSession(string webUserName, string webUserEmail, string extendedData, string queueId, string queueName, ICommunicationResource communication, IEventHandler events, string conversationId = null)
        {   
            this.webUserName = webUserName;
            this.webUserEmail = webUserEmail;
            this.extendedData = extendedData;
            this.ucwaCommunication = communication;
            this.ucwaEvents = events;
            this.queueId = queueId;
            this.queueName = queueName;
            this.queuedTime = DateTime.Now;                    
            messageHistory = new MessageHistory();            
            sentMessages = new List<SentMessage>();
            ucwaParticipants = new List<IParticipantResource>();
            participantDisplayNames = new List<string>();
            if (string.IsNullOrEmpty(conversationId))
                this.conversationId = Guid.NewGuid().ToString();
            else
                this.conversationId = conversationId;
            participantInvitationTimer = new Timer();
            participantInvitationTimer.Interval = PARTICIPANT_INVITATION_TIMEOUT;
            participantInvitationTimer.Elapsed += Handle_ParticipantInvitationTimerElapsed;
        }

        public async Task initialize()
        {
            this.sessionState = ChatSessionState.Initializing;
            await startOnlineMeeting(queueName);
        }

        public async Task inviteParticipant(string chatAgentSipUri, string chatAgentDisplayName)
        {
            ucwaParticipants = await ucwaConversation.getParticipants();

            IParticipantResource agentParticipant = ucwaParticipants.Where(x => x.uri == chatAgentSipUri).FirstOrDefault();
            if (agentParticipant == null)
            {
                invitedAgentSipUri = chatAgentSipUri;
                invitedAgentDisplayName = chatAgentDisplayName;                

                if (!subscribedToParticipantAddedEvent)
                {
                    ucwaEvents.OnParticipantAdded += Handle_OnParticipantAdded;
                    subscribedToParticipantAddedEvent = true;
                }

                if (!subscribedToParticipantInvitationStartedEvent)
                {
                    ucwaEvents.OnParticipantInvitationStarted += Handle_OnParticipantInvitationStarted;
                    subscribedToParticipantInvitationStartedEvent = true;
                }

                if (!subscribedToParticipantInvitationCompletedEvent)
                {
                    ucwaEvents.OnParticipantInvitationCompleted += Handle_OnParticipantInvitationCompleted;
                    subscribedToParticipantInvitationCompletedEvent = true;
                }

                subscribedToParticipantInvitationCompletedEvent = true;
                participantInvitationOperationId = Guid.NewGuid().ToString();
                log.Debug("Inviting agent: " + chatAgentSipUri + " to conversation with id: " + conversationId);
                await ucwaConversation.addParticipant(chatAgentSipUri, participantInvitationOperationId);
                participantInvitationTimer.Start();
                this.sessionState = ChatSessionState.InvitationPending;
                OutgoingConversationInvitationStarted?.Invoke(this, new OutgoingConversationInvitationEventArgs(chatAgentSipUri, conversationId));
            }
            else
            {
                //this person is already a participant
            }
        }

        public async Task drain()
        {
            log.Debug("Draining conversation with conversation id: " + conversationId);
            ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, "Chat services are shutting down. We apologize for the inconvenience."));
            if (messagingStarted && ucwaMessaging != null)
            {
                if (ucwaParticipants.Count > 0)
                {
                    await sendStatusMessageToAgent("Chat services are shutting down. We apologize for the inconvenience.");
                }
            }
            await endOnlineMeeting();
        }

        private async Task startOnlineMeeting(string subject)
        {   
            ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, "Initializing chat session..."));
            ucwaEvents.OnOnlineMeetingInvitationStarted += Handle_OnlineMeetingInvitationStarted;
            subscribedToOnlineMeetingInvitationStartedEvent = true;
            startOnlineMeetingInvitationOperationId = Guid.NewGuid().ToString();
            await ucwaCommunication.startOnlineMeeting(subject, Importance.Normal, startOnlineMeetingInvitationOperationId);
        }

        private async Task addMessaging()
        {   
            if (ucwaConversation != null)
            {
                if (ucwaMessaging == null)
                    ucwaMessaging = await ucwaConversation.getMessaging();
                
                ucwaEvents.OnMessagingInvitationCompleted += Handle_OnMessagingInvitationCompleted;
                ucwaEvents.OnMessagingUpdated += Handle_OnMessagingUpdated;
                subscribedToMessagingInvitationCompletedEvent = true;
                subscribedToMessagingUpdatedEvent = true;
                addMessagingOperationId = Guid.NewGuid().ToString();
                await ucwaMessaging.addMessaging(addMessagingOperationId);
            }
        }

        public async Task sendChatMessageToAgent(string messageText)
        {
            if (ucwaMessaging != null)
            {
                string sendMessageText = "<span style =\"margin-bottom:0pt;line-height:normal;font-size:10pt;font-family:&quot;Segoe UI&quot;,sans-serif;color:black;\"><p><b>" + webUserName + "&nbsp;says:&nbsp;</b></p>" + WebUtility.HtmlEncode(messageText) + "</span>";
                string sendMessageOperationId = Guid.NewGuid().ToString();
                sentMessages.Add(new SentMessage(messageText, sendMessageOperationId, SentMessageType.Chat));
                await ucwaMessaging.sendMessageHtml(sendMessageText, sendMessageOperationId);
            }
        }

        private async Task sendStatusMessageToAgent(string messageText)
        {
            if (ucwaMessaging != null)
            {   
                string sendMessageText = "<span style=\"margin-bottom:0pt; line-height:normal; font-size:10pt; font-family:&quot;Segoe UI&quot;,sans-serif; color: black;\"><b>" + WebUtility.HtmlEncode(messageText) + "</b></span>";
                string sendMessageOperationId = Guid.NewGuid().ToString();
                sentMessages.Add(new SentMessage(messageText, sendMessageOperationId, SentMessageType.Status));
                await ucwaMessaging.sendMessageHtml(sendMessageText, sendMessageOperationId);
            }
        }

        private async Task endOnlineMeeting()   //call this for a normal end, use drain if we're shutting down the system
        {
            if (webUserIsParticipant)
            {
                ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, "Chat session has ended."));
            }
            unsubscribeFromEvents();
            if (ucwaConversation != null)
            {
                await ucwaConversation.Delete();
            }
            ChatSessionEnded?.Invoke(this, new ConversationEndedEventArgs(conversationId, webUserName, webUserEmail, messageHistory));
        }

        public async Task webUserLeftConversation()
        {
            webUserIsParticipant = false;            
            if (ucwaParticipants.Count > 0)
            {
                this.sessionState = ChatSessionState.PostSession;
                await sendStatusMessageToAgent(webUserName + " has left the conversation. Close this conversation window to make yourself available for another incoming chat session.");                
            }
            else
            {                
                await endOnlineMeeting();
            }
            WebUserLeftConversationEvent?.Invoke(this, new WebUserLeftConversationEventArgs(conversationId));
        }

        private void unsubscribeFromEvents()
        {
            if (subscribedToParticipantAddedEvent)
            {
                ucwaEvents.OnParticipantAdded -= Handle_OnParticipantAdded;
                subscribedToParticipantAddedEvent = false;
            }
            if (subscribedToParticipantUpdatedDeletedEvents)
            {
                ucwaEvents.OnParticipantUpdated -= Handle_OnParticipantUpdated;
                subscribedToParticipantUpdatedDeletedEvents = false;
            }
            if (subscribedToParticipantInvitationCompletedEvent)
            {
                ucwaEvents.OnParticipantInvitationCompleted -= Handle_OnParticipantInvitationCompleted;
                subscribedToParticipantInvitationCompletedEvent = false;
            }
            if (subscribedToParticipantInvitationUpdatedEvent)
            {
                ucwaEvents.OnParticipantInvitationUpdated -= Handle_OnParticipantInvitationUpdated;
                subscribedToParticipantInvitationUpdatedEvent = false;
            }
            if (subscribedToParticipantInvitationStartedEvent)
            {
                ucwaEvents.OnParticipantInvitationStarted -= Handle_OnParticipantInvitationStarted;
                subscribedToParticipantInvitationStartedEvent = false;
            }
            if (subscribedToOnlineMeetingInvitationStartedEvent)
            {
                ucwaEvents.OnOnlineMeetingInvitationStarted -= Handle_OnlineMeetingInvitationStarted;
                subscribedToOnlineMeetingInvitationStartedEvent = false;
            }
            if (subscribedToMessagingInvitationCompletedEvent)
            {
                ucwaEvents.OnMessagingInvitationCompleted -= Handle_OnMessagingInvitationCompleted;
                subscribedToMessagingInvitationCompletedEvent = false;
            }
            if (subscribedToMessagingUpdatedEvent)
            {
                ucwaEvents.OnMessagingUpdated -= Handle_OnMessagingUpdated;
                subscribedToMessagingUpdatedEvent = false;
            }
            if (subscribedToConversationUpdatedEvent)
            {
                ucwaEvents.OnConversationUpdated -= Handle_OnConversationUpdated;
                subscribedToConversationUpdatedEvent = false;
            }
            if (subscribedToOnlineMeetingInvitationCompletedEvent)
            {
                ucwaEvents.OnOnlineMeetingInvitationCompleted -= Handle_OnlineMeetingInvitationCompleted;
                subscribedToOnlineMeetingInvitationCompletedEvent = false;
            }
            if (subscribedToConversationAddedEvent)
            {
                ucwaEvents.OnConversationAdded -= Handle_OnConversationAdded;
                subscribedToConversationAddedEvent = false;
            }
            if (subscribedToMessageCompletedEvent)
            {
                ucwaEvents.OnMessageCompleted -= Handle_OnMessageCompleted;
                subscribedToMessageCompletedEvent = false;
            }
        }

        private void Handle_OnlineMeetingInvitationStarted(object sender, UcwaOnlineMeetingInvitationEventArgs e)
        {   
            if (e.onlineMeetingInvitation.operationId == startOnlineMeetingInvitationOperationId)
            {
                threadId = e.onlineMeetingInvitation.threadId;
                ucwaEvents.OnOnlineMeetingInvitationStarted -= Handle_OnlineMeetingInvitationStarted;
                ucwaEvents.OnOnlineMeetingInvitationCompleted += Handle_OnlineMeetingInvitationCompleted;
                ucwaEvents.OnConversationAdded += Handle_OnConversationAdded;
                subscribedToOnlineMeetingInvitationStartedEvent = false;
                subscribedToOnlineMeetingInvitationCompletedEvent = false;
                subscribedToConversationAddedEvent = false;
            }            
        }

        private async void Handle_OnlineMeetingInvitationCompleted(object sender, UcwaOnlineMeetingInvitationEventArgs e)
        {   
            if (e.onlineMeetingInvitation.operationId == startOnlineMeetingInvitationOperationId)
            {
                ucwaEvents.OnOnlineMeetingInvitationCompleted -= Handle_OnlineMeetingInvitationCompleted;
                subscribedToOnlineMeetingInvitationCompletedEvent = false;

                if (e.onlineMeetingInvitation.state == InvitationState.Connected)
                {
                    onlineMeetingStarted = true;
                    if (ucwaConversation == null)
                    {
                        ucwaConversation = await e.onlineMeetingInvitation.getConversation();
                    }

                    ucwaLocalParticipant = await ucwaConversation.getLocalParticipant();
                    
                    await addMessaging();
                }
                else
                {   
                    ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, "An error occurred while establishing the messaging session. Please try again later."));
                    ucwaEvents.OnConversationAdded -= Handle_OnConversationAdded;
                    subscribedToConversationAddedEvent = false;
                    await endOnlineMeeting();
                }
            }        
        }

        private void Handle_OnConversationAdded(object sender, UcwaConversationEventArgs e)
        {         
            if (e.conversation.threadId == threadId)
            {
                ucwaEvents.OnConversationAdded -= Handle_OnConversationAdded;
                ucwaEvents.OnConversationUpdated += Handle_OnConversationUpdated;
                subscribedToConversationAddedEvent = false;
                subscribedToConversationUpdatedEvent = true;
            }
        }

        private void Handle_OnConversationUpdated(object sender, UcwaConversationEventArgs e)
        {   
            if (e.conversation.threadId == threadId)
            {
                ucwaConversation = e.conversation;
            }
        }

        private async void Handle_OnMessagingInvitationCompleted(object sender, UcwaMessagingInvitationEventArgs e)
        {
            if (e.messagingInvitation.operationId == addMessagingOperationId)
            {
                if (e.messagingInvitation.state == InvitationState.Connected)
                {
                    messagingStarted = true;
                    ucwaEvents.OnMessagingInvitationCompleted -= Handle_OnMessagingInvitationCompleted;
                    ucwaEvents.OnMessageCompleted += Handle_OnMessageCompleted;
                    subscribedToMessagingInvitationCompletedEvent = false;
                    subscribedToMessageCompletedEvent = true;
                    this.sessionState = ChatSessionState.Waiting; 
                    this.initialized = true;
                    ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, "One moment while we locate an agent to assist you..."));
                }
                else
                {   
                    ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, "An error occurred while establishing the messaging session. Please try again later."));
                    await endOnlineMeeting();
                }
            }            
        }
              
        private void Handle_OnMessagingUpdated(object sender, UcwaMessagingEventArgs e)
        {
            if (ucwaMessaging != null)
            {
                if (e.messaging._links.self.href == ucwaMessaging._links.self.href)
                {   
                    ucwaMessaging = e.messaging;
                }
            }
        }

        private void Handle_OnParticipantInvitationStarted(object sender, UcwaParticipantInvitationEventArgs e)
        {
            if (e.participantInvitation.operationId == participantInvitationOperationId)
            {
                ucwaEvents.OnParticipantInvitationStarted -= Handle_OnParticipantInvitationStarted;
                subscribedToParticipantInvitationStartedEvent = false;
                participantInvitation = e.participantInvitation;

                ucwaEvents.OnParticipantInvitationUpdated += Handle_OnParticipantInvitationUpdated;
                subscribedToParticipantInvitationUpdatedEvent = true;

            }
        }

        private void Handle_OnParticipantInvitationUpdated(object sender, UcwaParticipantInvitationEventArgs e)
        {
            //THIS DOESN'T DO SHIT
            if (e.participantInvitation.operationId == participantInvitationOperationId)
            {
                //ucwaEvents.OnParticipantInvitationStarted -= Handle_OnParticipantInvitationStarted;
                //subscribedToParticipantInvitationStartedEvent = false;
                participantInvitation = e.participantInvitation;
            }
        }

        private void Handle_OnParticipantInvitationCompleted(object sender, UcwaParticipantInvitationEventArgs e)
        {   
            if (e.participantInvitation.operationId == participantInvitationOperationId)
            {
                participantInvitationTimer.Stop();
                
                if (e.participantInvitation.state == InvitationState.Failed)
                {
                    this.sessionState = ChatSessionState.Waiting;
                    ucwaEvents.OnParticipantAdded -= Handle_OnParticipantAdded;
                    subscribedToParticipantAddedEvent = false;
                    OutgoingConversationInvitationDeclined?.Invoke(this, new OutgoingConversationInvitationEventArgs(invitedAgentSipUri, conversationId));
                    invitedAgentSipUri = string.Empty;
                    invitedAgentDisplayName = string.Empty;
                }
                else
                {   
                    this.sessionState = ChatSessionState.Active;
                    OutgoingConversationInvitationAccepted?.Invoke(this, new OutgoingConversationInvitationEventArgs(e.participantInvitation.to, conversationId));                    
                }
                ucwaEvents.OnParticipantInvitationCompleted -= Handle_OnParticipantInvitationCompleted;
                subscribedToParticipantInvitationCompletedEvent = false;
                ucwaEvents.OnParticipantInvitationUpdated -= Handle_OnParticipantInvitationUpdated;
                subscribedToParticipantInvitationUpdatedEvent = false;
            }
        }

        private async void Handle_ParticipantInvitationTimerElapsed(object sender, ElapsedEventArgs e)
        {
            participantInvitationTimer.Stop();
            //await participantInvitation.cancel();
        }

        private async void Handle_OnParticipantAdded(object sender, UcwaParticipantEventArgs e)
        {   

            //CHECK FOR INVITATION TIMEOUT AND INVITED PARTICIPANT. IF WE'RE IN TIMEOUT ON THIS PARTICIPANT (WE COULD HAVE ALREADY INVITED ANOTHER PARTICIPANT), REMOVE THIS PARTICIPANT

            if (e.contextId == participantInvitationOperationId || e.contextId == threadId)
            {   
                ucwaParticipants.Add(e.participant);
                participantDisplayNames.Add(e.participant.name);
                ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, e.participant.name + " has joined the conversation."));
                if (!subscribedToParticipantUpdatedDeletedEvents)
                {
                    ucwaEvents.OnParticipantUpdated += Handle_OnParticipantUpdated;
                    ucwaEvents.OnParticipantDeleted += Handle_OnParticipantDeleted;
                    subscribedToParticipantUpdatedDeletedEvents = true;                    
                }

                await sendStatusMessageToAgent("You have joined a chat session for " + queueName);

                if (e.contextId == participantInvitationOperationId)
                {
                    //this is the first skype agent to join the conversation (the invited agent). Send them a message about the web user info
                    await sendStatusMessageToAgent(webUserName + " (" + webUserEmail + ") has joined the conversation");
                    invitedAgentDisplayName = e.participant.name;
                    messagingStartTime = DateTime.Now;
                }
                ChatSessionParticipantAdded?.Invoke(this, new ChatSessionParticipantEventArgs(e.participant.uri, invitedAgentSipUri, conversationId, sessionState));
            }
        }

        private void Handle_OnParticipantUpdated(object sender, UcwaParticipantEventArgs e)
        {
            IParticipantResource participant = ucwaParticipants.Where(x => x.uri == e.participant.uri).FirstOrDefault();
            if (participant != null)
            {
                ucwaParticipants.RemoveAll(x => x.uri == e.participant.uri);
                ucwaParticipants.Add(e.participant);
            }
        }

        private async void Handle_OnParticipantDeleted(object sender, UcwaParticipantEventArgs e)
        {
            IParticipantResource participant = ucwaParticipants.Where(x => x?._links?.self?.href == e.participantLinkHref).FirstOrDefault();
            if (participant != null)
            {   

                ChatSessionParticipantDeleted?.Invoke(this, new ChatSessionParticipantEventArgs(participant.uri, invitedAgentSipUri, conversationId, sessionState));
            
                if (webUserIsParticipant)
                    ChatSessionStatusMessageReceived?.Invoke(this, new ConversationStatusMessageReceivedEventArgs(conversationId, participant.name + " has left the conversation."));

                ucwaParticipants.RemoveAll(x => x.uri == participant.uri);
                participantDisplayNames.RemoveAll(x => x == participant.name);
                if (ucwaParticipants.Count == 0)
                {
                    ucwaEvents.OnParticipantAdded -= Handle_OnParticipantAdded;
                    ucwaEvents.OnParticipantUpdated -= Handle_OnParticipantUpdated;
                    ucwaEvents.OnParticipantDeleted -= Handle_OnParticipantDeleted;
                    subscribedToParticipantUpdatedDeletedEvents = false;
                    subscribedToParticipantAddedEvent = false;
                    await endOnlineMeeting();                    
                }
            }
        }

        private void Handle_OnMessageCompleted(object sender, UcwaMessageEventArgs e)
        {
            if (e.message._links.messaging.href == ucwaMessaging._links.self.href)
            {
                if (e.message.direction == "Incoming")
                {   
                    string decodedPlainTextMessage = decodeReceivedMessageText(e.message.plainMessage);
                    string decodedHtmlTextMessage = decodeReceivedMessageText(e.message.htmlMessage);
                    string senderDisplayName = e.message._links.participant.title;
                    ChatSessionChatMessageReceivedHtml?.Invoke(this, new ConversationMessageReceivedEventArgs(senderDisplayName, conversationId, decodedHtmlTextMessage));
                    ChatSessionChatMessageReceivedPlainText?.Invoke(this, new ConversationMessageReceivedEventArgs(senderDisplayName, conversationId, decodedPlainTextMessage));
                }            
                else
                {
                    //this was a message we sent out to the skype online meeting
                    SentMessage sentMessage = sentMessages.Where(x => x.operationId == e.message.operationId).FirstOrDefault();
                    if (sentMessage != null)
                    {
                        if (sentMessage.type == SentMessageType.Chat)
                        {
                            string decodedPlainTextMessage = decodeReceivedMessageText(sentMessage.messageText);
                            string decodedHtmlTextMessage = decodeReceivedMessageText(sentMessage.messageText);
                            ChatSessionChatMessageReceivedHtml?.Invoke(this, new ConversationMessageReceivedEventArgs(webUserName, conversationId, decodedHtmlTextMessage));
                            ChatSessionChatMessageReceivedPlainText?.Invoke(this, new ConversationMessageReceivedEventArgs(webUserName, conversationId, decodedPlainTextMessage));
                        }
                        sentMessages.Remove(sentMessage);
                    }
                }
            }
        }      

        private string decodeReceivedMessageText(string messageText)
        {   
            messageText = messageText.Replace("data:text/plain;charset=utf-8,", string.Empty);
            messageText = messageText.Replace("data:text/html;charset=utf-8,", string.Empty);
            if (messageText.Length >= 6)
            {
                if (messageText.Substring(messageText.Length - 6) == "%0d%0a")
                    messageText = messageText.Remove(messageText.Length - 6);
            }
            return HttpUtility.UrlDecode(messageText, Encoding.UTF8);
        }

        private enum SentMessageType { Status, Chat };

        private class SentMessage
        {
            public string operationId;
            public string messageText;
            public SentMessageType type;

            public SentMessage(string messageText, string operationId, SentMessageType type)
            {
                this.messageText = messageText;
                this.operationId = operationId;
                this.type = type;
            }
        }
    }

    public class OutgoingConversationInvitationEventArgs : EventArgs
    {
        public string invitedParticipantUri;
        public string conversationId;
        public OutgoingConversationInvitationEventArgs(string invitedParticipantUri, string conversationId = null)
        {
            this.invitedParticipantUri = invitedParticipantUri;
            if (conversationId != null)
                this.conversationId = conversationId;
        }
    }

    public class MessageHistory
    {
        public string queueName;
        public string conversationGuidId;
        public string threadId;
        public List<Message> messages;

        public MessageHistory()
        {
            messages = new List<Message>();
        }
    }

    public class Message
    {
        public string from;        
        public string timeStamp;
        public string messageText;
    }

    public class ConversationEndedEventArgs : EventArgs
    {
        //send everything needed to email out a transcript
        public string conversationId;
        public string webUserName;
        public string webUserEmail;
        public MessageHistory messageHistory;
        //public string agentUri;

        public ConversationEndedEventArgs(string conversationGuidId, string webUserName, string webUserEmail, MessageHistory messageHistory)
        {
            this.conversationId = conversationGuidId;
            this.webUserName = webUserName;
            this.webUserEmail = webUserEmail;
            this.messageHistory = messageHistory;
            //this.agentUri = agentUri;
        }
    }

    public class ChatSessionParticipantEventArgs : EventArgs
    {
        public string participantSipUri;
        public string originallyInvitedParticipantSipUri;
        public string conversationId;
        public ChatSessionState chatSessionState;

        public ChatSessionParticipantEventArgs(string participantSipUri, string originallyInvitedParticipantSipUri, string conversationId, ChatSessionState chatSessionState)
        {
            this.participantSipUri = participantSipUri;
            this.conversationId = conversationId;
            this.originallyInvitedParticipantSipUri = originallyInvitedParticipantSipUri;
            this.chatSessionState = chatSessionState;
        }
    }

    public class ConversationMessageReceivedEventArgs : EventArgs
    {
        public string fromDisplayName;
        public string conversationId;
        public string messageText;

        public ConversationMessageReceivedEventArgs(string fromDisplayName, string conversationId, string messageText)
        {
            this.fromDisplayName = fromDisplayName;
            this.conversationId = conversationId;
            this.messageText = messageText;
        }
    }

    public class ConversationStatusMessageReceivedEventArgs : EventArgs
    {
        public string conversationId;
        public string statusMessage;

        public ConversationStatusMessageReceivedEventArgs(string conversationId, string statusMessage)
        {
            this.conversationId = conversationId;
            this.statusMessage = statusMessage;
        }
    }

    public class WebUserLeftConversationEventArgs : EventArgs
    {
        public string conversationId;

        public WebUserLeftConversationEventArgs(string conversationId)
        {
            this.conversationId = conversationId;
        }
    }
}
