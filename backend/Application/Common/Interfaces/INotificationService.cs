namespace Application.Common.Interfaces;

public interface INotificationService
{
    /// <summary>
    /// Send message to client with SignalR
    /// </summary>
    /// <param name="message">SignalR message data transfer object</param>
    Task SendAsync(SignalRMessageDto message);

    /// <summary>
    /// Send message to all clients
    /// </summary>
    /// <param name="message">SignalR message data transfer object</param>
    Task SendAllAsync(SignalRMessageDto message);
}
