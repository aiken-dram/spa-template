using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class SampleChild
    {
        public long IdChild { get; set; }
        public long IdSample { get; set; }
        public string? Text { get; set; }

        public virtual Sample IdSampleNavigation { get; set; } = null!;
    }
}
