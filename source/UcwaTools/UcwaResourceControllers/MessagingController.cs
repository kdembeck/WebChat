using System;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal class MessagingController
    {
        private HttpHelper httpHelper;

        public MessagingController(HttpHelper httpHelper)
        {
            this.httpHelper = new HttpHelper();
            this.httpHelper.ApplicationRootUri = httpHelper.ApplicationRootUri;
            this.httpHelper.AuthenticationResult = httpHelper.AuthenticationResult;
        }

        public async void SendMessage(string messageText, string sendMessageUri)
        {
            string httpResult = "";
            try
            {   
                httpResult = await httpHelper.HttpPostActionPlainText(httpHelper.ApplicationRootUri + sendMessageUri, messageText);
            }
            catch (Exception ex) { throw ex; }            
        }
    }
}
