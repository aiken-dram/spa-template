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

    /// <summary>
    /// Queue for sending SignalR messages from hosted webapi
    /// </summary>
    /// <remarks>
    /// I dont wanna host every background service as web application
    /// so i'm gonna use this queue for alerts from background services
    /// to be sent to signalR clients from single hosted WebApi application
    /// </remarks>
    public const string SignalR = "spa_signalr";
}
