using Shared.Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// User activity event (audit log for general actions)
/// </summary>
public partial class UserEvent : IAuditEvent
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public UserEvent()
    {
        UserEventData = new HashSet<UserEventData>();
    }

    /// <summary>
    /// Id of user activity event
    /// </summary>
    public long IdEvent { get; set; }

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
    /// Date and time of user activity event
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
    public virtual EventAction IdActionNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to action
    /// </summary>
    public virtual EventTarget IdTargetNavigation { get; set; } = null!;

    /// <summary>
    /// Collection of user event data
    /// </summary>
    public virtual ICollection<UserEventData> UserEventData { get; set; }
    #endregion

    /// <summary>
    /// Event data
    /// </summary>
    public List<IAuditEventData> EventData { get; set; } = new List<IAuditEventData>();

    /// <summary>
    /// Constructor from AuditEvent
    /// </summary>
    /// <param name="audit">AuditEvent</param>
    public UserEvent(IAuditEvent audit) : this()
    {
        IdUser = audit.IdUser;
        IdTarget = audit.IdTarget;
        IdAction = audit.IdAction;
        TargetId = audit.TargetId;
        TargetName = audit.TargetName;
        Stamp = audit.Stamp;
        Message = audit.Message;

        foreach (var d in audit.EventData)
            UserEventData.Add(new UserEventData(d));
    }
}