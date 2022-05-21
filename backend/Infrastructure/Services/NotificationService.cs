using Application.Common.Interfaces;
using Shared.Application.Models;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Service
{
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
}