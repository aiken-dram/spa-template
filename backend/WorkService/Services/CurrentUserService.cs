using Application.Common.Interfaces;

namespace WorkService.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IConfiguration _configuration;

    public CurrentUserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? UserId => _configuration["ServiceUserId"];
}
