using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class MyOnlineMeetingsManager
    {
        private MyOnlineMeetingsResource myOnlineMeetingsResource;
        private HttpHelper httpHelper;

        public MyOnlineMeetingsManager(HttpHelper HttpHelper)
        {
            httpHelper = HttpHelper;
        }

        public async Task Update(string OnlineMeetingsResourceUri)
        {
            if (myOnlineMeetingsResource == null)
                myOnlineMeetingsResource = new MyOnlineMeetingsResource();

            await myOnlineMeetingsResource.GetResource(OnlineMeetingsResourceUri, httpHelper);
        }

        public async Task<MyOnlineMeetingResource> MeetNow(string subject = "",string description = "") //, string accessLevel = "", List<string> attendees = null, List<string> leaders = null, string phoneUserAdmission = "", string lobbyBypassForPhoneUsers = "", string expirationTime = "", string entryExitAnnouncement = "", string automaticLeaderAssignment = "")
        {
            string meetNowJson = JsonConvert.SerializeObject(new
            {
                subject = subject,
                description = description,
                //accessLevel = accessLevel,
                //attendees = attendees,
                //leaders = leaders,
                //phoneUserAdmission = phoneUserAdmission,
                //lobbyBypassForPhoneUsers = lobbyBypassForPhoneUsers,
                //expirationTime = expirationTime,
                //entryExitAnnouncement = entryExitAnnouncement,
                //automaticLeaderAssignment = automaticLeaderAssignment
            });

            string myOnlineMeetingJsonString = await httpHelper.HttpPostAction(httpHelper.ApplicationRootUri + myOnlineMeetingsResource._links.self.href, meetNowJson);

            MyOnlineMeetingResource myOnlineMeetingResource = new MyOnlineMeetingResource();

            myOnlineMeetingResource.FillResourceValues(myOnlineMeetingJsonString);

            //join online meeting from the communication controller
            return myOnlineMeetingResource;
        }

    }
}
