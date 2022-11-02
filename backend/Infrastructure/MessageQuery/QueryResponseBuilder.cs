using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Shared.Application.Extensions;
using AutoMapper;

namespace Infrastructure.MessageQuery;

public partial class QueryResponseBuilder : IQueryResponseBuilder
{
    private readonly Infrastructure.Common.Interfaces.IConfigurationService _configuration;
    private readonly ILogger _logger;
    private readonly ISPADbContext _context;
    private readonly IMapper _mapper;
    private readonly IMessageQueryService _mq;

    public QueryResponseBuilder(
        ILogger<QueryResponseBuilder> logger,
        ISPADbContext context,
        Infrastructure.Common.Interfaces.IConfigurationService configuration,
        IMapper mapper,
        IMessageQueryService mq)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
        _mapper = mapper;
        _mq = mq;
    }

    public async Task ProcessRequestAsync(long Id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[QUERY] Processing request with id {Id}...");
        var req = await _context.Requests.GetAsync(Id, cancellationToken);

        //switch by request type and call appropriate export
        switch (req.IdType)
        {
            case eRequestType.TableExportAudit:
                await TableExportAuditAsync(req, cancellationToken);
                break;

#warning SAMPLE, remove in actual application
            case eRequestType.TableExportSample:
                await TableExportSampleAsync(req, cancellationToken);
                break;
        }
    }
}
