using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class SearchResource : ResourceBase, ISearchResource
    {
        public bool? moreResultsAvailable { get; set; }
        public SearchLinks _links { get; set; }
        public SearchEmbedded _embedded { get; set; }
        public List<ContactResource> contacts { get { return _embedded.contact; } }
        public List<DistributionGroupResource> distributionGroups { get { return _embedded.distributionGroup; } }

        public SearchResource()
        {
            initializeProperties();
        }

        public SearchResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            moreResultsAvailable = null;
            _links = new SearchLinks();
            _embedded = new SearchEmbedded();
        }

        public async Task<ISearchResource> Get()
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

        public new async Task<ISearchResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.contact != null && _embedded.contact.Count > 0)
                {
                    foreach (ContactResource contact in _embedded.contact)
                    {
                        contact.httpUtility = httpUtility;
                    }
                }
                if (_embedded.distributionGroup != null && _embedded.distributionGroup.Count > 0)
                {
                    foreach (DistributionGroupResource distributionGroup in _embedded.distributionGroup)
                    {
                        distributionGroup.httpUtility = httpUtility;
                    }
                }
            }
        }

        public async Task<ISearchResource> search(string query)
        {
            if (httpUtility != null && _links.self != null)
            {
                await base.Get(httpUtility.baseUrl + _links.self.href + "?query=" + query);
                initializeResources();
            }
            return this;
        }

        public async Task<ISearchResource> search(string query, int limit)
        {
            if (httpUtility != null && _links.self != null)
            {
                if (limit > 100)
                    limit = 100;
                else if (limit < 1)
                    limit = 1;

                await base.Get(httpUtility.baseUrl + _links.self.href + "?query=" + query + "&limit=" + limit.ToString());
                initializeResources();
            }
            return this;
        }

        public async Task<ISearchResource> search(string resourceUrl, string query)
        {
            if (httpUtility != null && _links.self != null)
            {
                await base.Get(resourceUrl + "?query=" + query);
                initializeResources();
            }
            return this;
        }

        public async Task<ISearchResource> search(string resourceUrl, string query, int limit)
        {
            if (httpUtility != null && _links.self != null)
            {
                if (limit > 100)
                    limit = 100;
                else if (limit < 1)
                    limit = 1;

                await base.Get(resourceUrl + "?query=" + query + "&limit=" + limit.ToString());
                initializeResources();
            }
            return this;
        }
    }
}
