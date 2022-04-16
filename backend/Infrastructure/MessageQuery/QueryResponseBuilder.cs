using Application.Common.Interfaces;
using Shared.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Shared.Application.Extensions;
using Microsoft.Extensions.Configuration;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Query;

public partial class QueryResponseBuilder : IQueryResponseBuilder
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly ISPADbContext _context;

    private readonly string _localPath;
    private readonly string _exportPath;
    private readonly string _databaseExportPath;

    public QueryResponseBuilder(
        ILogger<QueryResponseBuilder> logger,
        ISPADbContext context,
        IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;

        _localPath = _configuration["SiteSettings:LocalPath"];
        _exportPath = _configuration["SiteSettings:ExportPath"];
        _databaseExportPath = _configuration["SiteSettings:DatabaseExportPath"];
    }

    public async Task ProcessRequestAsync(long Id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Processing request with id {Id}...");
        var req = await _context.Requests.FindIdAsync(Id, cancellationToken);

        if (req == null)
            throw new NotFoundException(nameof(Domain.Entities.Request), Id);

        //switch by request type and call appropriate export
        //well, atm only one type
        await UserTableExportAsync(req, cancellationToken);
    }
}
