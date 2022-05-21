namespace Application.Common.Interfaces;

public interface IMessageQueryService
{
    /// <summary>
    /// Send message into queue service for processing by worker service
    /// </summary>
    /// <param name="queue">Name of queue</param>
    /// <param name="message">Message</param>
    /// <param name="routingKey">Routing key</param>
    void Send(string queue, string message, string routingKey);

    /// <summary>
    /// Get queue length
    /// </summary>
    /// <param name="queue">Name of queue</param>
    int QueueLength(string queue);
}
