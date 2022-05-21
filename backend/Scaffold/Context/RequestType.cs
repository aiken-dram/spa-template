using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class RequestType
    {
        public RequestType()
        {
            Requests = new HashSet<Request>();
        }

        public string? Desc { get; set; }
        public int IdType { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
