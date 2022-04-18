using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Request.Queries.GetProcessedRequestCount;

public class GetProcessedRequestCountQuery : IRequest<int>
{
    public class GetProcessedRequestCountQueryHandler : IRequestHandler<GetProcessedRequestCountQuery, int>
    {
        private readonly ISPADbContext _context;
        private readonly IUserService _user;

        public GetProcessedRequestCountQueryHandler(
            ISPADbContext context,
            IUserService user)
        {
            _context = context;
            _user = user;
        }

        public async Task<int> Handle(GetProcessedRequestCountQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Requests
                .Where(d => d.IdUser == _user.CurrentUserId && d.IdStateNavigation.State == eRequestState.Ready)
                .Select(p => p.IdRequest);

            var res = await query.CountAsync(cancellationToken);

            return res;
        }
    }
}
