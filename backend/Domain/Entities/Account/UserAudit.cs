using Shared.Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// User audit log (for general actions)
/// </summary>
public partial class UserAudit : IAudit
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public UserAudit()
    {
        UserAuditData = new HashSet<UserAuditData>();
    }

    /// <summary>
    /// Id of user audit
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
    /// Navigation to user
    /// </summary>
    public virtual User IdUserNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to target
    /// </summary>
    public virtual AuditAction IdActionNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to action
    /// </summary>
    public virtual AuditTarget IdTargetNavigation { get; set; } = null!;

    /// <summary>
    /// Collection of user audit data
    /// </summary>
    public virtual ICollection<UserAuditData> UserAuditData { get; set; }
    #endregion

    /// <summary>
    /// Audit data
    /// </summary>
    public List<IAuditData> AuditData { get; set; } = new List<IAuditData>();

    /// <summary>
    /// Constructor from Audit
    /// </summary>
    /// <param name="audit">Audit</param>
    public UserAudit(IAudit audit) : this()
    {
        IdUser = audit.IdUser;
        IdTarget = audit.IdTarget;
        IdAction = audit.IdAction;
        TargetId = audit.TargetId;
        TargetName = audit.TargetName;
        Stamp = audit.Stamp;
        Message = audit.Message;

        foreach (var d in audit.AuditData)
            UserAuditData.Add(new UserAuditData(d));
    }
}