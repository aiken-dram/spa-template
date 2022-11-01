using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class VAudit
    {
        public string Source { get; set; } = null!;
        public long IdAudit { get; set; }
        public long IdUser { get; set; }
        public string Login { get; set; } = null!;
        public int IdTarget { get; set; }
        public string Target { get; set; } = null!;
        public string? TargetDesc { get; set; }
        public int IdAction { get; set; }
        public string? Action { get; set; }
        public DateTime Stamp { get; set; }
        public long? TargetId { get; set; }
        public string? TargetName { get; set; }
        public string? Message { get; set; }
    }
}
