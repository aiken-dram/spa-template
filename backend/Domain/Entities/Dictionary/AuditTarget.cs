namespace Domain.Entities;

/// <summary>
/// Audit target dictionary
/// </summary>
public partial class AuditTarget
{
    public AuditTarget()
    {
        UserAudits = new HashSet<UserAudit>();
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
}