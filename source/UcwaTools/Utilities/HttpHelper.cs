using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Clients.ActiveDirectory;


namespace UcwaTools.Utilities
{
    public class HttpHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HttpClient HttpClient;
        public AuthenticationResult AuthenticationResult;
        public string ApplicationRootUri;

        public HttpHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            HttpClient = new HttpClient();
        }

        public static String GetBaseUri(string Uri)
        {
            string _baseUri = "";
            try
            {
                _baseUri = new Uri(Uri).Scheme + "://" + new Uri(Uri).Host;
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Error("Error occurred in GetBaseUri. Error message: " + exceptionMessage);
            }

            return _baseUri;
        }

        public async Task<string> HttpGetAction(string getUrl)
        {
            string getResult = "";
            try
            {
                HttpClient.DefaultRequestHeaders.Clear();
                if (AuthenticationResult != null)
                {
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationResult.AccessToken);
                }
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpResponseMessage = await HttpClient.GetAsync(getUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    getResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    log.Debug("Sucess. HttpGetAction " + getUrl);
                }
                else
                {
                    try
                    {
                        getResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    }
                    catch { }
                    log.Info("Http status code " + httpResponseMessage.StatusCode + " received performing GET on url: " + getUrl + ". Response content: " + getResult);
                }
            }
            catch (TaskCanceledException ex)
            {
                log.Info("Http GET async TaskCanceledException occurred on url: " + getUrl + ". Exception message: " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Warn("Http GET async HttpRequestException occurred on url: " + getUrl + ". Exception message: " + exceptionMessage);
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Error("Error occurred in HttpGetAction. Error message: " + exceptionMessage);
            }
            return getResult;
        }

        public async Task<string> HttpPostAction(string postUrl, string JsonString)
        {
            string postResult = "";
            try
            {
                HttpClient.DefaultRequestHeaders.Clear();
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationResult.AccessToken);
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpResponseMessage = await HttpClient.PostAsync(postUrl, new StringContent(JsonString, Encoding.UTF8, "application/json"));

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    log.Debug("Sucess. HttpPostAction " + postUrl);
                }
                else
                {
                    try
                    {
                        postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    }
                    catch { }
                    log.Warn("Http status code " + httpResponseMessage.StatusCode + " received performing POST on url: " + postUrl + " with JSON content: " + JsonString + ". HTTP response content: " + postResult);
                }
            }
            catch (TaskCanceledException ex)
            {
                log.Info("TaskCanceledException occurred in HttpPostAction on url: " + postUrl + ". Exception message: " + ex.Message);                
            }
            catch (HttpRequestException ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;

                log.Warn(
                    "HttpRequestException occurred in HttpPostAction.\n" +
                    "    Exception message: " + exceptionMessage + "\n" +
                    "    url: " + postUrl + "\n" + 
                    "    jsonPostData: " + JsonString                    
                    );
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;                
                log.Error("Error occurred in HttpPostAction. Error message: " + exceptionMessage);
            }
            return postResult;
        }

        public async Task<string> HttpPostActionPlainText(string postUrl, string PlainText)
        {
            string postResult = "";
            try
            {
                HttpClient.DefaultRequestHeaders.Clear();
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationResult.AccessToken);
                HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                var httpResponseMessage = await HttpClient.PostAsync(postUrl, new StringContent(PlainText, Encoding.UTF8, "text/plain"));

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    log.Debug("Sucess. HttpPostActionPlainText " + postUrl);
                }
                else
                {
                    postResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    string requestBody = httpResponseMessage.RequestMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (TaskCanceledException ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Info("TaskCanceledException occurred in HttpPostActionPlainText on url: " + postUrl + ". Exception message: " + exceptionMessage);                
            }
            catch (HttpRequestException ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;
                log.Warn("HttpRequestException occurred in HttpPostActionPlainText on url: " + postUrl + ". Exception message: " + exceptionMessage);
            }
            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;
                if (ex.InnerException != null)
                    exceptionMessage += " Inner exception message: " + ex.InnerException.Message;                
                log.Error("Error occurred in HttpPostActionPlainText. Error message: " + exceptionMessage);
            }
            return postResult;
        }
    }
}
