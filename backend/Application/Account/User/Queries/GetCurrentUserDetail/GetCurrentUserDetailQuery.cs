using AutoMapper;
using MediatR;
using Shared.Application.Exceptions;
using Application.Common.Interfaces;

namespace Application.Account.User.Queries.GetCurrentUserDetail;

/// <summary>
/// Get currently authenticated user detailed information
/// </summary>
public class GetCurrentUserDetailQuery : IRequest<CurrentUserDetailVm>
{
    public class GetCurrentUserDetailQueryHandler : IRequestHandler<GetCurrentUserDetailQuery, CurrentUserDetailVm>
    {
        private readonly ISPADbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _user;

        public GetCurrentUserDetailQueryHandler(
            ISPADbContext context,
            IMapper mapper,
            IUserService user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<CurrentUserDetailVm> Handle(GetCurrentUserDetailQuery request, CancellationToken cancellationToken)
        {
            var uid = _user.CurrentUserId;
            var entity = await _context.Users
                .FindAsync(uid);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.User), uid);
            }

            return _mapper.Map<CurrentUserDetailVm>(entity);
        }
    }
}
