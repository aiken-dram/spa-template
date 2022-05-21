namespace Application.Sample.Commands.DeleteSample;

#warning This is example, remove entire file in actual application
public class DeleteSampleCommand : IRequest
{
    /// <summary>
    /// Id of sample in database
    /// </summary>
    public long Id { get; set; }
}

public class DeleteSampleCommandHandler : IRequestHandler<DeleteSampleCommand>
{
    private readonly ISPADbContext _context;

    public DeleteSampleCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.Samples
            .FindIdAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Domain.Entities.Sample), request.Id);

        _context.Samples.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}