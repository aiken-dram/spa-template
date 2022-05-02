using Domain.Entities;
using Domain.Enums;
using Shared.Domain.Models;

namespace Infrastructure.Identity;

public class AuthAudit
{
    public static AuditEvent Auth(int idAction, User user, string? system)
    => new AuditEvent((int)eEventTarget.Auth, idAction, user.AuditTargetId, user.AuditTargetName, system)
    {
        IdUser = user.IdUser,
    };

    public static AuditEvent Expired(User entity, string? system)
    => Auth((int)eAuthEventAction.Expired, entity, system);

    public static AuditEvent WrongPassword(User entity, string? system)
    => Auth((int)eAuthEventAction.WrongPassword, entity, system);

    public static AuditEvent Lock(User entity, string? system)
    => Auth((int)eAuthEventAction.Lock, entity, system);

    public static AuditEvent Login(User entity, string? system)
    => Auth((int)eAuthEventAction.Login, entity, system);
}
