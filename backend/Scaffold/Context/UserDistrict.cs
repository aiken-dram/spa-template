using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class UserDistrict
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public int IdDistrict { get; set; }

        public virtual District IdDistrictNavigation { get; set; } = null!;
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
