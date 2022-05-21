using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class SampleAuditDatum
    {
        public long Id { get; set; }
        public long IdAudit { get; set; }
        public int IdType { get; set; }
        public string? Json { get; set; }

        public virtual SampleAudit IdAuditNavigation { get; set; } = null!;
        public virtual AuditDataType IdTypeNavigation { get; set; } = null!;
    }
}
