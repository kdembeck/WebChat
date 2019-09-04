using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ContactResource : ResourceBase, IContactResource
    {
        public string company { get; set; }
        public string department { get; set; }
        public List<string> emailAddresses { get; set; }
        public string homePhoneNumber { get; set; }
        public string sourceNetworkIconUrl { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string name { get; set; }
        public string office { get; set; }
        public string otherPhoneNumber { get; set; }
        public string sourceNetwork { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public string workPhoneNumber { get; set; }
        public ContactLinks _links { get; set; }

        public ContactResource()
        {
            initializePropterties();
        }

        public ContactResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializePropterties();
        }

        private void initializePropterties()
        {
            company = null;
            department = null;
            emailAddresses = new List<string>();
            homePhoneNumber = null;
            sourceNetworkIconUrl = null;
            mobilePhoneNumber = null;
            name = null;
            office = null;
            otherPhoneNumber = null;
            sourceNetwork = null;
            title = null;
            type = null;
            uri = null;
            workPhoneNumber = null;            
        }
        
        public async Task<IContactResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializePropterties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public new async Task<IContactResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializePropterties();
                await base.Get(resourceUrl);                
            }
            return this;
        }

        public async Task<ILocationResource> getContactLocation()
        {
            if (httpUtility != null && _links.contactLocation != null)
            {
                ILocationResource contactLocationResource = new LocationResource(httpUtility);
                await contactLocationResource.Get(httpUtility.baseUrl + _links.contactLocation.href);
                return contactLocationResource;
            }
            else
                return null;
        }

        public async Task<INoteResource> getContactNote()
        {
            if (httpUtility != null && _links.contactNote != null)
            {
                INoteResource contactNoteResource = new NoteResource(httpUtility);
                await contactNoteResource.Get(httpUtility.baseUrl + _links.contactNote.href);
                return contactNoteResource;
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
                IContactPresenceResource contactPresenceResource = new ContactPresenceResource(httpUtility);
                await contactPresenceResource.Get(httpUtility.baseUrl + _links.contactPresence.href);
                return contactPresenceResource;
            }
            else
                return null;
        }

        public async Task<IContactPrivacyRelationshipResource> getContactPrivacyRelationship()
        {
            if (httpUtility != null && _links.contactPrivacyRelationship != null)
            {
                IContactPrivacyRelationshipResource contactPrivacyRelationshipResource = new ContactPrivacyRelationshipResource(httpUtility);
                await contactPrivacyRelationshipResource.Get(httpUtility.baseUrl + _links.contactPrivacyRelationship.href);
                return contactPrivacyRelationshipResource;
            }
            else
                return null;
        }

        public async Task<IContactSupportedModalitiesResource> getContactSupportedModalities()
        {
            if (httpUtility != null && _links.contactSupportedModalities != null)
            {
                IContactSupportedModalitiesResource contactSupportedModalities = new ContactSupportedModalitiesResource(httpUtility);
                await contactSupportedModalities.Get(httpUtility.baseUrl + _links.contactSupportedModalities.href);
                return contactSupportedModalities;
            }
            else
                return null;
        }
    }
}
