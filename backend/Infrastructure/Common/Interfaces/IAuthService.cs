namespace Infrastructure.Common.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Authorize user
    /// </summary>
    /// <param name="request">Authorization request</param>
    /// <param name="System">Request system</param>
    /// <returns>Information about authorized user</returns>
    Task<AuthResponse> LoginAsync(AuthRequest request, string? System);

    /// <summary>
    /// Return information about authorized user
    /// </summary>
    /// <param name="userId">Id of user in database</param>
    /// <returns>Information about authorized user</returns>
    Task<AuthResponse> CurrentUserAsync(string userId);

    /// <summary>
    /// Validate user with provided user id
    /// </summary>
    /// <param name="userId">Id of user in database</param>
    Task Validate(string userId);
}
