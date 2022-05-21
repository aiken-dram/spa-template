namespace Application.Common.Interfaces;

/// <summary>
/// Service for custom audit in application
/// </summary>
public interface IAuditService
{
    /// <summary>
    /// Audit for updating user's password from file
    /// </summary>
    /// <param name="entity">User entity</param>
    /// <returns>Audit</returns>
    Task<Audit> UserUpdatePassword(Domain.Entities.User entity);
}
