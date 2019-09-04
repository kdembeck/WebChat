using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Dynamic;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public class PhoneResource : ResourceBase, IPhoneResource
    {
        public bool includeInContactCard { get; set; }
        public string number { get; set; }
        public PhoneType? type { get; set; }
        public PhoneLinks _links { get; set; }

        private void initializeProperties()
        {
            includeInContactCard = false;
            number = null;
            type = null;
            _links = new PhoneLinks();
        }

        public PhoneResource()
        {
            initializeProperties();
        }

        public PhoneResource(IHttpUtility HttpUtility)
        {
            httpUtility = HttpUtility;
            initializeProperties();
        }

        //Get
        public async Task<IPhoneResource> Get()
        {
            if (httpUtility != null && _links.self != null)
            {
                string resourceUrl = httpUtility.baseUrl + _links.self.href;
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public new async Task<IPhoneResource> Get(string resourceUrl)
        {
            if (httpUtility != null)
            {   
                initializeProperties();
                await base.Get(resourceUrl);
            }
            return this;
        }

        public async Task changeNumber(string number = null, bool? includeInContactCard = null)
        {
            if (httpUtility != null && _links.changeNumber != null)
            {
                if (number != null || includeInContactCard != null)
                {
                    dynamic changeNumberSettings = new ExpandoObject();
                    string queryParams = "";
                    if (number != null)
                    {
                        changeNumberSettings.number = number;
                        queryParams += "?number=" + number;
                    }
                    if (includeInContactCard != null)
                    {
                        changeNumberSettings.includeInContactCard = includeInContactCard;
                        if (queryParams.Length > 0)
                            queryParams += "&";
                        else
                            queryParams += "?";

                        queryParams += "includeInContactCard=" + includeInContactCard;
                    }

                    string changeNumberJson = JsonConvert.SerializeObject(changeNumberSettings);

                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.changeNumber.href + queryParams, changeNumberJson);
                }
            }
        }

        public async Task changeVisibility(bool? includeInContactCard=null)
        {
            if (httpUtility != null && _links.changeVisibility != null)
            {
                string queryParams = "";
                if (includeInContactCard != null)
                {
                    string changeVisibilityJson = JsonConvert.SerializeObject(new
                    {
                        includeInContactCard = includeInContactCard
                    });
                    queryParams = "?includeInContactCard=" + includeInContactCard.ToString();
                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.changeVisibility.href + "?includeInContactCard=" + includeInContactCard.ToString(), changeVisibilityJson);
                }
                else
                {
                    await httpUtility.httpPostJson(httpUtility.baseUrl + _links.changeVisibility.href);
                }
            }
        }
    }
}
