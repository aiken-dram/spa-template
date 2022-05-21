using AutoMapper;

namespace Application.Sample.Queries.GetSampleAuditTable;

public class GetSampleAuditTableQuery : IRequest<SampleAuditTableVm>
{
    /// <summary>
    /// Id of sample entity
    /// </summary>
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

        var vm = new SampleAuditTableVm();
        return vm;
    }
}