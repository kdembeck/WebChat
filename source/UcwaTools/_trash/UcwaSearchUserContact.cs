using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace UcwaTools
{
    internal class UcwaSearchUserContacts
    {
        public static async Task<string> GetContactResource(HttpClient httpClient, string ContactEmail, string ApplicationRootUri, string ApplicationsResource, AuthenticationResult ucwaAuthenticationResult)
        {   
            string contactResourceString = "";
            try
            {
                ////get the contact search uri
                //dynamic applicationsResourceObject = JObject.Parse(ApplicationsResource);
                //string contactSearchUri = ApplicationRootUri + applicationsResourceObject._embedded.people._links.search.href;

                ////perform search for contact with email=
                //contactSearchUri += "?query=" + ContactEmail;                
                //string resultString = await HttpHelper.HttpGetAction(httpClient, contactSearchUri, ucwaAuthenticationResult);
                //if (resultString != "")
                //{
                //    dynamic resultObject = JObject.Parse(resultString);
                //    var ContactInfo = resultObject._embedded.contact[0];
                //    contactResourceString = ContactInfo.ToString();
                //}
            }
            catch(Exception ex)
            { }
            return contactResourceString;
        }
    }
}
