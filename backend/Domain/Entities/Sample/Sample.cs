namespace Domain.Entities;

#warning This is example, remove entire file in actual application
/// <summary>
/// Sample main table
/// </summary>
public partial class Sample : AuditableEntity, IHasDomainEvent
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public Sample()
    {
        SampleAudits = new HashSet<SampleAudit>();
        SampleChildren = new HashSet<SampleChild>();
    }

    /// <summary>
    /// Id of sample in database
    /// </summary>
    public long IdSample { get; set; }

    /// <summary>
    /// Id of sample type
    /// </summary>
    public eSampleType IdType { get; set; }

    /// <summary>
    /// Id of sample dictionary
    /// </summary>
    public long IdDict { get; set; }

    /// <summary>
    /// Text field
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Number field
    /// </summary>
    public long? Number { get; set; }

    /// <summary>
    /// Date field
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Time stamp field
    /// </summary>
    public DateTime? TimeStamp { get; set; }

    /// <summary>
    /// Decimal field
    /// </summary>
    public decimal? Sum { get; set; }

    /// <summary>
    /// Navigation to sample type
    /// </summary>
    public virtual SampleType IdTypeNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to sample dictionary
    /// </summary>
    public virtual SampleDict IdDictNavigation { get; set; } = null!;

    /// <summary>
    /// Collection of sample children
    /// </summary>
    /// <value></value>
    public virtual ICollection<SampleChild> SampleChildren { get; set; }

    /// <summary>
    /// Collection of sample audit
    /// </summary>
    public virtual ICollection<SampleAudit> SampleAudits { get; set; }
    #endregion

    /// <summary>
    /// Domain events
    /// </summary>
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.Sample;

    public override long? AuditTargetId => IdSample;

    public override string AuditTargetName => Text ?? string.Empty;
    #endregion
}
