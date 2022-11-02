using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Sample.Queries.GetSample;

#warning SAMPLE, remove entire file in actual application
public class GetSampleQuery : IRequest<SampleVm>
{
    /// <summary>
    /// Id of sample in database
    /// </summary>
    public long Id { get; set; }
}

public class GetSampleQueryHandler : IRequestHandler<GetSampleQuery, SampleVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetSampleQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SampleVm> Handle(GetSampleQuery request, CancellationToken cancellationToken)
    {
        //check access

        var entity = await _context.Samples
            .Include(p => p.SampleChildren)
            .GetAsync(p => p.IdSample == request.Id, cancellationToken);

        var vm = _mapper.Map<SampleVm>(entity);

        return vm;
    }
}