using Application.Notification;

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
    public string IdConnection { get; set; } = null!;
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
    public string? IdConnection { get; set; }

    /// <summary>
    /// Disable sending SignalR messages if IdConnection equals 'disableSignalR'
    /// </summary>
    private bool DisableSignalR { get { return IdConnection == "disableSignalR"; } }

    private int? _element = 0; //current iteration
    private int _count = 0; //count of items to process in iterations
    private int? _bar = 0; //current bar progress

    public SignalRCommandHandler(IMediator mediator, string subject)
    {
        _mediator = mediator;
        _subject = subject;
    }

    /// <summary>
    /// Publishes SignalR notification event
    /// </summary>
    /// <param name="msg">Message body</param>
    /// <param name="id">Identity</param>
    /// <param name="bar">Progress bar value</param>
    protected async Task Report(object msg, long? id = null, int? bar = null)
    {
        //wait should i wrap async method as another async?
        if (!DisableSignalR)
        {
            if (IdConnection == null)
                throw new Exception(Messages.ConnectionHasNotBeenSet);
            await _mediator.Publish(new SignalRNotification(IdConnection, _subject, id, msg, bar));
        }
    }

    /// <summary>
    /// Setup iteration of count elements
    /// </summary>
    /// <param name="count">Count of elements in iteration</param>
    protected void SetIteration(int count)
    {
        this._element = 0;
        this._count = count;
        this._bar = GetBar();
    }

    /// <summary>
    /// Get progress bar value
    /// </summary>
    /// <returns>Progress bar value (0-100)</returns>
    private int? GetBar()
    {
        if (_element == null)
            return null;
        if (_element == 0)
            return 0;
        if (_count == 0)
            return 100;
        if (_element > _count)
            return 100;
        return ((_element - 1) * 100) / _count;
    }

    /// <summary>
    /// Moves iterator to next element
    /// </summary>
    protected void Next()
    {
        if (_element == null)
            throw new Exception(Messages.IterationHasNotBeenSet);

        this._element++;
        this._bar = GetBar();
    }

    /// <summary>
    /// Moves iterator to next element and publishes SignalR notification event
    /// </summary>
    /// <param name="msg">Message body</param>
    /// <param name="id">Identity</param>
    protected async Task ReportNext(object msg, long? id = null)
    {
        Next();

        await Report(msg, id, _bar);
    }
}
