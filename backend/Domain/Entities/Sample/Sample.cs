namespace Domain.Entities;

#warning SAMPLE, remove entire file in actual application
/// <summary>
/// Sample main table
/// </summary>
[DisplayName("Sample")]
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
    /// Id of district
    /// </summary>
    [Audit]
    public int? IdDistrict { get; set; }

    /// <summary>
    /// Id of sample type
    /// </summary>
    [Audit(dictionary: "SampleTypes")]
    public eSampleType IdType { get; set; }

    /// <summary>
    /// Id of sample dictionary
    /// </summary>
    [Audit(dictionary: "SampleDicts")]
    public long IdDict { get; set; }

    /// <summary>
    /// Text field
    /// </summary>
    [Audit]
    public string? Text { get; set; }

    /// <summary>
    /// Number field
    /// </summary>
    [Audit]
    public long? Number { get; set; }

    /// <summary>
    /// Date field
    /// </summary>
    [Audit]
    public DateTime? Date { get; set; }

    /// <summary>
    /// Time stamp field
    /// </summary>
    [Audit]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Decimal field
    /// </summary>
    [Audit]
    public decimal? Sum { get; set; }

    /// <summary>
    /// Navigation to district
    /// </summary>
    public virtual District? IdDistrictNavigation { get; set; }

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
