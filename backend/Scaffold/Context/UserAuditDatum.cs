using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class UserAuditDatum
    {
        public long Id { get; set; }
        public long IdAudit { get; set; }
        public int IdType { get; set; }
        public string? Json { get; set; }

        public virtual UserAudit IdAuditNavigation { get; set; } = null!;
        public virtual AuditDataType IdTypeNavigation { get; set; } = null!;
    }
}
