namespace Domain.Entities;

/// <summary>
/// Event action dictionary
/// </summary>
public partial class EventAction
{
    public EventAction()
    {
        UserEvents = new HashSet<UserEvent>();
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
    /// Collection of user events with this action
    /// </summary>
    public virtual ICollection<UserEvent> UserEvents { get; set; }
}