using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class SampleAudit
    {
        public long IdAudit { get; set; }
        public long IdUser { get; set; }
        public int IdTarget { get; set; }
        public int IdAction { get; set; }
        public DateTime Stamp { get; set; }
        public long? TargetId { get; set; }
        public string? TargetName { get; set; }
        public string? Message { get; set; }

        public virtual AuditAction IdActionNavigation { get; set; } = null!;
        public virtual AuditTarget IdTargetNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
        public virtual Sample? Target { get; set; }
    }
}
