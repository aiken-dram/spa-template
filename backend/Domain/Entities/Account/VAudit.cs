namespace Domain.Entities;

/// <summary>
/// View of audit union
/// </summary>
public partial class VAudit
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public VAudit()
    {
        AuditData = new HashSet<VAuditData>();
    }

    /// <summary>
    /// Source of audit in union
    /// </summary>
    public string Source { get; set; } = null!;

    /// <summary>
    /// Id of audit
    /// </summary>
    public long IdAudit { get; set; }

    /// <summary>
    /// Id of user
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// Id of target
    /// </summary>
    public int IdTarget { get; set; }

    /// <summary>
    /// Id of action
    /// </summary>
    public int IdAction { get; set; }

    /// <summary>
    /// Date and time of user audit
    /// </summary>
    public DateTime Stamp { get; set; }

    /// <summary>
    /// Id of target entity
    /// </summary>
    public long? TargetId { get; set; }

    /// <summary>
    /// Name of target entity
    /// </summary>
    public string? TargetName { get; set; }

    /// <summary>
    /// Event message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Collection of audit data
    /// </summary>
    public virtual ICollection<VAuditData> AuditData { get; set; }
}
