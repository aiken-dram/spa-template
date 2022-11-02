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

    #region ACCESS
    /// <summary>
    /// Check access to retrieving Audit table
    /// </summary>
    /// <param name="IdUser">Id of sample</param>
    /// <param name="IdSample">Id of sample</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task GetAuditTableAccess(long? IdUser, long? IdSample, CancellationToken cancellationToken);
    #endregion
}
