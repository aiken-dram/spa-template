namespace Application.MessageQuery.Commands.BatchProcessRequest;

public class BatchProcessRequestVm : ListVm<Message>
{
    public BatchProcessRequestVm(IList<Message>? items) : base(items) { }
}
