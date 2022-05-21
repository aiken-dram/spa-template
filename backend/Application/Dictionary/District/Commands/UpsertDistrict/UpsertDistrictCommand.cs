namespace Application.Dictionary.District.Commands.UpsertDistrict;

[Authorize(Modules = eAccountModule.DictionaryAdmin)]
public class UpsertDistrictCommand : IRequest
{
    /// <summary>
    /// District number
    /// </summary>
    public int IdDistrict { get; set; }

    /// <summary>
    /// Name of District
    /// </summary>
    public string Name { get; set; } = null!;
}

public class UpsertDistrictCommandHandler : IRequestHandler<UpsertDistrictCommand>
{
    private readonly ISPADbContext _context;

    public UpsertDistrictCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpsertDistrictCommand request, CancellationToken cancellationToken)
    {
        //check access
        Domain.Entities.District? entity;

        entity = await _context.Districts.FindIdAsync(request.IdDistrict, cancellationToken);

        if (entity == null)
        {
            entity = new Domain.Entities.District()
            {
                IdDistrict = request.IdDistrict,
                Name = request.Name
            };
            _context.Districts.Add(entity);
        }
        else
        {
            entity.Name = request.Name;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
