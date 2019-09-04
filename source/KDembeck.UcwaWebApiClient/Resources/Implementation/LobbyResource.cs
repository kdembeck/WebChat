using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class LobbyResource : ResourceBase, ILobbyResource
    {
        public LobbyLinks _links { get; set; }
        public LobbyEmbedded _embedded { get; set; }
        public List<ParticipantResource> participant { get { return _embedded.participant; } }

        private void initializeProperties()
        {
            _links = new LobbyLinks();
            _embedded = new LobbyEmbedded();
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.participant != null && _embedded.participant.Count > 0)
                {
                    foreach (ParticipantResource participant in _embedded.participant)
                    {
                        participant.httpUtility = httpUtility;
                    }
                }
            }
        }

        public LobbyResource()
        {
            initializeProperties();
        }

        public LobbyResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        public new async Task<ILobbyResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task<ILobbyResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }
    }
}
