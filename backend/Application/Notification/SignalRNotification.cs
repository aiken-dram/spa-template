using Application.Common.Interfaces;
using Application.Common.Models;
using Shared.Application.Models;
using MediatR;

namespace Application.Notification;

public class SignalRNotification : SignalRCommand, INotification
{
    /// <summary>
    /// Subject of message sent to client through SignalR
    /// </summary>
    /// <example>Account.User.Commands.ProcessFile</example>
    public string Subject { get; set; }

    /// <summary>
    /// Body of message sent to client through SignalR
    /// </summary>
    /// <example>"User's password was updated"</example>
    public object Message { get; set; }

    /// <summary>
    /// Progress bar value
    /// </summary>
    /// <example>50</example>
    public int? Bar { get; set; }

    public SignalRNotification(string idConnection, string subject, object msg, int? bar = null)
    {
        this.IdConnection = idConnection;
        this.Subject = subject;
        this.Message = msg;
        this.Bar = bar;
    }

    public class SignalRNotificationHandler : INotificationHandler<SignalRNotification>
    {
        private readonly INotificationService _notification;

        public SignalRNotificationHandler(INotificationService notification)
        {
            _notification = notification;
        }

        public async Task Handle(SignalRNotification notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(new SignalRMessageDto
            {
                From = "server",
                To = notification.IdConnection,
                Subject = notification.Subject,
                Body = notification.Message,
                Bar = notification.Bar
            });
        }
    }
}
