using System;
using System.Collections.Generic;

namespace Scaffold.Context
{
    public partial class Sample
    {
        public Sample()
        {
            SampleAudits = new HashSet<SampleAudit>();
            SampleChildren = new HashSet<SampleChild>();
        }

        public DateTime? Date { get; set; }
        public long IdDict { get; set; }
        public long IdSample { get; set; }
        public int IdType { get; set; }
        public long? Number { get; set; }
        public decimal? Sum { get; set; }
        public string? Text { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual SampleDict IdDictNavigation { get; set; } = null!;
        public virtual SampleType IdTypeNavigation { get; set; } = null!;
        public virtual ICollection<SampleAudit> SampleAudits { get; set; }
        public virtual ICollection<SampleChild> SampleChildren { get; set; }
    }
}
