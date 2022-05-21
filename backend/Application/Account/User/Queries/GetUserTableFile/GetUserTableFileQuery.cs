using Microsoft.Extensions.Logging;
using Application.Account.User.Queries.GetUserTable;

namespace Application.Account.User.Queries.GetUserTableFile;

/// <summary>
/// Get csv file with list of users
/// </summary>
[Authorize(Modules = eAccountModule.SecurityAdmin)]
public class GetUserTableFileQuery : TableQuery, IRequest<UserTableFileVm>
{
    /// <summary>
    /// List of search filters as strings with format "{fieldName}|{operation}|{value}"
    /// </summary>
    public IList<string>? Search { get; set; }
}

public class GetUserTableFileQueryHandler : IRequestHandler<GetUserTableFileQuery, UserTableFileVm>
{
    private readonly ILogger _logger;
    private readonly IFileService _file;
    private readonly IMediator _mediator;

    public GetUserTableFileQueryHandler(
        IMediator mediator,
        IFileService file,
        ILogger<GetUserTableFileQuery> logger)
    {
        _logger = logger;
        _file = file;
        _mediator = mediator;
    }

    public async Task<UserTableFileVm> Handle(GetUserTableFileQuery request, CancellationToken cancellationToken)
    {
        //check access

        /*
        * anti-pattern to have _mediator inside mediator, 
        * prolly should move this into controller code?
        * but isnt big of a deal i think, and this is kinda easier to navigate
        * and there might be some additional code here in case i need to split controller action
        * and i def dont wanna move shared query for GetUserTable and GetUserTableFile 
        * into some service or helper
        *
        * though moving this into controller is tempting? hm....
        */

        _logger.JsonLogDebug("Request", request);

        var query = new GetUserTableQuery()
        {
            Search = request.Search,

            Page = request.Page,
            ItemsPerPage = request.ItemsPerPage,
            SortBy = request.SortBy,
            SortDesc = request.SortDesc,
            Filters = request.Filters
        };

        var res = await _mediator.Send(query);
        var fileContent = _file.BuildUserTableFile(res.Items);

        var vm = new UserTableFileVm
        {
            Content = fileContent,
            ContentType = FileContentType.CSV,
            FileName = Messages.UserTableFileName(DateTime.Now)
        };

        return vm;
    }
}

