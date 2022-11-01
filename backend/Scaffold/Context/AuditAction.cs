using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class AuditAction
    {
        public AuditAction()
        {
            SampleAudits = new HashSet<SampleAudit>();
            UserAudits = new HashSet<UserAudit>();
        }

        public int IdAction { get; set; }
        public string Action { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<SampleAudit> SampleAudits { get; set; }
        public virtual ICollection<UserAudit> UserAudits { get; set; }
    }
}
