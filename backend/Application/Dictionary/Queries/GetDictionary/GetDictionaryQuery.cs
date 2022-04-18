using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Exceptions;
using Application.Common.Interfaces;
using Application.Common;

namespace Application.Dictionary.Queries.GetDictionary;

public class GetDictionaryQuery : IRequest<IList<DictionaryDto>>
{
    /// <summary>
    /// Dictionary name
    /// List of available dictionaries:
    ///
    ///     AccessGroups
    ///     AccessRoles
    ///     AuthActions
    /// 
    /// </summary>
    /// <example>Raions</example>
    public string Dictionary { get; set; } = null!;

    public class GetDictionaryQueryHandler : IRequestHandler<GetDictionaryQuery, IList<DictionaryDto>>
    {
        private readonly ISPADbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _user;

        public GetDictionaryQueryHandler(
            ISPADbContext context,
            IMapper mapper,
            IUserService user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<IList<DictionaryDto>> Handle(GetDictionaryQuery request, CancellationToken cancellationToken)
        {
            //any user can get dictionaries

            List<DictionaryDto> vm;
            switch (request.Dictionary)
            {
                // list of access groups
                case "AccessGroups":
                    vm = await _context.Groups
                        .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                        .OrderBy(p => p.Value)
                        .ToListAsync(cancellationToken);
                    return vm;

                //list of access roles
                case "AccessRoles":
                    vm = await _context.Roles
                        .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                        .OrderBy(p => p.Value)
                        .ToListAsync(cancellationToken);
                    return vm;

                //list of auth actions
                case "AuthActions":
                    vm = await _context.AuthActions
                        .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                        .OrderBy(p => p.Value)
                        .ToListAsync(cancellationToken);
                    return vm;

                default:
                    throw new NotFoundException(Messages.DictionaryNotFound(request.Dictionary));
            }
        }
    }
}
