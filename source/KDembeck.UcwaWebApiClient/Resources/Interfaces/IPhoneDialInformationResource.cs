using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IPhoneDialInformationResource : IResourceBase
    {
        PhoneDialInformationLinks _links { get; set; }
        PhoneDialInformationEmbedded _embedded { get; set; }
        string conferenceId { get; set; }
        string defaultRegion { get; set; }
        string externalDirectoryUri { get; set; }
        string internalDirectoryUri { get; set; }
        string isAudioConferenceProviderEnabled { get; set; }
        string participantPassCode { get; set; }
        string tollFreeNumbers { get; set; }
        string tollNumber { get; set; }
        List<DialInRegionResource> dialInRegions { get; }
        new Task<IPhoneDialInformationResource> Get(string resourceUrl);
        Task<IPhoneDialInformationResource> Get();
    }

    public class PhoneDialInformationLinks
    {
        public Link self;
    }

    public class PhoneDialInformationEmbedded
    {
        public List<DialInRegionResource> dialInRegion;

        public PhoneDialInformationEmbedded()
        {
            dialInRegion = new List<DialInRegionResource>();
        }
    }
}
