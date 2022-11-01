using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class VAuditDatum
    {
        public string Source { get; set; } = null!;
        public long Id { get; set; }
        public long IdAudit { get; set; }
        public int IdType { get; set; }
        public string Type { get; set; } = null!;
        public string? Json { get; set; }
    }
}
