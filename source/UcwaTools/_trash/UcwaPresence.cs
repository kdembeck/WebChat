using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace UcwaTools
{
    internal class UcwaPresence
    {
        public static async Task<string> GetPresenceForContact(HttpClient httpClient, string ApplicationsResource, string ApplicationRootUri, AuthenticationResult ucwaAuthenticationResult, string UserEmail)
        {
            string presenceStatus="";
            try
            {
                ////get a contact resource for the user we're searching for
                //string contactResource = await UcwaSearchUserContacts.GetContactResource(httpClient, UserEmail, ApplicationRootUri, ApplicationsResource, ucwaAuthenticationResult);

                ////get the presence query uri for this contact resource
                //dynamic contactResourceObject = JObject.Parse(contactResource);
                //string contactPresenceQueryUri = ApplicationRootUri + contactResourceObject._links.contactPresence.href;

                ////send a get to the presence query uri to get the contact's presence status
                //string resultString = await HttpHelper.HttpGetAction(httpClient, contactPresenceQueryUri, ucwaAuthenticationResult);
                //dynamic resultObject = JObject.Parse(resultString);
                //presenceStatus = resultObject.availability;
            }
            catch(Exception ex)
            { }
            return presenceStatus;
        }

        public static async Task<string> StartPresenceSubscription(HttpClient httpClient, string applicationResource, string applicationRootUri, AuthenticationResult ucwaAuthenticationResult, int duration, string[] sipUris)
        {
            string presenceSubscriptionResource = "";
            try
            {
                //dynamic jsonObject = JObject.Parse(applicationResource);
                //string presenceSubscriptionUri = applicationRootUri + jsonObject._embedded.people._links.presenceSubscriptions.href;

                //UcwaPresenceSubscriptionObject presenceSubscriptionObject = new UcwaPresenceSubscriptionObject();
                //presenceSubscriptionObject.duration = duration;
                //presenceSubscriptionObject.Uris = sipUris;

                //string jsonPostData = JsonConvert.SerializeObject(presenceSubscriptionObject);
                //presenceSubscriptionResource = await HttpHelper.HttpPostAction(httpClient, presenceSubscriptionUri, jsonPostData, ucwaAuthenticationResult);
            }
            catch (Exception ex)
            { }

            return presenceSubscriptionResource;
        }


    }
    internal class UcwaPresenceSubscriptionObject
    {
        public int duration { get; set; }
        public string[] Uris { get; set; }
    }
}
