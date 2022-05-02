using Application.Common.Interfaces;
using MediatR;
using Shared.Application.Extensions;
using Shared.Domain.Models;

namespace Application.Dictionary.District.Commands.UpsertDistrict;

public class UpsertDistrictCommand : IRequest
{
    /// <summary>
    /// District number
    /// </summary>
    public int IdDistrict { get; set; }

    /// <summary>
    /// Name of District
    /// </summary>
    public string Name { get; set; } = null!;

    public class UpsertDistrictCommandHandler : IRequestHandler<UpsertDistrictCommand>
    {
        private readonly ISPADbContext _context;
        private readonly IAppAuditService _audit;

        public UpsertDistrictCommandHandler(
            ISPADbContext context,
            IAppAuditService audit)
        {
            _context = context;
            _audit = audit;
        }

        public async Task<Unit> Handle(UpsertDistrictCommand request, CancellationToken cancellationToken)
        {
            //check access
            Domain.Entities.District? entity;
            //AuditEvent audit;

            entity = await _context.Districts.FindIdAsync(request.IdDistrict, cancellationToken);

            if (entity == null)
            {
                entity = new Domain.Entities.District()
                {
                    IdDistrict = request.IdDistrict,
                    Name = request.Name
                };
                _context.Districts.Add(entity);
                //audit = await _audit.Create(entity, request);
                //audit.TargetName = request.IdDistrict.ToString();
            }
            else
            {
                //audit = await _audit.Edit(entity, request);
                entity.Name = request.Name;
            }

            //entity.Log(audit);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
