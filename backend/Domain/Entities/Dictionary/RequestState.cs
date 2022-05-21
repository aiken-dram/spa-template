namespace Domain.Entities;

/// <summary>
/// Request state dictionary
/// </summary>
public partial class RequestState
{
    public RequestState()
    {
        Requests = new HashSet<Domain.Entities.Request>();
    }

    /// <summary>
    /// Id of state in database
    /// </summary>
    public eRequestState IdState { get; set; }

    /// <summary>
    /// Name of state
    /// </summary>
    public string State { get; set; } = null!;

    /// <summary>
    /// Description of state
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of requests with this state
    /// </summary>
    public virtual ICollection<Domain.Entities.Request> Requests { get; set; }
}
