namespace Domain.Enums;

/// <summary>
/// Audit actions in application
/// </summary>
public enum eAuditAction : int
{
    /// <summary>
    /// Create entity
    /// </summary>
    [Dictionary("CREATE")]
    Create = 1,

    /// <summary>
    /// Edit entity
    /// </summary>
    [Dictionary("EDIT")]
    Edit = 2,

    /// <summary>
    /// Delete entity
    /// </summary>
    [Dictionary("DELETE")]
    Delete = 3,
}

/// <summary>
/// Auth audit action in application
/// </summary>
public enum eAuthAuditAction : int
{
    /// <summary>
    /// User successfully authenticated
    /// </summary>
    [Dictionary("AUTH_LOGIN")]
    Login = 4,

    /// <summary>
    /// User entered wrong password
    /// </summary>
    [Dictionary("AUTH_WRONGPASS")]
    WrongPassword = 5,

    /// <summary>
    /// User's password was expired
    /// </summary>
    [Dictionary("AUTH_EXPIRED")]
    Expired = 6,

    /// <summary>
    /// User was locked
    /// </summary>
    [Dictionary("AUTH_LOCK")]
    Lock = 7,
}

/// <summary>
/// User audit action in application
/// </summary>
public enum eUserAuditAction : int
{
    /// <summary>
    /// Password updated from file
    /// </summary>
    [Dictionary("USER_UPDATE_PASS")]
    UpdatePassword = 8
}

/// <summary>
/// Message query audit action in application
/// </summary>
public enum eMessageQueryAuditAction : int
{
    /// <summary>
    /// Create new request to message query
    /// </summary>
    [Dictionary("MQ_REQUEST")]
    Request = 9,

    /// <summary>
    /// Error while processing request from message query
    /// </summary>
    [Dictionary("MQ_ERROR")]
    Error = 10,

    /// <summary>
    /// Request from message query was processed
    /// </summary>
    [Dictionary("MQ_READY")]
    Ready = 11,

    /// <summary>
    /// Downloaded request from message query
    /// </summary>
    [Dictionary("MQ_RECEIVED")]
    Receive = 12
}

#warning SAMPLE, remove in actual application
public enum eSampleAuditAction : int
{
    /// <summary>
    /// Batch update sample entities
    /// </summary>
    [Dictionary("SAMPLE_BATCH_UPDATE")]
    BatchUpdate = 13
}