using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

/// <summary>
/// Logging behaviour
/// </summary>
// Source: Clean Architecture example (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
 where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        _logger.LogInformation(Messages.LogRequest,
            name, _currentUserService.UserId, request);

        return Task.CompletedTask;
    }
}
