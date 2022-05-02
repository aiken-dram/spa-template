using Application.Common.Interfaces;
using Shared.Application.Exceptions;
using MediatR;
using Domain.Events;

namespace Application.Account.User.Commands.DeleteUser;

/// <summary>
/// Delete user
/// </summary>
public class DeleteUserCommand : IRequest
{
    /// <summary>
    /// User identity in database
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly ISPADbContext _context;
        private readonly IAppAuditService _audit;

        public DeleteUserCommandHandler(
            ISPADbContext context, 
            IAppAuditService audit)
        {
            _context = context;
            _audit = audit;
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

            //add delete to domain events
            entity.DomainEvents.Add(new UserDeletedEvent());

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
