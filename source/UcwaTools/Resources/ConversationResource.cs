using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class ConversationResource
    {
        public string ResourceString { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
        
        public List<string> activeModalities;
        public string audienceMessaging;
        public string audienceMute;
        public string created;
        public string expirationTime;
        public string importance;
        public string participantCount;
        public string readLocally;
        public string recording;
        public string state;
        public string subject;
        public string threadId;
        public ConversationLinks _links;        

        public ConversationResource()
        {
            _links = new ConversationLinks();
            activeModalities = new List<string>();            
        }

        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";
            try
            {
                ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + resourceUri);
                FillResourceValues(ResourceString);
            }
            catch (Exception ex) { }
            return ResourceString;
        }

        public void FillResourceValues(string resourceString)
        {
            JsonConvert.PopulateObject(resourceString, this);
        }

        public override string ToString()
        {
            if (ResourceString != null)
                return ResourceString;
            else
                return "";
        }
    }

    internal struct ConversationLinks
    {
        public Link self;
        public Link addParticipant;
        public Link applicationSharing;
        public Link attendees;
        public Link audioVideo;
        public Link dataCollaboration;
        public Link disableAudienceMessaging;
        public Link disableAudienceMuteLock;
        public Link enableAudienceMessaging;
        public Link enableAudienceMuteLock;
        public Link leaders;
        public Link lobby;
        public Link localParticipant;
        public Link messaging;
        public Link onlineMeeting;
        public Link participants;
        public Link phoneAudio;
        public Link userAcknowledged;
    }
}
