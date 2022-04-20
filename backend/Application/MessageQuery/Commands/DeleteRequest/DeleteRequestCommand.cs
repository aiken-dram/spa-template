using Application.Common;
using Application.Common.Interfaces;
using Shared.Application.Exceptions;
using Shared.Application.Extensions;
using Domain.Enums;
using MediatR;

namespace Application.MessageQuery.Commands.DeleteRequest;

public class DeleteRequestCommand : IRequest
{
    /// <summary>
    /// Id of request in database
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; }

    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand>
    {
        private readonly ISPADbContext _context;
        private readonly IUserService _user;

        public DeleteRequestCommandHandler(
            ISPADbContext context,
            IUserService user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Unit> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            //check access                
            var entity = await _context.Requests.FindIdAsync(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(nameof(Domain.Entities.Request), request.Id);

            //only supervisors or users who created request can delete it
            var user = await _user.GetCurrentUserAsync(cancellationToken);
            if (!user.Modules.Contains(eAccountModule.Supervise))
                if (entity.IdUser != user.IdUser)
                    throw new AccessDeniedException(Messages.NoAccessToDeleteRequest);

            //delete request
            _context.Requests.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
