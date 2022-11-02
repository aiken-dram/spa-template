namespace Application.Dictionary.SampleDict.Commands.DeleteSampleDict;

#warning SAMPLE, remove entire file in actual application
/// <summary>
/// Delete sample dictionary by provided Id
/// </summary>
[Authorize(Modules = eAccountModule.DictionaryAdmin)]
public class DeleteSampleDictCommand : IRequest
{
    /// <summary>
    /// Id of sample dictionary in database
    /// </summary>
    public long Id { get; set; }
}

public class DeleteSampleDictCommandHandler : IRequestHandler<DeleteSampleDictCommand>
{
    private readonly ISPADbContext _context;

    public DeleteSampleDictCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSampleDictCommand request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.SampleDicts
            .GetAsync(request.Id, cancellationToken);

        _context.SampleDicts.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
