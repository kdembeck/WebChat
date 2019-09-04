using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using log4net;

namespace KDembeck.UcwaWebApiClient.Utilities
{
    public class HttpUtility : IHttpUtility
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AuthenticationResult authenticationResult { get; set; }
        public string baseUrl { get; set; }
        public HttpClient httpClient { get; set; }

        public HttpUtility(HttpClient HttpClient)
        {
            httpClient = HttpClient;                      
        }

        public string getBaseUri(string Uri)
        {
            string _baseUri = "";
            _baseUri = new Uri(Uri).Scheme + "://" + new Uri(Uri).Host;            
            return _baseUri;
        }

        public async Task<string> httpGetJson(string getUrl)
        {
            string getResult = "";
           
            httpClient.DefaultRequestHeaders.Clear();
            if (authenticationResult != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            }
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await httpClient.GetAsync(getUrl);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                getResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            else
            {
                string failureResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
                
            return getResult;
        }

        public async Task<Stream> httpGetImageJpeg(string getUrl)
        {
            Stream getResult = null;

            httpClient.DefaultRequestHeaders.Clear();
            if (authenticationResult != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            }
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));
            var httpResponseMessage = await httpClient.GetAsync(getUrl);
            
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                getResult = httpResponseMessage.Content.ReadAsStreamAsync().Result;
            }
            else
            {
                string failureResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                //log
            }
            return getResult;
        }

        public async Task<string> httpDelete(string deleteUrl)
        {
            string getResult = "";

            httpClient.DefaultRequestHeaders.Clear();
            if (authenticationResult != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            }
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await httpClient.DeleteAsync(deleteUrl);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                getResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            else
            {
                string failureResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }

            return getResult;
        }

        public async Task<string> httpPutJson(string putUrl, string JsonString)
        {
            string putResult = "";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await httpClient.PutAsync(putUrl, new StringContent(JsonString, Encoding.UTF8, "application/json"));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                putResult = httpResponseMessage.Content.ReadAsStringAsync().Result;                
            }
            else
            {
                putResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            return putResult;
        }

        public async Task<string> httpPostJson(string postUrl, string JsonString)
        {
            string postResult = "";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await httpClient.PostAsync(postUrl, new StringContent(JsonString, Encoding.UTF8, "application/json"));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;                
            }
            else
            {                
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;   
                //should i raise an error and send the responseMessage.statusCode?              
            }           
            return postResult;
        }

        public async Task<string> httpPostPlainText(string postUrl, string PlainText)
        {
            string postResult = "";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var httpResponseMessage = await httpClient.PostAsync(postUrl, new StringContent(PlainText, Encoding.UTF8, "text/plain"));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                //log.Debug("Sucess. HttpPostActionPlainText " + postUrl);
            }
            else
            {
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                //httpResponseMessage.Content.Headers.
                //string requestBody = httpResponseMessage.RequestMessage.Content.ReadAsStringAsync().Result;
            }
            return postResult;
        }

        public async Task<string> httpPostTextHtml(string postUrl, string HtmlText)
        {
            string postResult = "";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));            
            var httpResponseMessage = await httpClient.PostAsync(postUrl, new StringContent(@HtmlText, Encoding.UTF8, "text/html"));

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;                
            }
            else
            {
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;               
            }
            return postResult;
        }

        public async Task<string> httpPostJson(string postUrl)
        {
            string postResult = "";

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await httpClient.PostAsync(postUrl, null);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                //log.Debug("Sucess. HttpPostAction " + postUrl);
            }
            else
            {
                postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            return postResult;
        }
    }
}
