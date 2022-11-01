using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class Module
    {
        public Module()
        {
            RoleModules = new HashSet<RoleModule>();
        }

        public long IdModule { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<RoleModule> RoleModules { get; set; }
    }
}
