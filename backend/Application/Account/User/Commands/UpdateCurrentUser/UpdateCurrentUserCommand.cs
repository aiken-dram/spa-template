using MediatR;
using Shared.Application.Exceptions;
using Shared.Application.Extensions;
using Application.Common.Interfaces;
using Application.Common;

namespace Application.Account.User.Commands.UpdateCurrentUser;

/// <summary>
/// Update information about current user
/// </summary>
public class UpdateCurrentUserCommand : IRequest
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public long IdUser { get; set; }

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

    public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand>
    {
        private readonly ISPADbContext _context;
        private readonly IAppAuditService _audit;
        private readonly IUserService _user;

        public UpdateCurrentUserCommandHandler(
            ISPADbContext context,
            IAppAuditService audit,
            IUserService user)
        {
            _context = context;
            _audit = audit;
            _user = user;
        }

        public async Task<Unit> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
        {
            //check if id is same as current user
            var uid = _user.CurrentUserId;
            if (uid != request.IdUser)
                throw new AccessDeniedException(Messages.NotMatchingUserId);
            //access checked

            var entity = await _context.Users.FindIdAsync(request.IdUser, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Domain.Entities.User), request.IdUser);

            //audit
            var audit = await _audit.Edit(entity, request, null);

            entity.Name = request.Name;
            entity.Description = request.Description;

            entity.Log(audit);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
