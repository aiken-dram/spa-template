using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class RscriptTree
    {
        public string? Color { get; set; }
        public string? Desc { get; set; }
        public string? Icon { get; set; }
        public long Id { get; set; }
        public long? IdParent { get; set; }
        public long? IdRscript { get; set; }
        public string? Modules { get; set; }
        public string Name { get; set; } = null!;

        public virtual Rscript? IdRscriptNavigation { get; set; }
    }
}
