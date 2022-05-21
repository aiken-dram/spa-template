using Domain.Entities;
using Domain.Enums;
using Shared.Domain.Models;

namespace Infrastructure.Identity;

/// <summary>
/// Audit for authorization
/// </summary>
public static class AuthAudit
{
    /// <summary>
    /// Create Audit for authorization
    /// </summary>
    /// <param name="idAction">Id of audit action</param>
    /// <param name="user">User entity</param>
    /// <param name="system">Name of system</param>
    /// <returns>Audit</returns>
    public static Audit Auth(int idAction, User user, string? system)
    => new Audit((int)eAuditTarget.Auth, idAction, user.AuditTargetId, user.AuditTargetName, system)
    {
        IdUser = user.IdUser,
    };

    /// <summary>
    /// Audit for password expired
    /// </summary>
    /// <param name="entity">User entity</param>
    /// <param name="system">Name of system</param>
    /// <returns>Audit</returns>
    public static Audit Expired(User entity, string? system)
    => Auth((int)eAuthAuditAction.Expired, entity, system);

    /// <summary>
    /// Audit for wrong password
    /// </summary>
    /// <param name="entity">User entity</param>
    /// <param name="system">Name of system</param>
    /// <returns>Audit</returns>
    public static Audit WrongPassword(User entity, string? system)
    => Auth((int)eAuthAuditAction.WrongPassword, entity, system);

    /// <summary>
    /// Audit for locking user
    /// </summary>
    /// <param name="entity">User entity</param>
    /// <param name="system">Name of system</param>
    /// <returns>Audit</returns>
    public static Audit Lock(User entity, string? system)
    => Auth((int)eAuthAuditAction.Lock, entity, system);

    /// <summary>
    /// Audit for successfull authorization
    /// </summary>
    /// <param name="entity">User entity</param>
    /// <param name="system">Name of system</param>
    /// <returns>Audit</returns>
    public static Audit Login(User entity, string? system)
    => Auth((int)eAuthAuditAction.Login, entity, system);
}
