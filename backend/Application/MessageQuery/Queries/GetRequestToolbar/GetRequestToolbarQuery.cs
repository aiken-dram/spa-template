using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.MessageQuery.Queries.GetRequestToolbar;

/// <summary>
/// Get MQ Request information for current user to display in toolbar
/// </summary>
public class GetRequestToolbarQuery : IRequest<RequestToolbarVm>
{ }

public class GetRequestToolbarQueryHandler : IRequestHandler<GetRequestToolbarQuery, RequestToolbarVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _user;

    public GetRequestToolbarQueryHandler(
        ISPADbContext context,
        IMapper mapper,
        IUserService user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<RequestToolbarVm> Handle(GetRequestToolbarQuery request, CancellationToken cancellationToken)
    {
        //check access
        var uid = _user.CurrentUserId;

        var query = _context.Requests
            .Where(p => p.IdUser == uid);

        var vm = new RequestToolbarVm();

        //last 5 created items that were not delivered
        vm.Items = await query
            .Where(p => p.IdState != eRequestState.Delivered)
            .ProjectTo<RequestToolbarDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(p => p.idRequest)
            .Take(5)
            .TagWith("TABLE FIRST PAGE N5ROWS") //this is fix for old DB2 version
            .ToListAsync(cancellationToken);

        //total amount of not delivered requests
        vm.CountTotal = await query
            .Where(p => p.IdState != eRequestState.Delivered)
            .CountAsync(cancellationToken);

        //amount of requests ready to be delivered
        vm.CountReady = await query
            .Where(p => p.IdState == eRequestState.Ready)
            .CountAsync(cancellationToken);

        return vm;
    }
}

