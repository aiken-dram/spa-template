using Application.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Request.Commands.CreateRequest;

public class CreateRequestCommand : IRequest
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

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand>
{
    private readonly ISPADbContext _context;
    private readonly IMessageQueryService _mq;
    private readonly ILogger _logger;
    private readonly IUserService _user;

    public CreateRequestCommandHandler(
        ISPADbContext context,
        IMessageQueryService mq,
        IUserService user,
        ILogger<CreateRequestCommand> logger)
    {
        _context = context;
        _mq = mq;
        _logger = logger;
        _user = user;
    }

    public async Task<Unit> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        //check access
        _logger.JsonLogDebug("request", request);

        var reqType = await _context.RequestTypes
            .FirstOrDefaultAsync(p => p.Type == request.Type, cancellationToken);
        if (reqType == null)
            throw new NotFoundException(nameof(RequestType), request.Type);

        //1. add request to database
        var entity = new Domain.Entities.Request()
        {
            Created = DateTime.Now,
            IdType = (eRequestType)reqType.IdType,
            IdUser = _user.CurrentUserId,
            Json = request.Json,
            IdState = eRequestState.InQueue
        };
        _context.Requests.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        //2. add processing document request to queue
        _mq.Send(eQueue.QueryService, entity.IdRequest.ToString(), "");

        return Unit.Value;
    }
}

