using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Sample.Queries.GetSampleTable;

#warning SAMPLE, remove entire file in actual application
/// <summary>
/// Get sample table
/// </summary>
public class GetSampleTableQuery : TableQuery, IRequest<SampleTableVm>
{
    /// <summary>
    /// List of archive search filters as strings with format "{fieldName}|{operation}|{value}"
    /// </summary>
    public IList<string>? Search { get; set; }

    /// <summary>
    /// List of archive extended search filters as strings with format "{fieldName}|{operation}|{value}"
    /// </summary>
    public IList<string>? Extended { get; set; }
}

public class GetSampleTableQueryHandler : IRequestHandler<GetSampleTableQuery, SampleTableVm>
{
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;

    public GetSampleTableQueryHandler(
        ISPADbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SampleTableVm> Handle(GetSampleTableQuery request, CancellationToken cancellationToken)
    {
        //check access

        //prepare filters
        var filterAfter = TableFilter.ToFilterList(request.Search)
            .Concat(TableFilter.ToFilterList(request.Filters));
        var _filterBefore = TableFilter.ToFilterList(request.Extended);

        //need to split fBefore into children
        var (filterBeforeChildren, filterBefore) = _filterBefore.Segment(p => p.Field.StartsWith("children"));

        var _query =
            from m in _context.Samples
            select m;

        //filter by children filters
        if (filterBeforeChildren != null && filterBeforeChildren.Count() > 0)
        {
            //inner join with children that have filter condition
            var filterPayment = filterBeforeChildren.Select(pf => new TableFilter(pf.Field.Substring("children".Length), pf.Operator, pf.Value));
            _query =
                from o in _query
                join p in _context.SampleChildren
                    .Where(FilterExtension.BuildPredicates<SampleChild>(filterPayment))
                    .Select(p => p.IdSample).Distinct()
                on o.IdSample equals p
                select o;
        }

        var query = _query
            .Filters(filterBefore)
            .ProjectTo<SampleTableDto>(_mapper.ConfigurationProvider)
            .Filters(filterAfter);

        var vm = new SampleTableVm();
        vm.Items = await query.TableQuery(request, "idSample").ToListAsync(cancellationToken);
        vm.Total = await query.CountAsync(cancellationToken);
        return vm;
    }
}
