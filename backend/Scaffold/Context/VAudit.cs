using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class VAudit
    {
        public int IdAction { get; set; }
        public long IdAudit { get; set; }
        public int IdTarget { get; set; }
        public long IdUser { get; set; }
        public string? Message { get; set; }
        public string Srs { get; set; } = null!;
        public DateTime Stamp { get; set; }
        public long? TargetId { get; set; }
        public string? TargetName { get; set; }
    }
}
