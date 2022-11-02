namespace Domain.Entities;

/// <summary>
/// View of audit data union
/// </summary>
[DisplayName("VAuditData")]
public class VAuditData
{
    /// <summary>
    /// Source of audit data in union
    /// </summary>
    public string Source { get; set; } = null!;

    /// <summary>
    /// Id of audit data
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of user audit
    /// </summary>
    public long IdAudit { get; set; }

    /// <summary>
    /// Id of data type
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Name of data type from dictionary
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Json with data
    /// </summary>
    public string? Json { get; set; }

    /// <summary>
    /// Navigation to VAudit
    /// </summary>
    public virtual VAudit VAuditNavigation { get; set; } = null!;
}
