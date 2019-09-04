using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class OnlineMeetingResource : ResourceBase, IOnlineMeetingResource
    {
        public string accessLevel { get; set; }
        public string attendees { get; set; }
        public string automaticLeaderAssignment { get; set; }
        public string conferenceId { get; set; }
        public string description { get; set; }
        public string disclaimerBody { get; set; }
        public string disclaimerTitle { get; set; }
        public string entryExitAnnouncement { get; set; }
        public string expirationTime { get; set; }
        public string hostingNetwork { get; set; }
        public string joinUrl { get; set; }
        public string largeMeeting { get; set; }
        public List<string> leaders { get; set; }
        public string lobbyBypassForPhoneUsers { get; set; }
        public string onlineMeetingId { get; set; }
        public string onlineMeetingRel { get; set; }
        public string onlineMeetingUri { get; set; }
        public string organizerName { get; set; }
        public string organizerUri { get; set; }
        public string phoneUserAdmission { get; set; }
        public string subject { get; set; }
        public OnlineMeetingLinks _links { get; set; }
        public OnlineMeetingEmbedded _embedded { get; set; }
        public List<OnlineMeetingExtensionsResource> onlineMeetingExtension { get { return _embedded.onlineMeetingExtension; } }

        private void initializeProperties()
        {
            accessLevel = null;
            attendees = null;
            automaticLeaderAssignment = null;
            conferenceId = null;
            description = null;
            disclaimerBody = null;
            disclaimerTitle = null;
            entryExitAnnouncement = null;
            expirationTime = null;
            hostingNetwork = null;
            joinUrl = null;
            largeMeeting = null;
            leaders = new List<string>();
            lobbyBypassForPhoneUsers = null;
            onlineMeetingId = null;
            onlineMeetingRel = null;
            onlineMeetingUri = null;
            organizerName = null;
            organizerUri = null;
            phoneUserAdmission = null;
            subject = null;
            _links = new OnlineMeetingLinks();
            _embedded = new OnlineMeetingEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.onlineMeetingExtension != null && _embedded.onlineMeetingExtension.Count > 0)
                {
                    foreach (OnlineMeetingExtensionsResource onlineMeetingExtension in _embedded.onlineMeetingExtension)
                    {
                        onlineMeetingExtension.httpUtility = httpUtility;
                    }
                }
            }
        }

        public OnlineMeetingResource()
        {
            initializeProperties();
        }

        public OnlineMeetingResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<IOnlineMeetingResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IOnlineMeetingResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<IConversationResource> conversation()
        {
            if (httpUtility != null && _links.conversation != null)
            {
                IConversationResource conversationResource = new ConversationResource(httpUtility);
                await conversationResource.Get(httpUtility.baseUrl + _links.conversation.href);
                return conversationResource;
            }
            else return null;
        }

        public async Task<IContactResource> organizer()
        {
            if (httpUtility != null && _links.organizer != null)
            {
                IContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.organizer.href);
                return contactResource;
            }
            else return null;
        }

        public async Task<IPhoneDialInformationResource> phoneDianInInformation()
        {
            if (httpUtility != null && _links.phoneDialInInformation != null)
            {
                IPhoneDialInformationResource phoneDialInInformationResource = new PhoneDialInformationResource(httpUtility);
                await phoneDialInInformationResource.Get(httpUtility.baseUrl + _links.phoneDialInInformation.href);
                return phoneDialInInformationResource;
            }
            else return null;
        }

        public async Task<IOnlineMeetingExtensionsResource> onlineMeetingExtensions()
        {
            if (httpUtility != null && _links.onlineMeetingExtensions != null)
            {
                IOnlineMeetingExtensionsResource onlineMeetingExtensionsResource = new OnlineMeetingExtensionsResource(httpUtility);
                await onlineMeetingExtensionsResource.Get(httpUtility.baseUrl + _links.onlineMeetingExtensions.href);
                return onlineMeetingExtensionsResource;
            }
            else return null;
        }
    }
}
