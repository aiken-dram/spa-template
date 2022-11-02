using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Sample.EventHandlers;

#warning SAMPLE, remove entire file in actual application
public class SampleCreatedEventHandler : INotificationHandler<DomainEventNotification<SampleCreatedEvent>>
{
    private readonly ISPADbContext _context;
    private readonly ILogger<SampleCreatedEventHandler> _logger;

    public SampleCreatedEventHandler(
        ISPADbContext context,
        ILogger<SampleCreatedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<SampleCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
