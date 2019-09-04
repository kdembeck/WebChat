using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class MeResource : IResource
    {
        public string ResourceString { get; set; }
        public string rel { get; set; }
        public string href { get; set; }
        public string company;
        public string department;
        public List<string> emailAddresses;
        public string endpointUri;
        public string homePhoneNumber;
        public string mobilePhoneNumber;
        public string name;
        public string officeLocation;
        public string otherPhoneNumber;
        public string title;
        public string uri;
        public string workPhoneNumber;        
        public MeLinks _links;

        public MeResource()
        {
            _links = new MeLinks();
            emailAddresses = new List<string>();
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
            ResourceString = resourceString;

            try
            {
                dynamic meResourceObject = JObject.Parse(ResourceString);

                rel = meResourceObject.rel;
                href = meResourceObject.href;

                company = meResourceObject.company;
                department = meResourceObject.department;
                foreach (string emailAddress in meResourceObject.emailAddresses)
                {
                    emailAddresses.Add(emailAddress);
                }
                endpointUri = meResourceObject.endpointUri;
                homePhoneNumber = meResourceObject.homePhoneNumber;
                mobilePhoneNumber = meResourceObject.mobilePhoneNumber;
                name = meResourceObject.name;
                officeLocation = meResourceObject.officeLocation;
                otherPhoneNumber = meResourceObject.otherPhoneNumber;
                title = meResourceObject.title;
                uri = meResourceObject.uri;
                workPhoneNumber = meResourceObject.workPhoneNumber;

                if (meResourceObject._links != null)
                {
                    if (meResourceObject._links.self != null)
                        _links.self.href = meResourceObject._links.self.href;
                    if (meResourceObject._links.callForwardingSettings != null)
                        _links.callForwardingSettings.href = meResourceObject._links.callForwardingSettings.href;
                    if (meResourceObject._links.location != null)
                        _links.location.href = meResourceObject._links.location.href;
                    if (meResourceObject._links.makeMeAvailable != null)
                        _links.makeMeAvailable.href = meResourceObject._links.makeMeAvailable.href;
                    if (meResourceObject._links.note != null)
                        _links.note.href = meResourceObject._links.note.href;
                    if (meResourceObject._links.phones != null)
                        _links.phones.href = meResourceObject._links.phones.href;
                    if (meResourceObject._links.photo != null)
                        _links.photo.href = meResourceObject._links.photo.href;
                    if (meResourceObject._links.presence != null)
                        _links.presence.href = meResourceObject._links.presence.href;
                    if (meResourceObject._links.reportMyActivity != null)
                        _links.reportMyActivity.href = meResourceObject._links.reportMyActivity.href;
                }
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            if (ResourceString != null)
                return ResourceString;
            else
                return "";
        }

    }
    public class MeLinks
    {
        public Link self;
        public Link callForwardingSettings;
        public Link location;
        public Link makeMeAvailable;
        public Link note;
        public Link phones;
        public Link photo;
        public Link presence;
        public Link reportMyActivity;
    }
}
