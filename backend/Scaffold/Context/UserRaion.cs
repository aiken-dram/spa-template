using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class UserRaion
    {
        public long Id { get; set; }
        public int IdRaion { get; set; }
        public long IdUser { get; set; }

        public virtual Raion IdRaionNavigation { get; set; } = null!;
    }
}
