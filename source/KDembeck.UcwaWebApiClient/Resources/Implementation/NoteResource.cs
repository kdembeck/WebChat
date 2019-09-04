using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class NoteResource : ResourceBase, INoteResource
    {
        public string message { get; set; }
        public NoteType? type { get; set; }
        public NoteLinks _links { get; set; }

        public NoteResource()
        {
            initializeProperties();
        }

        public NoteResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            message = null;
            type = null;
            _links = new NoteLinks();
        }

        //Get
        public async Task<INoteResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<INoteResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        //Post
        public async Task setNote(string message)
        {
            if (httpUtility != null && _links.self != null)
            {
                string noteJson = JsonConvert.SerializeObject(new
                {
                    message = message
                });
                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href + "?message=" + message, noteJson);
            }
        }
    }
}
