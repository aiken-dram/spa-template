using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionary.District.Queries.GetDistrictList;

/// <summary>
/// Get list of districts in database
/// </summary>
public class GetDistrictListQuery : IRequest<DistrictListVm>
{ }

public class GetDistrictListQueryHandler : IRequestHandler<GetDistrictListQuery, DistrictListVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetDistrictListQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DistrictListVm> Handle(GetDistrictListQuery request, CancellationToken cancellationToken)
    {
        //check access
        //any user can get district list

        var items = await _context.Districts
            .ProjectTo<DistrictListDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.idDistrict)
            .ToListAsync(cancellationToken);

        var vm = new DistrictListVm { Items = items };

        return vm;
    }
}

