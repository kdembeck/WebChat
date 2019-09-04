//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace UcwaTools
//{
//    public class EventChannelEventHandlerConversationsManager
//    {
//        private ConversationController _conversationController;
//        private MessagingController _messagingController;
//        private Dictionary<string, string> _startMessagingTextsByOperatingId;
//        private List<MessagingInvitationResource> _messagingInvitations;
//        private Dictionary<string, ConversationResource> _conversationsByThreadId;

//        public EventChannelEventHandlerConversationsManager()
//        {
//            _conversationController = new ConversationController();
//            //_messagingController = new MessagingController();
//            _messagingInvitations = new List<MessagingInvitationResource>();
//            _startMessagingTextsByOperatingId = new Dictionary<string, string>();
//            _conversationsByThreadId = new Dictionary<string, ConversationResource>();
//        }

//        public void Handle_OutgoingInvitationStartedEvent(string eventObjectString)
//        {
//            try
//            {
//                //create a new messagingInvitation object and put it in our list 
//                //add a new startMessagingTextsByOperatingId item   

//                dynamic eventObject = JObject.Parse(eventObjectString);
//                if (eventObject._embedded != null)
//                {                       
//                    string messagingInvitationResourceString = JsonConvert.SerializeObject(eventObject._embedded.messagingInvitation);
//                    MessagingInvitationResource newMessagingInvitationResource = new MessagingInvitationResource();
//                    newMessagingInvitationResource.FillResourceValues(messagingInvitationResourceString);

//                }
//            }
//            catch (Exception ex){ throw ex; }
//        }

//        public void Handle_OutgoingInvitationCompletedEvent(string invitationObjectString)
//        {
//            try
//            {
//                //update the messaging invitation object we have in our list
//                //send message startMessagingTextsByOperatingId
//            }
//            catch (Exception ex) { throw ex; }
//        }

//        public string StartConversation()
//        {
//            string threadId = "";
//            try { }
//            catch { }
//            return threadId;
//        }

//        public void SendMessage(string threadId) { }
        
//    }
//}
