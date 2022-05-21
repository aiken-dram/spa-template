namespace Application.Account.User.Commands.DeleteUser;

/// <summary>
/// Delete user
/// </summary>
[Authorize(Modules = eAccountModule.SecurityAdmin)]
public class DeleteUserCommand : IRequest
{
    /// <summary>
    /// User identity in database
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly ISPADbContext _context;
    private IAuditBuilder _audit => _context.AuditBuilder;

    public DeleteUserCommandHandler(
        ISPADbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        //already have roles declared on api controller
        //no further restrictions necessary

        var entity = await _context.Users
            .FindAsync(request.Id);

        if (entity == null)
            throw new NotFoundException(nameof(Domain.Entities.User), request.Id);

        //add delete to audit log
        entity.Log(_audit.Delete(entity));

        _context.Users.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

