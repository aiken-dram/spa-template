using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class Rscript
    {
        public Rscript()
        {
            RscriptParams = new HashSet<RscriptParam>();
            RscriptTrees = new HashSet<RscriptTree>();
        }

        public string ContentType { get; set; } = null!;
        public string? Desc { get; set; }
        public long IdRscript { get; set; }
        public string Name { get; set; } = null!;
        public string ResultFile { get; set; } = null!;
        public string ScriptFile { get; set; } = null!;

        public virtual ICollection<RscriptParam> RscriptParams { get; set; }
        public virtual ICollection<RscriptTree> RscriptTrees { get; set; }
    }
}
