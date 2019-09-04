using System.Threading.Tasks;
using UcwaTools.Utilities;

namespace UcwaTools
{
    internal interface IResource
    {
        Task<string> GetResource(string resourceUri, HttpHelper httpHelper);
        void FillResourceValues(string resourceString);
        string ToString();
    }
}
