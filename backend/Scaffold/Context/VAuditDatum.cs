using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class VAuditDatum
    {
        public long Id { get; set; }
        public long IdAudit { get; set; }
        public int IdType { get; set; }
        public string? Json { get; set; }
        public string Srs { get; set; } = null!;
    }
}
