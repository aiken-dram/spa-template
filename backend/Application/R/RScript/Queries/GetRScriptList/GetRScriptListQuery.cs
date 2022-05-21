using AutoMapper;

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

        var vm = new RScriptListVm();
        return vm;
    }
}