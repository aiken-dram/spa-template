namespace Application.Notification;

public class SignalRNotification : SignalRCommand, INotification
{
    /// <summary>
    /// Subject of message sent to client through SignalR
    /// </summary>
    /// <example>Account.User.Commands.ProcessFile</example>
    public string Subject { get; set; }

    /// <summary>
    /// Identity
    /// </summary>
    /// <example>1</example>
    public long? Id { get; set; }

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

    public SignalRNotification(string idConnection, string subject, long? id, object msg, int? bar = null)
    {
        this.IdConnection = idConnection;
        this.Subject = subject;
        this.Id = id;
        this.Message = msg;
        this.Bar = bar;
    }
}

public class SignalRNotificationHandler : INotificationHandler<SignalRNotification>
{
    private readonly INotificationService _notification;
    private readonly IUserService _user;

    public SignalRNotificationHandler(
        INotificationService notification,
        IUserService user)
    {
        _notification = notification;
        _user = user;
    }

    public async Task Handle(SignalRNotification notification, CancellationToken cancellationToken)
    {
        await _notification.SendAsync(new SignalRMessageDto
        {
            From = "server",
            To = notification.IdConnection,
            Subject = notification.Subject,
            Id = notification.Id,
            IdUser = _user.CurrentUserId,
            Body = notification.Message,
            Bar = notification.Bar
        });
    }
}
