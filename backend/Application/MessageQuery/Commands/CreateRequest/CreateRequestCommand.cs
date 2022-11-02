using Application.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.MessageQuery.Commands.CreateRequest;

public class CreateRequestCommand : IRequest<long>
{
    /// <summary>
    /// Request type
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// JSON with request parameters
    /// </summary>
    public string? Json { get; set; }
}

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, long>
{
    private readonly ISPADbContext _context;
    private readonly IMessageQueryService _mq;
    private readonly ILogger _logger;
    private readonly IUserService _user;
    private readonly IConfigurationService _config;

    private IAuditBuilder _audit => _context.AuditBuilder;

    public CreateRequestCommandHandler(
        ISPADbContext context,
        IMessageQueryService mq,
        IUserService user,
        IConfigurationService config,
    ILogger<CreateRequestCommand> logger)
    {
        _context = context;
        _mq = mq;
        _logger = logger;
        _user = user;
        _config = config;
    }

    private async Task<Audit> Audit(Domain.Entities.Request entity)
    {
        var res = new Audit(
            entity,
            (int)eMessageQueryAuditAction.Request,
            null);

        // AuditData 
        res.Add(await _audit.PropertyValueAsync(entity, p => p.Json));

        return res;
    }

    public async Task<long> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        //check access
        _logger.JsonLogDebug("request", request);

        //this is sorta validation, but i also need reqType and i dont wanna retrieve it twice, sooo...
        var reqType = await _context.RequestTypes
            .FirstOrDefaultAsync(p => p.Type == request.Type, cancellationToken);
        if (reqType == null)
            throw new NotFoundException(nameof(RequestType), request.Type);

        var _type = reqType.IdType;

        //check queue overflow for each request type
        //could group some based on queue later, if needed
        var cnt = await _context.Requests
            .CountAsync(p =>
                p.IdType == _type &&
                p.IdState == eRequestState.InQueue &&
                p.IdUser == _user.CurrentUserId,
            cancellationToken);

        if (cnt > _config.MessageQueryUserLimit)
            throw new BadRequestException(Messages.RequestQueueOverflow);

        //check access for each request
        //2D: maybe move this into custom validators?
        switch (_type)
        {
            case eRequestType.TableExportAudit:
                break;
#warning SAMPLE
            case eRequestType.TableExportSample:
                break;
            case eRequestType.RScript:
                break;
        }

        //1. add request to database
        var entity = new Domain.Entities.Request()
        {
            Created = DateTime.Now,
            IdType = (eRequestType)reqType.IdType,
            IdUser = _user.CurrentUserId,
            Json = request.Json,
            IdState = eRequestState.InQueue
        };
        //audit
        entity.Log(await Audit(entity));
        _context.Requests.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        //2. send request to message queue
        _mq.SendRequest(entity);

        return entity.IdRequest;
    }
}

