using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FluentValidation.Results;

namespace Application.Account.User.Commands.UpsertUser;

/// <summary>
/// Update user information or add new user
/// </summary>
[Authorize(Modules = eAccountModule.SecurityAdmin)]
public class UpsertUserCommand : IRequest<long>
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public long? IdUser { get; set; }

    /// <summary>
    /// Is user active (T = yes, F = no)
    /// </summary>
    /// <example>T</example>
    public string IsActive { get; set; } = null!;

    /// <summary>
    /// Login
    /// </summary>
    /// <example>test17</example>
    public string Login { get; set; } = null!;

    /// <summary>
    /// Password
    /// </summary>
    /// <example>test</example>
    public string? Password { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    /// <example>Test user 17</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// User description
    /// </summary>
    /// <example>Test user #17</example>
    public string? Description { get; set; }

    /// <summary>
    /// Password expiration date
    /// </summary>
    /// <example>2010-01-01T00:00:00</example>
    public DateTime? PassDate { get; set; }

    /// <summary>
    /// Groups (id)
    /// </summary>
    public long[]? Groups { get; set; }

    /// <summary>
    /// Roles (id)
    /// </summary>
    public long[]? Roles { get; set; }

    /// <summary>
    /// Districts (id)
    /// </summary>
    public long[]? Districts { get; set; }
}

public class UpsertUserCommandHandler : IRequestHandler<UpsertUserCommand, long>
{
    private readonly ISPADbContext _context;
    private readonly ILogger _logger;
    private IAuditBuilder _audit => _context.AuditBuilder;

    public UpsertUserCommandHandler(
        ISPADbContext context,
        ILogger<UpsertUserCommand> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<long> Handle(UpsertUserCommand request, CancellationToken cancellationToken)
    {
        //already have roles declared on api controller
        //no further restrictions necessary

        _logger.JsonLogDebug("Request", request);

        Domain.Entities.User? entity;
        Audit audit;

        if (request.IdUser.HasValue)
        {
            entity = await _context.Users
                .Include(p => p.UserGroups)
                .Include(p => p.UserRoles)
                .Include(p => p.UserDistricts)
                .GetAsync(p => p.IdUser == request.IdUser.Value, cancellationToken);

            audit = await _audit.EditAsync(entity, request);
        }
        else
        {
            entity = new Domain.Entities.User();

            _context.Users.Add(entity);

            audit = await _audit.CreateAsync(entity, request);
            audit.TargetName = request.Login;
        }

        entity.Login = request.Login;
        entity.IsActive = request.IsActive;
        entity.Name = request.Name;
        entity.PassDate = request.PassDate;
        entity.Description = request.Description;
        if (!string.IsNullOrEmpty(request.Password))
            entity.Pass = request.Password.MD5Hash() ?? String.Empty;

        //UserGroups
        ListHelper.Process(
            request.IdUser,
            request.Groups,
            entity.UserGroups.Select(p => p.IdGroup),
            _context.UserGroups,
            a => new UserGroup() { IdUserNavigation = entity, IdGroup = a },
            r => (p => p.IdUser == entity.IdUser && p.IdGroup == r),
            _audit,
            ref audit,
            nameof(entity.UserGroups),
            await _context.Groups.ToDictionaryAsync(p => p.IdGroup, v => v.Name, cancellationToken));

        //UserRoles
        ListHelper.Process(
            request.IdUser,
            request.Roles,
            entity.UserRoles.Select(p => p.IdRole),
            _context.UserRoles,
            a => new UserRole() { IdUserNavigation = entity, IdRole = a },
            r => (p => p.IdUser == entity.IdUser && p.IdRole == r),
            _audit,
            ref audit,
            nameof(entity.UserRoles),
            await _context.Roles.ToDictionaryAsync(p => p.IdRole, v => v.Name, cancellationToken));

        //UserDistricts
        ListHelper.Process(
            request.IdUser,
            request.Districts,
            entity.UserDistricts.Select(p => Convert.ToInt64(p.IdDistrict)),
            _context.UserDistricts,
            a => new UserDistrict() { IdUserNavigation = entity, IdDistrict = (int)a },
            r => (p => p.IdUser == entity.IdUser && p.IdDistrict == r),
            _audit,
            ref audit,
            nameof(entity.UserDistricts),
            await _context.Districts.ToDictionaryAsync(p => Convert.ToInt64(p.IdDistrict), v => v.IdDistrict.ToString(), cancellationToken));

        entity.Log(audit);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.IdUser;
    }
}
