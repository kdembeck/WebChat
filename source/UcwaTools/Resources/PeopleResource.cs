using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class PeopleResource : IResource
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string ResourceString { get; set; }        
        public PeopleLinks _links;

        public PeopleResource()
        {
            _links = new PeopleLinks();
        }

        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";
            try
            {
                ResourceString = await httpHelper.HttpGetAction(resourceUri);
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

                if (meResourceObject._links != null)
                {
                    if (meResourceObject._links.myContacts != null)
                        _links.myContacts.href = meResourceObject._links.myContacts.href;
                    if (meResourceObject._links.myContactsAndGroupsSubscription != null)
                        _links.myContactsAndGroupsSubscription.href = meResourceObject._links.myContactsAndGroupsSubscription.href;
                    if (meResourceObject._links.myGroupMemberships != null)
                        _links.myGroupMemberships.href = meResourceObject._links.myGroupMemberships.href;
                    if (meResourceObject._links.myGroups != null)
                        _links.myGroups.href = meResourceObject._links.myGroups.href;
                    if (meResourceObject._links.myPrivacyRelationships != null)
                        _links.myPrivacyRelationships.href = meResourceObject._links.myPrivacyRelationships.href;
                    if (meResourceObject._links.presenceSubscriptionMemberships != null)
                        _links.presenceSubscriptionMemberships.href = meResourceObject._links.presenceSubscriptionMemberships.href;
                    if (meResourceObject._links.presenceSubscriptions != null)
                        _links.presenceSubscriptions.href = meResourceObject._links.presenceSubscriptions.href;
                    if (meResourceObject._links.search != null)
                        _links.search.href = meResourceObject._links.search.href;
                    if (meResourceObject._links.self != null)
                        _links.self.href = meResourceObject._links.self.href;
                    if (meResourceObject._links.subscribedContacts != null)
                        _links.subscribedContacts.href = meResourceObject._links.subscribedContacts.href;
                }
            }
            catch (Exception ex) { }
        }
    }

    public class PeopleLinks
    {
        public Link self;
        public Link myContactsAndGroupsSubscription;
        public Link myContacts;
        public Link myGroupMemberships;
        public Link myGroups;
        public Link myPrivacyRelationships;
        public Link presenceSubscriptionMemberships;
        public Link presenceSubscriptions;
        public Link search;
        public Link subscribedContacts;
    }
}
