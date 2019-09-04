using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcwaTools
{
    internal class OnlineMeetingInvitation
    {
        public string direction;
        public string importance;
        public string threadId;
        public string state;
        public string operationId;
        public string subject;
        public string onlineMeetingUri;
        public OnlineMeetingInvitationLinks _links;
    }

    internal struct OnlineMeetingInvitationLinks
    {
        public Link self;
        public FromLink from;
        public Link conversation;   
    }

    internal struct FromLink
    {
        public string href;
        public string title;
    }
}
