using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public partial class UserService : IUserService
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ICurrentUserService _user;

    public UserService(
        ISPADbContext context,
        IMapper mapper,
        ILogger<UserService> logger,
        ICurrentUserService user)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
        _user = user;
    }
}
