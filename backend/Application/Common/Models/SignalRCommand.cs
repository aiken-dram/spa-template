using Application.Notification;
using MediatR;

namespace Application.Common.Models;

/// <summary>
/// Command expects SignalR messages from server
/// </summary>
public class SignalRCommand
{
    /// <summary>
    /// UNID of SignalR connection to client
    /// </summary>
    /// <example>pjr5YU-uBjALoJZwWOTnzw</example>
    public string IdConnection { get; set; }
}

/// <summary>
/// Handler for command that expects SignalR messages from server
/// </summary>
public class SignalRCommandHandler
{
    private readonly IMediator _mediator;
    private readonly string _subject;

    /// <summary>
    /// UNID of SignalR connection to client
    /// </summary>
    /// <example>pjr5YU-uBjALoJZwWOTnzw</example>
    public string IdConnection { get; set; }

    /// <summary>
    /// Disable sending SignalR messages if IdConnection equals 'disableSignalR'
    /// </summary>
    private bool DisableSignalR { get { return IdConnection == "disableSignalR"; } }

    public SignalRCommandHandler(IMediator mediator, string subject)
    {
        _mediator = mediator;
        _subject = subject;
    }

    /// <summary>
    /// Publishes SignalR notification event
    /// </summary>
    /// <param name="msg">Message body</param>
    protected async Task Report(object msg)
    {
        //wait should i wrap async method as another async?
        if (!DisableSignalR)
            await _mediator.Publish(new SignalRNotification(IdConnection, _subject, msg));
    }
}