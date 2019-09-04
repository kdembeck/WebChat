using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDembeck.UcwaWebApiClient.Utilities;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface IResourceBase
    {   
        IHttpUtility httpUtility { get; set; }
        //string rel { get; set; }
        //string href { get; set; }
        Task Get(string ResourceUrl);
    }
}
