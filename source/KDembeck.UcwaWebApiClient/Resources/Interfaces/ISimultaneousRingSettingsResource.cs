using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ISimultaneousRingSettingsResource : IResourceBase
    {
        int ringDelay { get; set; }
        string target { get; set; }
        SimultaneousRingSettingsLinks _links { get; set; }
        Task<ISimultaneousRingSettingsResource> Get();
        new Task<ISimultaneousRingSettingsResource> Get(string resourceUrl);
        Task<IContactResource> getContact();
        Task simultaneousRingToContact(string targetUri);
        Task simultaneousRingToDelegates(int ringDelay);
        Task simultaneousRingToDelegates();
        Task simultaneousRingToTeam(int ringDelay);
        Task simultaneousRingToTeam();
    }

    public class SimultaneousRingSettingsLinks
    {
        public Link self;
        public Link contact;
        public Link simultaneousRingToContact;
        public Link simultaneousRingToDelegates;
        public Link simultaneousRingToTeam;
    }
}
