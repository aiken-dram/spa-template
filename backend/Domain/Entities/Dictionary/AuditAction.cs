namespace Domain.Entities;

/// <summary>
/// Audit action dictionary
/// </summary>
public partial class AuditAction
{
    public AuditAction()
    {
        UserAudits = new HashSet<UserAudit>();

#warning SAMPLE, remove in actual application
        SampleAudits = new HashSet<SampleAudit>();
    }

    /// <summary>
    /// Id of action in database
    /// </summary>
    public int IdAction { get; set; }

    /// <summary>
    /// Name of action
    /// </summary>
    public string Action { get; set; } = null!;

    /// <summary>
    /// Description of action
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of user audit with this action
    /// </summary>
    public virtual ICollection<UserAudit> UserAudits { get; set; }

#warning SAMPLE, remove in actual application
    /// <summary>
    /// Collection of sample audit with this action
    /// </summary>
    public virtual ICollection<SampleAudit> SampleAudits { get; set; }
}