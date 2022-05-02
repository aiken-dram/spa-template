using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Shared.Application.Extensions;
using Shared.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Account.User.Queries.GetUserTable;

/// <summary>
/// Get data for displaying user table
/// </summary>
public class GetUserTableQuery : TableQuery, IRequest<UserTableVm>
{
    /// <summary>
    /// List of search filters as strings with format "{fieldName}|{operation}|{value}"
    /// </summary>
    public IList<string>? Search { get; set; }

    /// <summary>
    /// Search string in table
    /// </summary>
    public string? FullSearch { get; set; }

    public class GetUserTableQueryHandler : IRequestHandler<GetUserTableQuery, UserTableVm>
    {
        private readonly ISPADbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetUserTableQueryHandler(
            ISPADbContext context,
            IMapper mapper,
            ILogger<GetUserTableQuery> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserTableVm> Handle(GetUserTableQuery request, CancellationToken cancellationToken)
        {
            //check access
            _logger.JsonLogDebug("request", request);

            //prepare filters
            var filterAfter = TableFilter.ToFilterList(request.Search)
                .Concat(TableFilter.ToFilterList(request.Filters));

            var query = _context.Users
               .Include(p => p.UserGroups)
               .ProjectTo<UserTableDto>(_mapper.ConfigurationProvider)
               .Filters(filterAfter);

            //fulltext search (ish)
            if (!string.IsNullOrEmpty(request.FullSearch))
                query = query.Where(p =>
                    EF.Functions.Like(p.login, $"%{request.FullSearch}%") ||
                    EF.Functions.Like(p.name, $"%{request.FullSearch}%") ||
                    EF.Functions.Like(p.description ?? String.Empty, $"%{request.FullSearch}%"));

            var vm = new UserTableVm();
            vm.Items = await query.TableQuery(request, "idUser").ToListAsync(cancellationToken);
            vm.Total = await query.CountAsync(cancellationToken);
            return vm;
        }
    }
}
