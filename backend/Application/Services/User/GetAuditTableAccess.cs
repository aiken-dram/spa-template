
namespace Application.Services;

public partial class UserService
{
    public async Task GetAuditTableAccess(long? IdUser, long? IdSample, CancellationToken cancellationToken)
    {
        var curr = await GetCurrentUserAsync(cancellationToken);
        _logger.JsonLogDebug("current user", curr);

        if (IdUser.HasValue && !curr.Modules.Contains(eAccountModule.SecurityAdmin) && curr.IdUser != IdUser.Value)
            throw new AccessDeniedException(Messages.NotMatchingUserId);

        if (!IdUser.HasValue && !curr.Modules.Contains(eAccountModule.SecurityAdmin))
            throw new AccessDeniedException(Messages.NoAccessToAuditUsers);

        //2D: sample? nanikore
    }
}
