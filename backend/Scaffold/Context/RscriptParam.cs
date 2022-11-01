using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class RscriptParam
    {
        public long Id { get; set; }
        public long IdRscript { get; set; }
        public int IdType { get; set; }
        public string Name { get; set; } = null!;
        public string? Hint { get; set; }
        public string? Description { get; set; }

        public virtual Rscript IdRscriptNavigation { get; set; } = null!;
        public virtual RscriptParamType IdTypeNavigation { get; set; } = null!;
    }
}
