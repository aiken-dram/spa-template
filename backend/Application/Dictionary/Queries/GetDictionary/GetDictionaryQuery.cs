using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionary.Queries.GetDictionary;

public class GetDictionaryQuery : IRequest<IList<DictionaryDto>>
{
    /// <summary>
    /// Dictionary name
    /// List of available dictionaries:
    ///
    ///     Districts
    ///     UserDistricts
    ///     AccessGroups
    ///     AccessRoles
    ///     AuditActions
    ///     AuditTargets
    ///     RScriptParamTypes
    /// 
    ///     SampleTypes
    ///     SampleDicts
    /// 
    /// </summary>
    /// <example>Districts</example>
    public string? Dictionary { get; set; }
}

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
            //list of all districts in dictionary
            case "Districts":
                vm = await _context.Districts
                    .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(p => p.Value)
                    .ToListAsync(cancellationToken);
                return vm;

            //list of districts available for current user in dictionary
            case "UserDistricts":
                long uid = _user.CurrentUserId;
                if (await _context.UserDistricts.CountAsync(p => p.IdUser == uid) > 0)
                {
                    //user districts have been defined in link table
                    var query = from r in _context.Districts
                                join u in _context.UserDistricts.Where(p => p.IdUser == uid)
                                on r.IdDistrict equals u.IdDistrict
                                select r;
                    vm = await query
                        .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                        .OrderBy(p => p.Value)
                        .ToListAsync(cancellationToken);
                }
                else
                {
                    //no user districts have been defined, return all districts by default then
                    vm = await _context.Districts
                        .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                        .OrderBy(p => p.Value)
                        .ToListAsync(cancellationToken);
                }
                return vm;

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

            //list of audit actions
            case "AuditActions":
                vm = await _context.AuditActions
                    .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(p => p.Value)
                    .ToListAsync(cancellationToken);
                return vm;

            //list of audit targets
            case "AuditTargets":
                vm = await _context.AuditTargets
                    .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(p => p.Value)
                    .ToListAsync(cancellationToken);
                return vm;

            case "RScriptParamTypes":
                vm = await _context.RScriptParamTypes
                    .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(p => p.Value)
                    .ToListAsync(cancellationToken);
                return vm;

#warning SAMPLE, remove case in actual application
            // sample types
            case "SampleTypes":
                vm = await _context.SampleTypes
                    .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(p => p.Value)
                    .ToListAsync(cancellationToken);
                return vm;

#warning SAMPLE, remove case in actual application
            // sample dictionary
            case "SampleDicts":
                vm = await _context.SampleDicts
                    .ProjectTo<DictionaryDto>(_mapper.ConfigurationProvider)
                    .OrderBy(p => p.Value)
                    .ToListAsync(cancellationToken);
                return vm;

            default:
                throw new NotFoundException(Messages.DictionaryNotFound(request.Dictionary ?? string.Empty));
        }
    }
}

