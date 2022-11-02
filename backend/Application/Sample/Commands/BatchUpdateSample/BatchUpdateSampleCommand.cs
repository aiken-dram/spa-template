namespace Application.Sample.Commands.BatchUpdateSample;

#warning SAMPLE, remove entire file in actual application
/// <summary>
/// Batch updating samples
/// </summary>
public class BatchUpdateSampleCommand : SignalRCommand, IRequest<BatchUpdateSampleVm>
{
    /// <summary>
    /// Action for updating selected items
    /// type_a
    /// type_b
    /// </summary>
    /// <example>type_a</example>
    public string Action { get; set; } = null!;

    /// <summary>
    /// Array of ids of samples to update
    /// </summary>
    public IList<long> Items { get; set; } = null!;
}

public class BatchUpdateSampleCommandHandler : SignalRCommandHandler, IRequestHandler<BatchUpdateSampleCommand, BatchUpdateSampleVm>
{
    private readonly ISPADbContext _context;
    private readonly IAuditService _audit;

    public BatchUpdateSampleCommandHandler(
        ISPADbContext context,
        IMediator mediator,
        IAuditService audit)
        : base(mediator, eSignalRSubject.SampleBatchUpdate)
    {
        _context = context;
        _audit = audit;
    }

    private string _field = null!;
    private string _target = null!;

    private int _valInt;
    private long _valLong;

    private async Task<Message> ProcessItem(long id, CancellationToken cancellationToken)
    {
        var entity = await _context.Samples
            .FindIdAsync(id, cancellationToken);

        if (entity == null)
            return Message.Error(Messages.SampleIdNotFound(id));

        //change values
        switch (_field)
        {
            case "IdType":
                entity.IdType = _target switch
                {
                    "A" => eSampleType.A,
                    "B" => eSampleType.B,
                    _ => throw new BadRequestException(Messages.SampleBatchUpdateActionNotSupported)
                };
                break;
        }

        entity.Log(await _audit.SampleBatchUpdateAsync(entity));

        return Message.Success(Messages.SampleBatchUpdated(entity.Text, _field, _target));

    }

    public async Task<BatchUpdateSampleVm> Handle(BatchUpdateSampleCommand request, CancellationToken cancellationToken)
    {
        //check access

        //setup connection
        this.IdConnection = request.IdConnection;

        var res = new List<Message>();

        //setup values for batch update
        switch (request.Action)
        {
            case "type_a":
                _field = "IdType";
                _target = "A";
                break;
            case "type_b":
                _field = "IdType";
                _target = "B";
                break;
            default:
                throw new BadRequestException(Messages.SampleBatchUpdateActionNotSupported);
        }

        if (request.Items.Count > 0)
        {
            SetIteration(request.Items.Count);
            foreach (var i in request.Items)
            {
                var m = await ProcessItem(i, cancellationToken);
                await ReportNext(m, i);
                res.Add(m);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        return new BatchUpdateSampleVm(res);
    }
}
