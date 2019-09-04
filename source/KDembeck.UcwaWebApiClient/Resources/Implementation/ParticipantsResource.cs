using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class ParticipantsResource :ResourceBase, IParticipantsResource
    {
        public ParticipantsLinks _links { get; set; }

        private void initializeProperties()
        {   
            _links = new ParticipantsLinks();
        }

        public ParticipantsResource()
        {
            initializeProperties();
        }

        public ParticipantsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }
        public new async Task<IParticipantsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IParticipantsResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }
    }
}
