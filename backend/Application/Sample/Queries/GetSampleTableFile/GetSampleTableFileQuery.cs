using Application.Sample.Queries.GetSampleTable;

namespace Application.Sample.Queries.GetSampleTableFile;

#warning SAMPLE, remove entire file in actual application
public class GetSampleTableFileQuery : TableQuery, IRequest<SampleTableFileVm>
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

public class GetSampleTableFileQueryHandler : IRequestHandler<GetSampleTableFileQuery, SampleTableFileVm>
{
    private readonly ISPADbContext _context;
    private readonly IMediator _mediator;
    private readonly IFileService _file;

    public GetSampleTableFileQueryHandler(
        ISPADbContext context,
        IMediator mediator,
        IFileService file)
    {
        _context = context;
        _mediator = mediator;
        _file = file;
    }

    public async Task<SampleTableFileVm> Handle(GetSampleTableFileQuery request, CancellationToken cancellationToken)
    {
        //check access

        var query = new GetSampleTableQuery()
        {
            Page = request.Page,
            ItemsPerPage = request.ItemsPerPage,
            SortBy = request.SortBy,
            SortDesc = request.SortDesc,
            Filters = request.Filters,
            Search = request.Search,
            Extended = request.Extended,
        };

        var res = await _mediator.Send(query, cancellationToken);
        var fileContent = await _file.BuildSampleTableFileAsync(res.Items, cancellationToken);

        var vm = new SampleTableFileVm
        {
            Content = fileContent,
            ContentType = FileContentType.CSV,
            FileName = Messages.SampleTableFileName(DateTime.Now)
        };

        return vm;
    }
}