namespace Application.Sample.Commands.UpsertSample;

#warning This is example, remove entire file in actual application
public class UpsertSampleCommand : IRequest<long>
{
    public long? id { get; set; }
}

public class UpsertSampleCommandHandler : IRequestHandler<UpsertSampleCommand, long>
{
    private readonly ISPADbContext _context;

    public UpsertSampleCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpsertSampleCommand request, CancellationToken cancellationToken)
    {
        //check access

        Domain.Entities.Sample? entity;

        if (request.id.HasValue)
        {
            //edit
            entity = await _context.Samples
                .FindIdAsync(request.id.Value, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Sample), request.id);
        }
        else
        {
            //create
            entity = new Domain.Entities.Sample();
        }


        return entity.IdSample;
    }
}