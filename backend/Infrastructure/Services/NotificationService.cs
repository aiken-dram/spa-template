using Application.Common.Interfaces;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Application.Common.Models;

namespace Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendAsync(SignalRMessageDto message)
    {
        await _hubContext.Clients.Client(message.To).SendAsync("notification", message);
    }

    public async Task SendAllAsync(SignalRMessageDto message)
    {
        await _hubContext.Clients.All.SendAsync("notification", message);
    }
}
