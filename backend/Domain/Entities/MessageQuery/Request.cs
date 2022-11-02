namespace Domain.Entities;

/// <summary>
/// Request for message query processing service
/// </summary>
[DisplayName("Request")]
public partial class Request : AuditableEntity
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

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.MessageQueryRequest;

    public override long? AuditTargetId => this.IdRequest;

    public override string AuditTargetName => this.IdType switch
    {
        eRequestType.TableExportAudit => Messages.RequestAuditExport,
        eRequestType.RScript => Messages.RequestRScript,

#warning SAMPLE
        eRequestType.TableExportSample => Messages.RequestSampleExport,

        _ => String.Empty
    };
    #endregion

    #region DOMAIN LOGIC
    /// <summary>
    /// Start processing request
    /// </summary>
    /// <returns>Generated GUID</returns>
    public string Start()
    {
        //generate guid
        Guid = System.Guid.NewGuid().ToString();

        //set state of request to processing
        IdState = eRequestState.Processing;

        return Guid;
    }

    /// <summary>
    /// Error while processing request
    /// </summary>
    /// <param name="err">Thrown Exception</param>
    public void Error(Exception err)
    {
        //set request state as error
        Message = err.Message.Truncate(500);
        IdState = eRequestState.Error;
    }

    /// <summary>
    /// Finish processing request
    /// </summary>
    public void Success()
    {
        //set request state as ready
        Processed = DateTime.Now;
        IdState = eRequestState.Ready;
    }
    #endregion
}
