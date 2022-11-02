namespace Application.Dictionary.District.Commands.UpsertDistrict;

[Authorize(Modules = eAccountModule.DictionaryAdmin)]
public class UpsertDistrictCommand : IRequest<long>
{
    /// <summary>
    /// District number
    /// </summary>
    public int idDistrict { get; set; }

    /// <summary>
    /// Name of district
    /// </summary>
    public string name { get; set; } = null!;
}

public class UpsertDistrictCommandHandler : IRequestHandler<UpsertDistrictCommand, long>
{
    private readonly ISPADbContext _context;

    public UpsertDistrictCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpsertDistrictCommand request, CancellationToken cancellationToken)
    {
        //check access
        Domain.Entities.District? entity;

        entity = await _context.Districts
            .FindIdAsync(request.idDistrict, cancellationToken);

        if (entity == null)
        {
            //create
            entity = new Domain.Entities.District()
            {
                IdDistrict = request.idDistrict
            };
            _context.Districts.Add(entity);
        }

        //set fields
        entity.Name = request.name;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.IdDistrict;
    }
}

