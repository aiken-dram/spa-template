namespace Application.Common.Interfaces;

public interface IMessageQueryService
{
    /// <summary>
    /// Send request into message query for processing with background worker
    /// </summary>
    /// <param name="request">Request</param>
    void SendRequest(Domain.Entities.Request request);

    /// <summary>
    /// Send request into message query for sending SignalR to clients about request update from web application
    /// </summary>
    /// <param name="request">Request</param>
    void SendRequestSignalR(Domain.Entities.Request request);

    /// <summary>
    /// Get queue length
    /// </summary>
    /// <param name="queue">Name of queue</param>
    int QueueLength(string queue);
}
