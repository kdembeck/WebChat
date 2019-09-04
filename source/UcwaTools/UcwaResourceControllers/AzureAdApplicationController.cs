using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class AzureAdApplicationController
    {        
        public static async Task<string> Login(AzureAdApplicationInformation ApplicationInformation, HttpHelper httpHelper)
        { 
            string returnApplicationResourceUri = null;
            try
            {   
                string autoDiscoverResultString = "";
                while (autoDiscoverResultString == "")
                    autoDiscoverResultString = await httpHelper.HttpGetAction("https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root");
                dynamic autoDiscoverResultObject = JObject.Parse(autoDiscoverResultString);

                if (autoDiscoverResultObject._links != null)
                {
                    if (autoDiscoverResultObject._links.user != null)
                    {
                        string rootOauthUserUri = autoDiscoverResultObject._links.user.href;

                        AuthenticationContext authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/" + ApplicationInformation.Tenant);
                        UserCredential ApplicationCredentials = new UserPasswordCredential(ApplicationInformation.UserUpn, ApplicationInformation.Password);

                        httpHelper.AuthenticationResult = await authenticationContext.AcquireTokenAsync(HttpHelper.GetBaseUri(rootOauthUserUri), ApplicationInformation.ClientId, ApplicationCredentials);
                        //authenticationResult = await authenticationContext.AcquireTokenAsync(HttpHelper.GetBaseUri(rootOauthUserUri), ApplicationInformation.ClientId, ApplicationCredentials);

                        autoDiscoverResultString = await httpHelper.HttpGetAction(rootOauthUserUri);
                        autoDiscoverResultObject = JObject.Parse(autoDiscoverResultString);

                        if (autoDiscoverResultObject._links != null)
                        {
                            while (autoDiscoverResultObject._links.redirect != null)
                            {
                                string redirectUri = autoDiscoverResultObject._links.redirect.href;
                                autoDiscoverResultString = await httpHelper.HttpGetAction(redirectUri);
                                autoDiscoverResultObject = JObject.Parse(autoDiscoverResultString);

                                if (autoDiscoverResultObject._links != null)
                                {
                                    if (autoDiscoverResultObject._links.user != null)
                                    {
                                        rootOauthUserUri = autoDiscoverResultObject._links.user.href;
                                        break;
                                    }
                                }
                                else { break; }
                            }
                        }

                        httpHelper.AuthenticationResult = await authenticationContext.AcquireTokenAsync(HttpHelper.GetBaseUri(rootOauthUserUri), ApplicationInformation.ClientId, ApplicationCredentials);

                        string rootOauthUserResultString = await httpHelper.HttpGetAction(rootOauthUserUri);
                        dynamic rootOauthUserResultObject = JObject.Parse(rootOauthUserResultString);

                        if (rootOauthUserResultObject._links != null)
                        {
                            if (rootOauthUserResultObject._links.applications != null)
                            {
                                returnApplicationResourceUri = rootOauthUserResultObject._links.applications.href;

                                httpHelper.ApplicationRootUri = HttpHelper.GetBaseUri(returnApplicationResourceUri);
                                httpHelper.AuthenticationResult = await authenticationContext.AcquireTokenAsync(httpHelper.ApplicationRootUri, ApplicationInformation.ClientId, ApplicationCredentials);                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return returnApplicationResourceUri;
        }
    }

    public class AzureAdApplicationInformation
    {
        public string UserUpn;
        public string Password;
        public string ClientId;
        public string ResourceId;
        public string RedirectUri;
        public string Tenant;
    }
}
