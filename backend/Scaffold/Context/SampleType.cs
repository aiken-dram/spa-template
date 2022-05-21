using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class SampleType
    {
        public SampleType()
        {
            Samples = new HashSet<Sample>();
        }

        public string? Desc { get; set; }
        public int IdType { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Sample> Samples { get; set; }
    }
}
