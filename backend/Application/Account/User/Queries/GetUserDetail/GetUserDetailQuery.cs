using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Exceptions;
using Application.Common.Interfaces;

namespace Application.Account.User.Queries.GetUserDetail;

/// <summary>
/// Get user detail by provided Id
/// </summary>
public class GetUserDetailQuery : IRequest<UserDetailVm>
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }

    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailVm>
    {
        private readonly ISPADbContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(
            ISPADbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDetailVm> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            //already have roles declared on api controller
            //no further restrictions necessary

            var entity = await _context.Users
                .Include(p => p.UserGroups).Include(p => p.UserRoles)
                .Where(e => e.IdUser == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Domain.Entities.User), request.Id);

            var vm = _mapper.Map<UserDetailVm>(entity);
            return vm;
        }
    }
}
