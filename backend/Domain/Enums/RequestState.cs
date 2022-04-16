namespace Domain.Enums;

/// <summary>
/// Request states for message query processing
/// </summary>
public static class eRequestState
{
    /// <summary>
    /// State after creating request to make a document
    /// </summary>
    public const string InQueue = "QUEUE";

    /// <summary>
    /// State of request in process of making the file
    /// </summary>
    public const string Processing = "PROCESSING";

    /// <summary>
    /// File has been created, ready to download
    /// </summary>
    public const string Ready = "READY";

    /// <summary>
    /// Fila has been downloaded
    /// </summary>
    public const string Delivered = "DELIVERED";

    /// <summary>
    /// Error while creating file
    /// </summary>
    public const string Error = "ERROR";
}
