namespace Domain.Entities;

/// <summary>
/// Event target dictionary
/// </summary>
public partial class EventTarget
{
    public EventTarget()
    {
        UserEvents = new HashSet<UserEvent>();
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
    /// Collection of user events with this target
    /// </summary>
    public virtual ICollection<UserEvent> UserEvents { get; set; }
}