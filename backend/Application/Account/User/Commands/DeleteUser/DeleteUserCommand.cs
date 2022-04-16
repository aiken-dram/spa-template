using Application.Common.Interfaces;
using Shared.Application.Exceptions;
using MediatR;

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

        public DeleteUserCommandHandler(ISPADbContext context)
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

            _context.Users.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
