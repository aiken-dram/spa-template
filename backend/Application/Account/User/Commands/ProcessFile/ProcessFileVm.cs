namespace Application.Account.User.Commands.ProcessFile;

/// <summary>
/// View model for processing new password files
/// </summary>
public class ProcessFileVm : ListVm<Message>
{
    public ProcessFileVm(IList<Message>? items) : base(items) { }
}