using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class SampleDict
    {
        public SampleDict()
        {
            Samples = new HashSet<Sample>();
        }

        public int IdDict { get; set; }
        public string Dict { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Sample> Samples { get; set; }
    }
}
