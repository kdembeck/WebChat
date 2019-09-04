using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IParticipantAudioResource : IResourceBase
    {
        MediaDirectionType? audioDirection { get; set; }
        bool? audioMuted { get; set; }
        string audioSourceId { get; set; }
        ParticipantAudioLinks _links { get; set; }
        new Task<IParticipantAudioResource> Get(string resourceUrl);
        Task<IParticipantAudioResource> Get();
        Task muteAudio();
        Task<IParticipantResource> getParticipant();
        Task unmuteAudio();
    }

    public class ParticipantAudioLinks
    {
        public Link self;
        public Link muteAudio;
        public Link participant;
        public Link unmuteAudio;
    }
}
