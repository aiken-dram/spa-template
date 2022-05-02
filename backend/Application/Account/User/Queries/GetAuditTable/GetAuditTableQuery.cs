using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shared.Application.Exceptions;
using Shared.Application.Extensions;
using Shared.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;
using Application.Common;
using Microsoft.Extensions.Logging;

namespace Application.Account.User.Queries.GetAuditTable;

/// <summary>
/// Table of user events for auditing user activity
/// </summary>
public class GetAuditTableQuery : TableQuery, IRequest<AuditTableVm>
{
    /// <summary>
    /// Identity of user in database
    /// </summary>
    /// <example>1</example>
    public long? Id { get; set; }

    public class GetAuditTableQueryHandler : IRequestHandler<GetAuditTableQuery, AuditTableVm>
    {
        private readonly ISPADbContext _context;
        private readonly IUserService _user;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetAuditTableQueryHandler(
            ISPADbContext context,
            IUserService user,
            IMapper mapper,
            ILogger<GetAuditTableQuery> logger)
        {
            _context = context;
            _user = user;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuditTableVm> Handle(GetAuditTableQuery request, CancellationToken cancellationToken)
        {
            //change into full access and resrict for everyone not admin here to view other that themselves
            //check access
            var curr = await _user.GetCurrentUserAsync(cancellationToken);
            _logger.JsonLogDebug("current user", curr);

            if (request.Id.HasValue && !curr.Modules.Contains(eAccountModule.SecurityAdmin) && curr.IdUser != request.Id.Value)
                throw new AccessDeniedException(Messages.NotMatchingUserId);

            if (!request.Id.HasValue && !curr.Modules.Contains(eAccountModule.SecurityAdmin))
                throw new AccessDeniedException(Messages.NoAccessToAuditUsers);
            //access checked

            if (request.Id.HasValue)
            {
                var user = await _context.Users.FindIdAsync(request.Id, cancellationToken);

                if (user == null)
                    throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
            }

            //prepare filters
            var filterAfter = TableFilter.ToFilterList(request.Filters);

            IQueryable<UserEvent> _query = _context.UserEvents.Include(p => p.UserEventData);

            if (request.Id.HasValue)
                _query = _query.Where(p => p.IdUser == request.Id.Value);

            var query = _query
                .ProjectTo<AuditTableDto>(_mapper.ConfigurationProvider)
                .Filters(filterAfter);

            var vm = new AuditTableVm();
            vm.Items = await query.TableQuery(request, "stamp", true).ToListAsync(cancellationToken);
            vm.Total = await query.CountAsync(cancellationToken);
            return vm;
        }
    }
}
