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

        public int IdType { get; set; }
        public string Type { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Sample> Samples { get; set; }
    }
}
