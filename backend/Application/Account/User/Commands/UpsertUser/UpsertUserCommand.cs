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

    /// <summary>
    /// Processing list for many-to-many relations table
    /// </summary>
    /// <remarks>
    /// Hmm not sure where to put this function, helper?
    /// </remarks>
    /// <param name="requestId">Id of user (can be null for new)</param>
    /// <param name="list">list of elements in request</param>
    /// <param name="dbset">DbSet of list elements</param>
    /// <param name="whereExisting">expression to select elements for id</param>
    /// <param name="selectExisting">expression to select ids</param>
    /// <param name="newEntity">expression to create new list element</param>
    /// <param name="delEntity">expression to delete elements for id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <typeparam name="Tentity">type of entity</typeparam>
    /// <returns>void</returns>
    private async Task ProcessList<Tentity>(long? requestId, long[]? list, DbSet<Tentity> dbset, Expression<Func<Tentity, bool>> whereExisting, Expression<Func<Tentity, long>> selectExisting, Expression<Func<long, Tentity>> newEntity, Expression<Func<long, Expression<Func<Tentity, bool>>>> delEntity, CancellationToken cancellationToken)
        where Tentity : class, new()
    {
        var existing = await dbset.Where(whereExisting).Select(selectExisting).ToListAsync(cancellationToken);
        var _add = ListHelper.AddList(requestId, list, existing);
        var _remove = ListHelper.RemoveList(requestId, list, existing);

        if (_add != null)
            foreach (var a in _add)
            {
                var e = (newEntity.Compile())(a);
                dbset.Add(e);
            }

        if (_remove != null)
            foreach (var r in _remove)
            {
                var pred = (delEntity.Compile())(r);
                dbset.RemoveRange(dbset.Where(pred));
            }

        await _context.SaveChangesAsync(cancellationToken);
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
            entity = await _context.Users.FindIdAsync(request.IdUser.Value, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Domain.Entities.User), request.IdUser.Value);

            audit = await _audit.Edit(entity, request);
        }
        else
        {
            entity = new Domain.Entities.User();

            _context.Users.Add(entity);

            audit = await _audit.Create(entity, request);
            audit.TargetName = request.Login;
        }

        //check if same login already exists
        var loginDuplicates = await _context.Users.CountAsync(p => p.Login == request.Login && p.IdUser != request.IdUser);
        if (loginDuplicates > 0)
        {
            var ve = new List<ValidationFailure>();
            ve.Add(new ValidationFailure("Login", Messages.UserWithProvidedLoginAlreadyExists));
            throw new ValidationException(ve);
        }

        entity.Login = request.Login;
        entity.IsActive = request.IsActive;
        entity.Name = request.Name;
        entity.PassDate = request.PassDate;
        entity.Description = request.Description;
        if (!string.IsNullOrEmpty(request.Password))
            entity.Pass = EncryptorHelper.MD5Hash(request.Password) ?? String.Empty;

        entity.Log(audit);
        await _context.SaveChangesAsync(cancellationToken);

        //well since i already wrote the next step in this way, auditing it might be difficult
        //rewrite into single SaveChangesAsync, if possible?
        //why wasnt i working with entity navigation properties in the first place?

        //UserGroups
        await ProcessList(
            request.IdUser,
            request.Groups,
            _context.UserGroups,
            p => p.IdUser == entity.IdUser,
            q => q.IdGroup,
            a => new UserGroup() { IdUser = entity.IdUser, IdGroup = a },
            r => (p => p.IdUser == entity.IdUser && p.IdGroup == r),
            cancellationToken);

        //UserRoles
        await ProcessList(
            request.IdUser,
            request.Roles,
            _context.UserRoles,
            p => p.IdUser == entity.IdUser,
            q => q.IdRole,
            a => new UserRole() { IdUser = entity.IdUser, IdRole = a },
            r => (p => p.IdUser == entity.IdUser && p.IdRole == r),
            cancellationToken);

        //UserDistricts
            await ProcessList(
                request.IdUser,
                request.Districts,
                _context.UserDistricts,
                p => p.IdUser == entity.IdUser,
                q => q.IdDistrict,
                a => new UserDistrict() { IdUser = entity.IdUser, IdDistrict = (int)a },
                r => (p => p.IdUser == entity.IdUser && p.IdDistrict == r),
                cancellationToken);

        return entity.IdUser;
    }
}
