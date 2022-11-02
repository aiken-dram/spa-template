namespace Application.Common.Models;

/// <summary>
/// SignalR message data transfer object
/// </summary>
public class SignalRMessageDto
{
    /// <summary>
    /// From
    /// </summary>
    public string From { get; init; } = null!;

    /// <summary>
    /// To (client connection)
    /// </summary>
    public string To { get; init; } = null!;

    /// <summary>
    /// Subject
    /// </summary>
    public string Subject { get; init; } = null!;

    /// <summary>
    /// Identity
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    /// User
    /// </summary>
    public long? IdUser { get; init; }

    /// <summary>
    /// Body
    /// </summary>
    public object? Body { get; init; }

    /// <summary>
    /// Progress bar stage (0-100)
    /// </summary>
    public int? Bar { get; init; }
}
