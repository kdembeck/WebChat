using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UcwaTools.Utilities;

namespace UcwaTools
{
    public class OnlineMeetingsManager
    {
        private OnlineMeetingsResource onlineMeetingsResource;
        private HttpHelper httpHelper;

        public MyOnlineMeetingsManager myOnlineMeetings;

        public OnlineMeetingsManager(HttpHelper HttpHelper)
        {
            httpHelper = HttpHelper;
        }

        public async Task Update(string OnlineMeetingsResourceUri)
        {
            if (onlineMeetingsResource == null)
                onlineMeetingsResource = new OnlineMeetingsResource();

            await onlineMeetingsResource.GetResource(OnlineMeetingsResourceUri, httpHelper);

            //initialize myOnlineMeetings here
            if (myOnlineMeetings == null)
                myOnlineMeetings = new MyOnlineMeetingsManager(httpHelper);

            await myOnlineMeetings.Update(onlineMeetingsResource._links.myOnlineMeetings.href);
        }
    }
}
