namespace Domain.Entities;

/// <summary>
/// Request for message query processing service
/// </summary>
public partial class Request : AuditableEntity, IHasDomainEvent
{
    #region ENTITY
    /// <summary>
    /// Id of request in database
    /// </summary>
    public long IdRequest { get; set; }

    /// <summary>
    /// Id of user making request
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// Id of request type
    /// </summary>
    public eRequestType IdType { get; set; }

    /// <summary>
    /// Id of request state
    /// </summary>
    public eRequestState IdState { get; set; }

    /// <summary>
    /// JSON string with request parameters
    /// </summary>
    public string? Json { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    public string? Guid { get; set; }

    /// <summary>
    /// Timestamp of creating request
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Timestamp of processing request
    /// </summary>
    public DateTime? Processed { get; set; }

    /// <summary>
    /// Timestamp of delivering request result
    /// </summary>
    public DateTime? Delivered { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Navigation to user
    /// </summary>
    public virtual User IdUserNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to request type
    /// </summary>
    public virtual RequestType IdTypeNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to request type
    /// </summary>
    public virtual RequestState IdStateNavigation { get; set; } = null!;
    #endregion

    #region DOMAIN EVENTS
    /// <summary>
    /// Domain events
    /// </summary>
    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    #endregion

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.MessageQueryRequest;

    public override long? AuditTargetId => this.IdRequest;

    public override string AuditTargetName => $"{this.IdRequest}";
    #endregion
}
