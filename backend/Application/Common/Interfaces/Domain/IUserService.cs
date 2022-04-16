using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Id of current user
    /// </summary>
    long CurrentUserId { get; }

    /// <summary>
    /// Returns current user data
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    Task<CurrentUser> GetCurrentUserAsync(CancellationToken cancellationToken);
}
