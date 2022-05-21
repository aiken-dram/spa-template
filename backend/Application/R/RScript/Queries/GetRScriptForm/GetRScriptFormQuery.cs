using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.R.RScript.Queries.GetRScriptForm;

public class GetRScriptFormQuery : IRequest<RScriptFormVm>
{
    /// <summary>
    /// Id of R script
    /// </summary>
    public long Id { get; set; }
}

public class GetRScriptFormQueryHandler : IRequestHandler<GetRScriptFormQuery, RScriptFormVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetRScriptFormQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RScriptFormVm> Handle(GetRScriptFormQuery request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.RScripts
            .Include(p => p.RScriptParams).ThenInclude(p => p.IdTypeNavigation)
            .FirstOrDefaultAsync(p => p.IdRScript == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Domain.Entities.RScript), request.Id);

        var vm = _mapper.Map<RScriptFormVm>(entity);

        return vm;
    }
}