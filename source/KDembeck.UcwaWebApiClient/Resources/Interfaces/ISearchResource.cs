using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface ISearchResource : IResourceBase
    {
        bool? moreResultsAvailable { get; set; }
        SearchLinks _links { get; set; }
        SearchEmbedded _embedded { get; set; }
        List<ContactResource> contacts { get; }
        List<DistributionGroupResource> distributionGroups { get; }
        Task<ISearchResource> Get();
        new Task<ISearchResource> Get(string resourceUrl);
        Task<ISearchResource> search(string query);
        Task<ISearchResource> search(string query, int limit);
        Task<ISearchResource> search(string resourceUrl, string query);
        Task<ISearchResource> search(string resourceUrl, string query, int limit);
        void initializeResources();
    }

    public class SearchLinks
    {
        public Link self;
    }

    public class SearchEmbedded
    {
        public List<ContactResource> contact;
        public List<DistributionGroupResource> distributionGroup;

        public SearchEmbedded()
        {
            contact = new List<ContactResource>();
            distributionGroup = new List<DistributionGroupResource>();
        }
    }
}
