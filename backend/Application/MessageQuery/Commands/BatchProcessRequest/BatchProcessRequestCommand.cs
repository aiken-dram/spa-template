namespace Application.MessageQuery.Commands.BatchProcessRequest;

public class BatchProcessRequestCommand : SignalRCommand, IRequest<BatchProcessRequestVm>
{
    /// <summary>
    /// Array of ids of requests to process
    /// </summary>
    public IList<long> Items { get; set; } = null!;
}

public class BatchProcessRequestCommandHandler : SignalRCommandHandler, IRequestHandler<BatchProcessRequestCommand, BatchProcessRequestVm>
{
    private readonly ISPADbContext _context;

    public BatchProcessRequestCommandHandler(
        IMediator mediator,
        ISPADbContext context)
        : base(mediator, eSignalRSubject.BatchProcessRequest)
    {
        _context = context;
    }

    private async Task<(Message, long?)> ProcessRequest(long idRequest, CancellationToken cancellationToken)
    {
        var entity = await _context.Requests.FindIdAsync(idRequest, cancellationToken);
        if (entity == null)
            return (Message.Error(Messages.RequestNotFound), null);

        _context.Requests.Remove(entity);

        return (Message.Success(Messages.RequestDeleted), entity.IdUser);
    }

    public async Task<BatchProcessRequestVm> Handle(BatchProcessRequestCommand request, CancellationToken cancellationToken)
    {
        this.IdConnection = request.IdConnection;

        var res = new List<Message>();

        SetIteration(request.Items.Count());
        foreach (var id in request.Items)
        {
            (var m, var i) = await ProcessRequest(id, cancellationToken);
            await ReportNext(m, i);
            res.Add(m);

            //Thread.Sleep(TimeSpan.FromSeconds(1)); //was checking animation
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new BatchProcessRequestVm(res);
    }
}