using Shared.Domain.Interfaces;

namespace Domain.Entities;

#warning This is example, remove entire file in actual application
/// <summary>
/// Sample audit log
/// </summary>
public partial class SampleAudit : IAudit
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public SampleAudit()
    {
        SampleAuditData = new HashSet<SampleAuditData>();
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
    /// Date and time of sample audit
    /// </summary>
    public DateTime Stamp { get; set; }

    /// <summary>
    /// Id of sample entity
    /// </summary>
    public long? TargetId { get; set; }

    /// <summary>
    /// Name of sample entity
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
    /// Navigation to sample
    /// </summary>
    public virtual Sample? TargetIdNavigation { get; set; }

    /// <summary>
    /// Collection of sample audit data
    /// </summary>
    public virtual ICollection<SampleAuditData> SampleAuditData { get; set; }
    #endregion

    /// <summary>
    /// Audit data
    /// </summary>
    public List<IAuditData> AuditData { get; set; } = new List<IAuditData>();

    /// <summary>
    /// Constructor from Audit
    /// </summary>
    /// <param name="audit">Audit</param>
    public SampleAudit(IAudit audit) : this()
    {
        IdUser = audit.IdUser;
        IdTarget = audit.IdTarget;
        IdAction = audit.IdAction;
        TargetId = audit.TargetId;
        TargetName = audit.TargetName;
        Stamp = audit.Stamp;
        Message = audit.Message;

        foreach (var d in audit.AuditData)
            SampleAuditData.Add(new SampleAuditData(d));
    }
}
