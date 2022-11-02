using AutoMapper;

namespace Application.Account.User.Queries.GetCurrentUser;

/// <summary>
/// Get currently authenticated user information
/// </summary>
public class GetCurrentUserQuery : IRequest<CurrentUserVm>
{ }

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _user;

    public GetCurrentUserQueryHandler(
        ISPADbContext context,
        IMapper mapper,
        IUserService user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<CurrentUserVm> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var uid = _user.CurrentUserId;
        var entity = await _context.Users.GetAsync(uid, cancellationToken);

        return _mapper.Map<CurrentUserVm>(entity);
    }
}
