using AutoMapper;

namespace Application.Dictionary.District.Queries.GetDistrict;

/// <summary>
/// Get district information by provided Id
/// </summary>
/// <remarks>
/// District will likely to have more fields in actual application,
/// which is why it has its own separate get request
/// </remarks>
[Authorize(Modules = eAccountModule.DictionaryAdmin)]
public class GetDistrictQuery : IRequest<DistrictVm>
{
    /// <summary>
    /// Id of district in dictionary
    /// </summary>
    public int Id { get; set; }
}

public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, DistrictVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetDistrictQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DistrictVm> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
    {
        //check access
        var entity = await _context.Districts.GetAsync(request.Id, cancellationToken);

        var vm = _mapper.Map<DistrictVm>(entity);

        return vm;
    }
}

