using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ConversationLogRecipientResource : ResourceBase, IConversationLogRecipientResource
    {
        public string displayName { get; set; }
        public string sipUri { get; set; }
        public ConversationLogRecipientLinks _links { get; set; }
        //public IContactResource contact { get; set; }
        //public Stream contactPhoto { get; set; }
        //public IContactPresenceResource contactPresence { get; set; }

        public ConversationLogRecipientResource()
        {
            initializeProperties();
        }

        public ConversationLogRecipientResource(IHttpUtility HttpUtilty)
        {
            httpUtility = HttpUtilty;
            initializeProperties();
        }

        private void initializeProperties()
        {
            displayName = null;
            sipUri = null;
            _links = new ConversationLogRecipientLinks();
        }

        //public async Task initializeResources()
        //{
        //    contact = await getContact();
        //    contactPhoto = await getContactPhoto();
        //    contactPresence = await getContactPresence();
        //}

        public new async Task<IConversationLogRecipientResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                //await initializeResources();
            }
            return this;
        }

        public async Task<IConversationLogRecipientResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                //await initializeResources();
            }
            return this;
        }

        public async Task<IContactResource> getContact()
        {
            if (httpUtility != null && _links.contact != null)
            {
                ContactResource contactResource = new ContactResource(httpUtility);
                await contactResource.Get(httpUtility.baseUrl + _links.contact.href);
                return contactResource;
            }
            else
                return null;
        }

        public async Task<Stream> getContactPhoto()
        {
            if (httpUtility != null && _links.contactPhoto != null)
            {
                Stream contactPhotoStream = null;
                contactPhotoStream = await httpUtility.httpGetImageJpeg(httpUtility.baseUrl + _links.contactPhoto.href);
                return contactPhotoStream;
            }
            else
                return null;
        }

        public async Task<IContactPresenceResource> getContactPresence()
        {
            if (httpUtility != null && _links.contactPresence != null)
            {
                ContactPresenceResource contactPresenceResource = new ContactPresenceResource(httpUtility);
                await contactPresenceResource.Get(httpUtility.baseUrl + _links.contactPresence.href);
                return contactPresenceResource;
            }
            else
                return null;
        }
    }
}
