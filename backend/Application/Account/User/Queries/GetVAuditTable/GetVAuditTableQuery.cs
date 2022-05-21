using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Account.User.Queries.GetVAuditTable;

/// <summary>
/// Table of audit for auditing user activity
/// </summary>
public class GetVAuditTableQuery : TableQuery, IRequest<VAuditTableVm>
{
    /// <summary>
    /// Identity of user in database
    /// </summary>
    /// <example>1</example>
    public long? Id { get; set; }
}

public class GetVAuditTableQueryHandler : IRequestHandler<GetVAuditTableQuery, VAuditTableVm>
{
    private readonly ISPADbContext _context;
    private readonly IUserService _user;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetVAuditTableQueryHandler(
        ISPADbContext context,
        IUserService user,
        IMapper mapper,
        ILogger<GetVAuditTableQuery> logger)
    {
        _context = context;
        _user = user;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<VAuditTableVm> Handle(GetVAuditTableQuery request, CancellationToken cancellationToken)
    {
        //change into full access and resrict for everyone not admin here to view other that themselves
        //check access
        var curr = await _user.GetCurrentUserAsync(cancellationToken);
        _logger.JsonLogDebug("current user", curr);

        if (request.Id.HasValue && !curr.Modules.Contains(eAccountModule.SecurityAdmin) && curr.IdUser != request.Id.Value)
            throw new AccessDeniedException(Messages.NotMatchingUserId);

        if (!request.Id.HasValue && !curr.Modules.Contains(eAccountModule.SecurityAdmin))
            throw new AccessDeniedException(Messages.NoAccessToAuditUsers);
        //access checked

        if (request.Id.HasValue)
        {
            var user = await _context.Users.FindIdAsync(request.Id, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
        }

        //prepare filters
        var filterAfter = TableFilter.ToFilterList(request.Filters);

#warning this isnt finished, but need to compile at least, and debug pref to finish
        /*IQueryable<UserAudit> _query = _context.UserAudits.Include(p => p.UserAuditData);

        if (request.Id.HasValue)
            _query = _query.Where(p => p.IdUser == request.Id.Value);

        var query = _query
            .ProjectTo<AuditTableDto>(_mapper.ConfigurationProvider)
            .Filters(filterAfter);

        var vm = new AuditTableVm();
        vm.Items = await query.TableQuery(request, "stamp", true).ToListAsync(cancellationToken);
        vm.Total = await query.CountAsync(cancellationToken);
        return vm;*/

        return new VAuditTableVm();
    }
}