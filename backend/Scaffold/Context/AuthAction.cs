using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class AuthAction
    {
        public AuthAction()
        {
            UserAuths = new HashSet<UserAuth>();
        }

        public int IdAction { get; set; }
        public string Action { get; set; } = null!;
        public string? Desc { get; set; }

        public virtual ICollection<UserAuth> UserAuths { get; set; }
    }
}
