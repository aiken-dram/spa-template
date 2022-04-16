using Shared.Application.Models;

namespace Application.Account.User.Commands.ProcessFile;

/// <summary>
/// View model for processing new password files
/// </summary>
public class ProcessFileVm
{
    /// <summary>
    /// List of messages
    /// </summary>
    public IList<Message> Items { get; set; }
}
