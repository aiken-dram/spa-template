using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class AuditTarget
    {
        public AuditTarget()
        {
            SampleAudits = new HashSet<SampleAudit>();
            UserAudits = new HashSet<UserAudit>();
        }

        public string? Desc { get; set; }
        public int IdTarget { get; set; }
        public string Target { get; set; } = null!;

        public virtual ICollection<SampleAudit> SampleAudits { get; set; }
        public virtual ICollection<UserAudit> UserAudits { get; set; }
    }
}
