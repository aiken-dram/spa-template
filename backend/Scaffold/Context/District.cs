using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class District
    {
        public District()
        {
            Samples = new HashSet<Sample>();
            UserDistricts = new HashSet<UserDistrict>();
        }

        public int IdDistrict { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Sample> Samples { get; set; }
        public virtual ICollection<UserDistrict> UserDistricts { get; set; }
    }
}
