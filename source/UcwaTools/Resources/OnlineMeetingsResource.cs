using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class OnlineMeetingsResource : IResource
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string ResourceString { get; set; }
        public OnlineMeetingsLinks _links;

        public OnlineMeetingsResource()
        {   
            _links = new OnlineMeetingsLinks();
        }

        public async Task<string> GetResource(string resourceUri, HttpHelper httpHelper)
        {
            ResourceString = "";
            try
            {
                ResourceString = await httpHelper.HttpGetAction(httpHelper.ApplicationRootUri + resourceUri);
                FillResourceValues(ResourceString);
            }
            catch (Exception ex) { }
            return ResourceString;
        }

        public void FillResourceValues(string resourceString)
        {   
            ResourceString = resourceString;

            try
            {
                dynamic resourceObject = JObject.Parse(ResourceString);

                rel = resourceObject.rel;
                href = resourceObject.href;

                if (resourceObject._links != null)
                {
                    if (resourceObject._links.self != null)
                        _links.self.href = resourceObject._links.self.href;
                    if (resourceObject._links.myAssignedOnlineMeetings != null)
                        _links.myAssignedOnlineMeeting.href = resourceObject._links.myAssignedOnlineMeetings.href;
                    if (resourceObject._links.myOnlineMeetings != null)
                        _links.myOnlineMeetings.href = resourceObject._links.myOnlineMeetings.href;
                    if (resourceObject._links.onlineMeetingEligibleValues != null)
                        _links.onlineMeetingEligibleValues.href = resourceObject._links.onlineMeetingEligibleValues.href;
                    if (resourceObject._links.onlineMeetingInvitationCusomization != null)
                        _links.onlineMeetingInvitationCustomization.href = resourceObject._links.onlineMeetingInvitationCusomization.href;
                    if (resourceObject._links.onlineMeetingPolicies != null)
                        _links.onlineMeetingPolicies.href = resourceObject._links.onlineMeetingPolicies.href;
                    if (resourceObject._links.phoneDialInformation != null)
                        _links.phoneDialInformation.href = resourceObject._links.phoneDialInformation.href;
                }
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            if (ResourceString != null)
                return ResourceString;
            else
                return "";
        }
    }

    public class OnlineMeetingsLinks
    {
        public Link self;
        public Link myAssignedOnlineMeeting;
        public Link myOnlineMeetings;
        public Link onlineMeetingEligibleValues;
        public Link onlineMeetingInvitationCustomization;
        public Link onlineMeetingPolicies;
        public Link phoneDialInformation;
    }
}
