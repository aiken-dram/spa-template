using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionary.SampleDict.Queries.GetSampleDictList;

#warning SAMPLE, remove entire file in actual application
/// <summary>
/// Get list of sample dictionary
/// </summary>
public class GetSampleDictListQuery : IRequest<SampleDictListVm>
{ }

public class GetSampleDictListQueryHandler : IRequestHandler<GetSampleDictListQuery, SampleDictListVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetSampleDictListQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SampleDictListVm> Handle(GetSampleDictListQuery request, CancellationToken cancellationToken)
    {
        //check access
        //any user can get sample dictionary list

        var items = await _context.SampleDicts
            .ProjectTo<SampleDictListDto>(_mapper.ConfigurationProvider)
            .OrderBy(p => p.idDict)
            .ToListAsync(cancellationToken);

        var vm = new SampleDictListVm { Items = items };
        return vm;
    }
}