using System.Collections.Generic;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class AttendeesResource : ResourceBase, IAttendeesResource
    {
        public AttendeesLinks _links { get; set; }

        public AttendeesResource()
        {
            initializeProperties();
        }

        public AttendeesResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new AttendeesLinks();
        }

        public new async Task<IAttendeesResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<IAttendeesResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task<List<IParticipantResource>> getParticipants()
        {
            if (httpUtility != null && _links.participant.Count > 0)
            {
                List<IParticipantResource> participantsList = new List<IParticipantResource>();
                foreach (Link participantLink in _links.participant)
                {
                    IParticipantResource newParticipantResource = new ParticipantResource(httpUtility);
                    await newParticipantResource.Get(httpUtility.baseUrl + participantLink.href);
                    participantsList.Add(newParticipantResource);
                }
                return participantsList;
            }
            else return null;
        }
    }
}
