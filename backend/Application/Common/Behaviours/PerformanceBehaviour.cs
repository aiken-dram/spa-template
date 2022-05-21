using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.Behaviours;

/// <summary>
/// Performance behaviour
/// </summary>
// Source: Clean Architecture example (https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public PerformanceBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            var name = typeof(TRequest).Name;

            _logger.LogWarning(Messages.LongRunningRequest,
                name, _timer.ElapsedMilliseconds, _currentUserService.UserId, request);
        }

        return response;
    }
}
