using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class RscriptParamType
    {
        public RscriptParamType()
        {
            RscriptParams = new HashSet<RscriptParam>();
        }

        public int IdType { get; set; }
        public string Type { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<RscriptParam> RscriptParams { get; set; }
    }
}
