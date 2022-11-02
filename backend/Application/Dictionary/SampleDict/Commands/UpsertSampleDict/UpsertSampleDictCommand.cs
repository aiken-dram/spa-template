namespace Application.Dictionary.SampleDict.Commands.UpsertSampleDict;

#warning SAMPLE, remove entire file in actual application
[Authorize(Modules = eAccountModule.DictionaryAdmin)]
public class UpsertSampleDictCommand : IRequest<long>
{
    /// <summary>
    /// Id of dictionary in database
    /// </summary>
    public long? idDict { get; set; }

    /// <summary>
    /// Name of dictionary
    /// </summary>
    public string dict { get; set; } = null!;

    /// <summary>
    /// Description of dictionary
    /// </summary>
    public string? description { get; set; }
}

public class UpsertSampleDictCommandHandler : IRequestHandler<UpsertSampleDictCommand, long>
{
    private readonly ISPADbContext _context;

    public UpsertSampleDictCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpsertSampleDictCommand request, CancellationToken cancellationToken)
    {
        //check access

        Domain.Entities.SampleDict? entity;

        if (request.idDict.HasValue)
        {
            //edit
            entity = await _context.SampleDicts
                .GetAsync(request.idDict, cancellationToken);
        }
        else
        {
            //create
            entity = new Domain.Entities.SampleDict();
            _context.SampleDicts.Add(entity);
        }

        //set fields
        entity.Dict = request.dict;
        entity.Description = request.description;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.IdDict;
    }
}
