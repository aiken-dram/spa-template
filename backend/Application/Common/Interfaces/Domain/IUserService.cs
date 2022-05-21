namespace Application.Common.Interfaces;

/// <summary>
/// Service for retrieving current user information
/// </summary>
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
