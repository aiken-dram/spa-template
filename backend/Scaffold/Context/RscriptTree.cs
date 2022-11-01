using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class RscriptTree
    {
        public long Id { get; set; }
        public long? IdParent { get; set; }
        public long? IdRscript { get; set; }
        public string? Name { get; set; }
        public string? Modules { get; set; }
        public string? Icon { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }

        public virtual Rscript? IdRscriptNavigation { get; set; }
    }
}
