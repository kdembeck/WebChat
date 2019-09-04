using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcwaTools
{
    internal class UcwaMessagingSessionManager
    {
        private Dictionary<string, UcwaMessagingSession> _ucwaMessagingSessions;

        public UcwaMessagingSessionManager()
        {
            _ucwaMessagingSessions = new Dictionary<string, UcwaMessagingSession>();
        }

        public void addNewMessagingSession() { }
        
        public void endMessagingSessionWithSessionId() { }

        public void findMessagingSessionIdForRecipientUri() { }

        public void endAllMessagingSessions() { }


    }
}
