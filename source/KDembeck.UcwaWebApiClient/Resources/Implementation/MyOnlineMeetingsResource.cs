using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyOnlineMeetingsResource : ResourceBase, IMyOnlineMeetingsResource
    {
        public MyOnlineMeetingsLinks _links { get; set; }
        public MyOnlineMeetingsEmbedded _embedded { get; set; }
        public List<MyAssignedOnlineMeetingResource> myAssignedOnlineMeetings { get { return _embedded.myAssignedOnlineMeeting; } }
        public List<MyOnlineMeetingResource> myOnlineMeeting { get { return _embedded.myOnlineMeeting; } }

        public MyOnlineMeetingsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
        }

        private void initializeProperties()
        {
            _links = new MyOnlineMeetingsLinks();
            _embedded = new MyOnlineMeetingsEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.myAssignedOnlineMeeting != null && _embedded.myAssignedOnlineMeeting.Count > 0)
                {
                    foreach (MyAssignedOnlineMeetingResource myAssignedOnlineMeeting in _embedded.myAssignedOnlineMeeting)
                    {
                        myAssignedOnlineMeeting.httpUtility = httpUtility;
                        myAssignedOnlineMeeting.initializeResources();
                    }
                }

                if (_embedded.myOnlineMeeting != null && _embedded.myOnlineMeeting.Count > 0)
                {
                    foreach (MyOnlineMeetingResource myOnlineMeeting in _embedded.myOnlineMeeting)
                    {
                        myOnlineMeeting.httpUtility = httpUtility;
                        myOnlineMeeting.initializeResources();
                    }
                }
            }
        }

        public MyOnlineMeetingsResource()
        {
            initializeProperties();
        }

        public new async Task<IMyOnlineMeetingsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            else
            {
                //raise an error
            }
            return this;
        }

        public async Task<IMyOnlineMeetingsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            else
            {
                //raise an error
            }
            return this;
        }
    }
}
