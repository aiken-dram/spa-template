namespace Domain.Entities;

/// <summary>
/// Event data type dictionary
/// </summary>
public partial class EventDataType
{
    public EventDataType()
    {
        UserEventData = new HashSet<UserEventData>();
    }

    /// <summary>
    /// Id of type in database
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Name of type
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Description of type
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of user event data with this type
    /// </summary>
    public virtual ICollection<UserEventData> UserEventData { get; set; }
}