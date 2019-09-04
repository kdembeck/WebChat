using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDembeck.UcwaWebApiClient.Resources
{
    public interface INoteResource : IResourceBase
    {
        string message { get; set; }
        NoteType? type { get; set; }
        NoteLinks _links { get; set; }
        Task<INoteResource> Get();
        new Task<INoteResource> Get(string resourceUrl);
        Task setNote(string message);
    }

    public class NoteLinks
    {
        public Link self;
    }
}
