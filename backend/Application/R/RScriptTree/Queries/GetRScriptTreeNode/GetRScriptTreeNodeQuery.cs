using AutoMapper;

namespace Application.R.RScriptTree.Queries.GetRScriptTreeNode;

[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class GetRScriptTreeNodeQuery : IRequest<RScriptTreeNodeVm>
{
    /// <summary>
    /// Id of R script tree node
    /// </summary>
    public long Id { get; set; }
}

public class GetRScriptTreeNodeQueryHandler : IRequestHandler<GetRScriptTreeNodeQuery, RScriptTreeNodeVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetRScriptTreeNodeQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RScriptTreeNodeVm> Handle(GetRScriptTreeNodeQuery request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.RScriptTree
            .GetAsync(request.Id, cancellationToken);

        var vm = _mapper.Map<RScriptTreeNodeVm>(entity);

        return vm;
    }
}