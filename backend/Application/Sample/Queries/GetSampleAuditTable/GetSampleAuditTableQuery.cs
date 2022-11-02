using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Sample.Queries.GetSampleAuditTable;

#warning SAMPLE, remove entire file in actual application
public class GetSampleAuditTableQuery : TableQuery, IRequest<SampleAuditTableVm>
{
    /// <summary>
    /// Id of sample in database
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; }
}

public class GetSampleAuditTableQueryHandler : IRequestHandler<GetSampleAuditTableQuery, SampleAuditTableVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetSampleAuditTableQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SampleAuditTableVm> Handle(GetSampleAuditTableQuery request, CancellationToken cancellationToken)
    {
        //check access

        //prepare filters
        var filterAfter = TableFilter.ToFilterList(request.Filters);

        var query = _context.SampleAudits.Include(p => p.SampleAuditData)
            .Where(p => p.TargetId == request.Id)
            .ProjectTo<SampleAuditTableDto>(_mapper.ConfigurationProvider)
            .Filters(filterAfter);

        var vm = new SampleAuditTableVm();
        vm.Items = await query.TableQuery(request, "stamp", true).ToListAsync(cancellationToken);
        vm.Total = await query.CountAsync(cancellationToken);
        return vm;
    }
}