using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Shared.Application.Exceptions;
using Shared.Application.Extensions;

namespace Application.Dictionary.District.Queries.GetDistrict;

public class GetDistrictQuery : IRequest<DistrictVm>
{
    /// <summary>
    /// Id of District in dictionary
    /// </summary>
    public int Id { get; set; }

    public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, DistrictVm>
    {
        private readonly ISPADbContext _context;
        private readonly IMapper _mapper;

        public GetDistrictQueryHandler(
            ISPADbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DistrictVm> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
        {
            //check access
            var entity = await _context.Districts.FindIdAsync(request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Domain.Entities.District), request.Id);

            var vm = _mapper.Map<DistrictVm>(entity);

            return vm;
        }
    }
}
