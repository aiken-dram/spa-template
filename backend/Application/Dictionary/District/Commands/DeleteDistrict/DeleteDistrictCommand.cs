using Application.Common.Interfaces;
using MediatR;
using Shared.Application.Exceptions;
using Shared.Application.Extensions;

namespace Application.Dictionary.District.Commands.DeleteDistrict;

public class DeleteDistrictCommand : IRequest
{
    /// <summary>
    /// District id in dictionary
    /// </summary>
    public long Id { get; set; }

    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand>
    {
        private readonly ISPADbContext _context;
        private readonly IAppAuditService _audit;

        public DeleteDistrictCommandHandler(
            ISPADbContext context,
            IAppAuditService audit)
        {
            _context = context;
            _audit = audit;
        }

        public async Task<Unit> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            //check access

            var entity = await _context.Districts
                .FindIdAsync(request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Domain.Entities.District), request.Id);

            //entity.Log(_audit.Delete(entity));
            _context.Districts.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
