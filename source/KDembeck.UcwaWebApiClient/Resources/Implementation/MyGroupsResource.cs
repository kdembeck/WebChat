using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class MyGroupsResource : ResourceBase, IMyGroupsResource
    {
        public MyGroupsLinks _links { get; set; }
        public MyGroupsEmbedded _embedded { get; set; }
        public List<DistributionGroupResource> distributionGroups { get { return _embedded.distributionGroup; } }
        public List<GroupResource> groups { get { return _embedded.group; } }
        public GroupResource defaultGroup { get { return _embedded.defaultGroup; } }
        public GroupResource delegatesGroup { get { return _embedded.delegatesGroup; } }
        public GroupResource delegatorsGroup { get { return _embedded.delegatorsGroup; } }
        public GroupResource myOrganizationGroup { get { return _embedded.myOrganizationGroup; } }
        public GroupResource pinnedGroup { get { return _embedded.pinnedGroup; } }

        public MyGroupsResource()
        {
            initializeProperties();
        }

        public MyGroupsResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        private void initializeProperties()
        {
            _links = new MyGroupsLinks();
            _embedded = new MyGroupsEmbedded();
        }

        private void initializeResources()
        {
            if (httpUtility != null)
            {
                if (_embedded.distributionGroup != null && _embedded.distributionGroup.Count > 0)
                {
                    foreach (DistributionGroupResource distGroupResource in _embedded.distributionGroup)
                    {
                        distGroupResource.httpUtility = httpUtility;
                        distGroupResource.initializeResources();
                    }
                }

                if (_embedded.group != null && _embedded.group.Count > 0)
                {
                    foreach (GroupResource groupResource in _embedded.group)
                    {
                        groupResource.httpUtility = httpUtility;
                    }
                }
                if (_embedded.defaultGroup != null)
                    _embedded.defaultGroup.httpUtility = httpUtility;
                if (_embedded.delegatesGroup != null)
                    _embedded.delegatesGroup.httpUtility = httpUtility;
                if (_embedded.delegatorsGroup != null)
                    _embedded.delegatorsGroup.httpUtility = httpUtility;
                if (_embedded.myOrganizationGroup != null)
                    _embedded.myOrganizationGroup.httpUtility = httpUtility;
                if (_embedded.pinnedGroup != null)
                    _embedded.pinnedGroup.httpUtility = httpUtility;
            }
        }

        //Get
        public async Task<IMyGroupsResource> Get()
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

        public new async Task<IMyGroupsResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
                initializeResources();
            }
            return this;
        }

        public async Task createNewContactGroup(string displayName)
        {
            if (httpUtility != null && _links.self != null)
            {
                string createNewGroupJson = JsonConvert.SerializeObject(new {
                    displayName = displayName
                });

                await httpUtility.httpPostJson(httpUtility.baseUrl + _links.self.href, createNewGroupJson);
            }
        }

        public async Task<IGroupResource> getDefaultGroup()
        {
            if (httpUtility != null && _links.defaultGroup != null)
            {
                GroupResource defaultGroupResource = new GroupResource(httpUtility);
                await defaultGroupResource.Get(httpUtility.baseUrl + _links.defaultGroup.href);
                return defaultGroupResource;
            }
            else
                return null;
        }

        public async Task<IGroupResource> getDelegatesGroup()
        {
            if (httpUtility != null && _links.delegatesGroup != null)
            {
                GroupResource delegatesGroupResource = new GroupResource(httpUtility);
                await delegatesGroupResource.Get(httpUtility.baseUrl + _links.delegatesGroup.href);
                return delegatesGroupResource;
            }
            else
                return null;
        }

        public async Task<List<IDistributionGroupResource>> getDistributionGroups()
        {
            if (httpUtility != null && _links.distributionGroup.Count > 0)
            {
                List<IDistributionGroupResource> distributionGroupList = new List<IDistributionGroupResource>();
                foreach (Link distributionGroup in _links.distributionGroup)
                {
                    IDistributionGroupResource newDistributionGroupResource = new DistributionGroupResource(httpUtility);
                    await newDistributionGroupResource.Get(httpUtility.baseUrl + distributionGroup.href);
                    distributionGroupList.Add(newDistributionGroupResource);
                }
                return distributionGroupList;
            }
            else
                return null;
        }

        public async Task<List<IGroupResource>> getGroups()
        {
            if (httpUtility != null && _links.group.Count > 0)
            {
                List<IGroupResource> groupList = new List<IGroupResource>();
                foreach (Link group in _links.group)
                {
                    IGroupResource newGroupResource = new GroupResource(httpUtility);
                    await newGroupResource.Get(httpUtility.baseUrl + group.href);
                    groupList.Add(newGroupResource);
                }
                return groupList;
            }
            else
                return null;
        }

        public async Task<IGroupResource> getMyOrganizationGroup()
        {
            if (httpUtility != null && _links.myOrganizationGroup != null)
            {
                IGroupResource myOrganizationGroupResource = new GroupResource(httpUtility);
                await myOrganizationGroupResource.Get(httpUtility.baseUrl + _links.myOrganizationGroup.href);
                return myOrganizationGroupResource;
            }
            else
                return null;
        }

        public async Task<IGroupResource> getPinnedGroup()
        {
            if (httpUtility != null && _links.pinnedGroup != null)
            {
                IGroupResource pinnedGroupResource = new GroupResource(httpUtility);
                await pinnedGroupResource.Get(httpUtility.baseUrl + _links.pinnedGroup.href);
                return pinnedGroupResource;
            }
            else
                return null;
        }
    }
}
