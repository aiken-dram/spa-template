namespace Application.Dictionary.District.Commands.DeleteDistrict;

[Authorize(Modules = eAccountModule.DictionaryAdmin)]
public class DeleteDistrictCommand : IRequest
{
    /// <summary>
    /// District id in dictionary
    /// </summary>
    public long Id { get; set; }
}

public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand>
{
    private readonly ISPADbContext _context;

    public DeleteDistrictCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.Districts
            .GetAsync(Convert.ToInt32(request.Id), cancellationToken);

        _context.Districts.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

