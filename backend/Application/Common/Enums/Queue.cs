namespace Application.Common.Enums;

/// <summary>
/// List of background worker queues in application
/// </summary>
#warning Names of Rabbit MQ queues on application server, ensure that this is unique across all deployed applications on the server
public static class eQueue
{
    /// <summary>
    /// Queue for processing requests
    /// </summary>
    public const string QueryService = "spa_query";

    /// <summary>
    /// Queue for processing R scripts (statistics)
    /// </summary>
    public const string RQueryService = "spa_rquery";
}
