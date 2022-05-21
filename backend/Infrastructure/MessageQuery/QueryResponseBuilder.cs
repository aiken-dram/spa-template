using Application.Common.Interfaces;
using Shared.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Shared.Application.Extensions;
using Microsoft.Extensions.Configuration;
using Infrastructure.Common.Interfaces;
using Domain.Enums;

namespace Infrastructure.MessageQuery;

public partial class QueryResponseBuilder : IQueryResponseBuilder
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly ISPADbContext _context;

    private readonly string _requestStoragePath;
    private readonly string _databaseExportPath;
    private readonly string _databaseExportRemotePath;

    public QueryResponseBuilder(
        ILogger<QueryResponseBuilder> logger,
        ISPADbContext context,
        IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;

        _requestStoragePath = _configuration["SiteSettings:RequestStoragePath"];
        _databaseExportPath = _configuration["SiteSettings:DatabaseExportPath"];
        _databaseExportRemotePath = _configuration["SiteSettings:DatabaseExportRemotePath"];
    }

    public async Task ProcessRequestAsync(long Id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Processing request with id {Id}...");
        var req = await _context.Requests.FindIdAsync(Id, cancellationToken);

        if (req == null)
            throw new NotFoundException(nameof(Domain.Entities.Request), Id);

        //switch by request type and call appropriate export
        switch (req.IdType)
        {
            case eRequestType.TableExportAudit:
                await TableExportAuditAsync(req, cancellationToken);
                break;
            case eRequestType.TableExportSample:
                await TableExportSampleAsync(req, cancellationToken);
                break;
        }
    }
}
