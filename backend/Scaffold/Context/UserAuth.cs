using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class UserAuth
    {
        public long IdAuth { get; set; }
        public long IdUser { get; set; }
        public int IdAction { get; set; }
        public DateTime Stamp { get; set; }
        public string System { get; set; } = null!;
        public string? Message { get; set; }

        public virtual AuthAction IdActionNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
