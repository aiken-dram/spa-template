using AutoMapper;

namespace Application.R.RScript.Queries.GetRScript;

[Authorize(Modules = eAccountModule.ConfigurationAdmin)]
public class GetRScriptQuery : IRequest<RScriptVm>
{
    /// <summary>
    /// Id of R script
    /// </summary>
    public long Id { get; set; }
}

public class GetRScriptQueryHandler : IRequestHandler<GetRScriptQuery, RScriptVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileService _file;

    public GetRScriptQueryHandler(
        ISPADbContext context,
        IMapper mapper,
        IFileService file)
    {
        _context = context;
        _mapper = mapper;
        _file = file;
    }

    public async Task<RScriptVm> Handle(GetRScriptQuery request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.RScripts
            .FindIdAsync(request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Domain.Entities.RScript), request.Id);

        var vm = _mapper.Map<RScriptVm>(entity);

        //R script file content
        vm.scriptContent = _file.ReadRScriptFile(entity.ScriptFile);

        return vm;
    }
}
