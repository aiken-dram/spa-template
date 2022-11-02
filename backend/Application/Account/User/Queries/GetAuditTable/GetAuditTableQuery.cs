using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Account.User.Queries.GetAuditTable;

/// <summary>
/// Table of audit for auditing user activity
/// </summary>
public class GetAuditTableQuery : TableQuery, IRequest<AuditTableVm>
{
    /// <summary>
    /// Identity of user in database
    /// </summary>
    /// <example>1</example>
    public long? IdUser { get; set; }

#warning SAMPLE
    /// <summary>
    /// Identity of sample in database
    /// </summary>
    /// <example>1</example>
    public long? IdSample { get; set; }
}

public class GetAuditTableQueryHandler : IRequestHandler<GetAuditTableQuery, AuditTableVm>
{
    private readonly ISPADbContext _context;
    private readonly IUserService _user;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAuditTableQueryHandler(
        ISPADbContext context,
        IUserService user,
        IMapper mapper,
        ILogger<GetAuditTableQuery> logger)
    {
        _context = context;
        _user = user;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Reusable query for VAuditTable
    /// </summary>
    /// <remarks>
    /// also can use this for making TableCsv mediatr without resolving to calling mediatr from mediatr
    /// </remarks>
    /// <param name="context">ISPADbContext ref</param>
    /// <param name="mapper">IMapper ref</param>
    /// <param name="IdUser">Id of user</param>
    /// <param name="IdSample">Id of sample</param>
    /// <param name="Filters">Filters for table</param>
    /// <returns>IQueryable for VAuditTable</returns>
    public static IQueryable<AuditTable> Query(ISPADbContext context, IMapper mapper, long? IdUser, long? IdSample, IList<string>? Filters)
    {
        //prepare filters
        var filterAfter = TableFilter.ToFilterList(Filters);

        IQueryable<VAudit> _query = context.VAudits.Include(p => p.AuditData);

        if (IdUser.HasValue)
            _query = _query.Where(p => p.IdUser == IdUser.Value);

        if (IdSample.HasValue)
            _query = _query.Where(p => p.TargetId == IdSample && p.IdTarget == (int)eAuditTarget.Sample);

        var query = _query.ProjectTo<AuditTable>(mapper.ConfigurationProvider);

        /*
        var query =
            from a in _query
            join u in context.Users
            on a.IdUser equals u.IdUser
            join aa in context.AuditActions
            on a.IdAction equals aa.IdAction
            join at in context.AuditTargets
            on a.IdTarget equals at.IdTarget
            select new VAuditTableDto
            {
                source = a.Source,
                idAudit = a.IdAudit,
                idUser = a.IdUser,
                login = u.Login,
                idTarget = a.IdTarget,
                target = at.Target,
                targetDesc = at.Description,
                idAction = a.IdAction,
                action = aa.Description,
                stamp = a.Stamp,
                targetId = a.TargetId,
                targetName = a.TargetName,
                message = a.Message,
                auditData = (
                    from d in context.VAuditData
                    join dt in context.AuditDataTypes
                    on d.IdType equals dt.IdType
                    where d.Source == a.Source && d.IdAudit == a.IdAudit
                    select new VAuditDataTableDto
                    {
                        source = d.Source,
                        id = d.Id,
                        idType = d.IdType,
                        type = dt.Type,
                        json = d.Json
                    }).ToList()
            };*/

        query = query.Filters(filterAfter);
        return query;
    }

    public async Task<AuditTableVm> Handle(GetAuditTableQuery request, CancellationToken cancellationToken)
    {
        //check access
        await _user.GetAuditTableAccess(request.IdUser, request.IdSample, cancellationToken);

        var query = Query(_context, _mapper, request.IdUser, request.IdSample, request.Filters);

        var vm = new AuditTableVm();
        vm.Items = await query.TableQuery(request, "stamp", true).ToListAsync(cancellationToken);
        vm.Total = await query.CountAsync(cancellationToken);
        return vm;
    }
}