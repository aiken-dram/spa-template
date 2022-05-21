using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class AuditDataType
    {
        public AuditDataType()
        {
            SampleAuditData = new HashSet<SampleAuditDatum>();
            UserAuditData = new HashSet<UserAuditDatum>();
        }

        public string? Desc { get; set; }
        public int IdType { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<SampleAuditDatum> SampleAuditData { get; set; }
        public virtual ICollection<UserAuditDatum> UserAuditData { get; set; }
    }
}
