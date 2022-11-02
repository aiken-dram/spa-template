namespace Domain.Entities;

/// <summary>
/// Audit target dictionary
/// </summary>
public partial class AuditTarget
{
    public AuditTarget()
    {
        UserAudits = new HashSet<UserAudit>();

#warning SAMPLE, remove in actual application
        SampleAudits = new HashSet<SampleAudit>();
    }

    /// <summary>
    /// Id of target in dictionary
    /// </summary>
    public int IdTarget { get; set; }

    /// <summary>
    /// Name of target
    /// </summary>
    public string Target { get; set; } = null!;

    /// <summary>
    /// Description of target
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of user audit with this target
    /// </summary>
    public virtual ICollection<UserAudit> UserAudits { get; set; }

#warning SAMPLE, remove in actual application
    /// <summary>
    /// Collection of sample audit with this target
    /// </summary>
    public virtual ICollection<SampleAudit> SampleAudits { get; set; }
}