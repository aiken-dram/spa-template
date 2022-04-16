using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shared.Application.Exceptions;
using Shared.Application.Extensions;
using Shared.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.User.Queries.GetUserAuthTable;

public class GetUserAuthTableQuery : TableQuery, IRequest<UserAuthTableVm>
{
    /// <summary>
    /// Identity of user in database
    /// </summary>
    /// <example>1</example>
    public long Id { get; set; }

    public class GetUserAuthTableQueryHandler : IRequestHandler<GetUserAuthTableQuery, UserAuthTableVm>
    {
        private readonly ISPADbContext _context;
        private readonly IMapper _mapper;

        public GetUserAuthTableQueryHandler(
            ISPADbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserAuthTableVm> Handle(GetUserAuthTableQuery request, CancellationToken cancellationToken)
        {
            //access checked in controller
            //no further restrictions necessary

            var user = await _context.Users.FindIdAsync(request.Id, cancellationToken);

            if (user == null)
                throw new NotFoundException(nameof(Domain.Entities.User), request.Id);

            //prepare filters
            var filterAfter = TableFilter.ToFilterList(request.Filters);

            var query = _context.UserAuth
                .Where(p => p.IdUser == request.Id)
                .ProjectTo<UserAuthTableDto>(_mapper.ConfigurationProvider)
                .Filters(filterAfter);

            var vm = new UserAuthTableVm();
            vm.Items = await query.TableQuery(request, "stamp", true).ToListAsync(cancellationToken);
            vm.Total = await query.CountAsync(cancellationToken);
            return vm;
        }
    }
}
