using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.R.RScript.Queries.GetRScriptList;

[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class GetRScriptListQuery : IRequest<RScriptListVm>
{ }

public class GetRScriptListQueryHandler : IRequestHandler<GetRScriptListQuery, RScriptListVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetRScriptListQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RScriptListVm> Handle(GetRScriptListQuery request, CancellationToken cancellationToken)
    {
        //check access

        var items = await _context.RScripts
            .ProjectTo<RScriptListDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.idRScript)
            .ToListAsync(cancellationToken);

        var vm = new RScriptListVm { Items = items };

        return vm;
    }
}