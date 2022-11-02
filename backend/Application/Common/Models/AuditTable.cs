namespace Application.Common.Models;

/// <summary>
/// Audit data table class
/// </summary>
public class AuditDataTable : IMapFrom<VAuditData>
{
    /// <summary>
    /// Source of audit
    /// </summary>
    public string source { get; set; } = null!;

    /// <summary>
    /// Id of audit data
    /// </summary>
    public long id { get; set; }

    /// <summary>
    /// Id of data type in dictionary
    /// </summary>
    public int idType { get; set; }

    /// <summary>
    /// Name of data type from dictionary
    /// </summary>
    public string type { get; set; } = null!;

    /// <summary>
    /// Json with data
    /// </summary>
    public string? json { get; set; }
}

/// <summary>
/// Audit table class
/// </summary>
public class AuditTable : IMapFrom<VAudit>
{
    /// <summary>
    /// Source of audit
    /// </summary>
    public string source { get; set; } = null!;

    /// <summary>
    /// Id of user audit
    /// </summary>
    public long idAudit { get; set; }

    /// <summary>
    /// Id of user
    /// </summary>
    public long idUser { get; set; }

    /// <summary>
    /// User login
    /// </summary>
    public string login { get; set; } = null!;

    /// <summary>
    /// Id of target in dictionary
    /// </summary>
    public int idTarget { get; set; }

    /// <summary>
    /// Name of target from dictionary
    /// </summary>
    public string target { get; set; } = null!;

    /// <summary>
    /// Description of target from dictionary
    /// </summary>
    public string? targetDesc { get; set; }

    /// <summary>
    /// Id of action
    /// </summary>
    public int idAction { get; set; }

    /// <summary>
    /// Description of action
    /// </summary>
    public string? action { get; set; }

    /// <summary>
    /// Date and time of user audit
    /// </summary>
    public DateTime stamp { get; set; }

    /// <summary>
    /// Id of target entity
    /// </summary>
    public long? targetId { get; set; }

    /// <summary>
    /// Name of target entity
    /// </summary>
    public string? targetName { get; set; }

    /// <summary>
    /// Event message
    /// </summary>
    public string? message { get; set; }

    /// <summary>
    /// Collection of audit data
    /// </summary>
    public virtual ICollection<AuditDataTable>? auditData { get; set; }
}
