using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using log4net;

namespace KDembeck.UcwaWebApiClient.Utilities
{
    public interface IHttpUtility
    {   
        HttpClient httpClient { get; set; }
        AuthenticationResult authenticationResult { get; set; }    
        string baseUrl { get; set; } 
        string getBaseUri(string Uri);
        Task<string> httpGetJson(string getUrl);
        Task<Stream> httpGetImageJpeg(string getUrl);
        Task<string> httpDelete(string deleteUrl);
        Task<string> httpPutJson(string putUrl, string JsonString);
        Task<string> httpPostJson(string postUrl, string JsonString);
        Task<string> httpPostJson(string postUrl);
        Task<string> httpPostPlainText(string postUrl, string PlainText);
        Task<string> httpPostTextHtml(string postUrl, string HtmlText);
    }
}
