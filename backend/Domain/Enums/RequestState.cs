namespace Domain.Enums;

/// <summary>
/// Request states for message query processing
/// </summary>
public enum eRequestState : int
{
    /// <summary>
    /// State after creating request to make a document
    /// </summary>
    [Dictionary("QUEUE")]
    InQueue = 1,

    /// <summary>
    /// State of request in process of making the file
    /// </summary>
    [Dictionary("PROCESSING")]
    Processing = 2,

    /// <summary>
    /// File has been created, ready to download
    /// </summary>
    [Dictionary("READY")]
    Ready = 3,

    /// <summary>
    /// Fila has been downloaded
    /// </summary>
    [Dictionary("DELIVERED")]
    Delivered = 4,

    /// <summary>
    /// Error while creating file
    /// </summary>
    [Dictionary("ERROR")]
    Error = 5
}