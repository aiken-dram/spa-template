using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class Request
    {
        public long IdRequest { get; set; }
        public long IdUser { get; set; }
        public int IdType { get; set; }
        public int IdState { get; set; }
        public string? Json { get; set; }
        public string? Guid { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Processed { get; set; }
        public DateTime? Delivered { get; set; }
        public string? Message { get; set; }

        public virtual RequestState IdStateNavigation { get; set; } = null!;
        public virtual RequestType IdTypeNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
