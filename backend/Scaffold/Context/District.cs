using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class District
    {
        public District()
        {
            UserDistricts = new HashSet<UserDistrict>();
        }

        public int IdDistrict { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserDistrict> UserDistricts { get; set; }
    }
}
