using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class Raion
    {
        public Raion()
        {
            UserRaions = new HashSet<UserRaion>();
        }

        public int IdRaion { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserRaion> UserRaions { get; set; }
    }
}
