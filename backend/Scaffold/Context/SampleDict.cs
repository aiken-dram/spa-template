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

        public string? Desc { get; set; }
        public string Dict { get; set; } = null!;
        public long IdDict { get; set; }

        public virtual ICollection<Sample> Samples { get; set; }
    }
}
