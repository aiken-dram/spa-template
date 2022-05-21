using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Sample.EventHandlers;

#warning This is example, remove entire file in actual application
public class SampleDeletedEventHandler : INotificationHandler<DomainEventNotification<SampleDeletedEvent>>
{
    private readonly ISPADbContext _context;
    private readonly ILogger<SampleDeletedEventHandler> _logger;

    public SampleDeletedEventHandler(
        ISPADbContext context,
        ILogger<SampleDeletedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<SampleDeletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
