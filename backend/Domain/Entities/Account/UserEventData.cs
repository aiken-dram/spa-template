using Shared.Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Data for user activity event
/// </summary>
public partial class UserEventData : IAuditEventData
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public UserEventData()
    {

    }

    /// <summary>
    /// Id of event data
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of user event
    /// </summary>
    public long IdEvent { get; set; }

    /// <summary>
    /// Id of data type
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Json with data
    /// </summary>
    public string? Json { get; set; }

    /// <summary>
    /// Navigation to user event
    /// </summary>
    public virtual UserEvent IdEventNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to data type
    /// </summary>
    public virtual EventDataType IdTypeNavigation { get; set; } = null!;
    #endregion

    /// <summary>
    /// Constructor from AuditEventData
    /// </summary>
    /// <param name="data">AuditEventData</param>
    public UserEventData(IAuditEventData data)
    {
        IdType = data.IdType;
        Json = data.Json;
    }
}