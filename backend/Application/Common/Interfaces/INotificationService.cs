using Shared.Application.Models;

namespace Application.Common.Interfaces;

public interface INotificationService
{
    /// <summary>
    /// Send message to client with SignalR
    /// </summary>
    /// <param name="message">SignalR message data transfer object</param>
    Task SendAsync(SignalRMessageDto message);
}
