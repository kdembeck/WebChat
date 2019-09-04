using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ILobbyResource : IResourceBase
    {
        LobbyLinks _links { get; set; }
        LobbyEmbedded _embedded { get; set; }
        List<ParticipantResource> participant { get; }
        new Task<ILobbyResource> Get(string resourceUrl);
        Task<ILobbyResource> Get();
    }

    public class LobbyLinks
    {
        public Link self;
    }

    public class LobbyEmbedded
    {
        public List<ParticipantResource> participant;
    }
}
