namespace Application.Sample.Commands.BatchUpdateSample;

#warning SAMPLE, remove entire file in actual application
public class BatchUpdateSampleVm : ListVm<Message>
{
    public BatchUpdateSampleVm(IList<Message>? items) : base(items) { }
}
