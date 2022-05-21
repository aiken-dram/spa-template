using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.User.Queries.GetUser;

/// <summary>
/// Get user by provided Id
/// </summary>
[Authorize(Modules = eAccountModule.SecurityAdmin)]
public class GetUserQuery : IRequest<UserVm>
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        //already have roles declared on api controller
        //no further restrictions necessary

        var entity = await _context.Users
            .Include(p => p.UserGroups).Include(p => p.UserRoles).Include(p => p.UserDistricts)
            .Where(e => e.IdUser == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Domain.Entities.User), request.Id);

        var vm = _mapper.Map<UserVm>(entity);
        return vm;
    }
}
