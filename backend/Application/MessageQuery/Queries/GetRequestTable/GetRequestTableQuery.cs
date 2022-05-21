using AutoMapper;
using Microsoft.Extensions.Logging;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Request.Queries.GetRequestTable;

public class GetRequestTableQuery : TableQuery, IRequest<RequestTableVm>
{
    /// <summary>
    /// Selected folder:
    /// 0 - incoming
    /// 1 - delivered
    /// </summary>
    public int SelectedFolder { get; set; }
}

public class GetRequestTableQueryHandler : IRequestHandler<GetRequestTableQuery, RequestTableVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _user;
    private readonly ILogger _logger;

    public GetRequestTableQueryHandler(
        ISPADbContext context,
        IMapper mapper,
        IUserService user,
        ILogger<GetRequestTableQuery> logger)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
        _logger = logger;
    }

    public async Task<RequestTableVm> Handle(GetRequestTableQuery request, CancellationToken cancellationToken)
    {
        //_logger.JsonLogDebug("Current user", user);

        var _query =
            from d in _context.Requests
            where d.IdUser == _user.CurrentUserId
            select d;

        switch (request.SelectedFolder)
        {
            case 0:
                _query = _query.Where(p => p.IdState != eRequestState.Delivered);
                break;
            case 1:
                _query = _query.Where(p => p.IdState == eRequestState.Delivered);
                break;
        }

        var query = _query
            .ProjectTo<RequestTableDto>(_mapper.ConfigurationProvider);

        //check access
        var vm = new RequestTableVm();
        request.SortDesc = true;
        vm.Items = await query
            .TableQuery(request, "created")
            .ToListAsync(cancellationToken);
        vm.Total = await query.CountAsync(cancellationToken);
        _logger.JsonLogDebug("Items", vm.Items);
        return vm;
    }
}

