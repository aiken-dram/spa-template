using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Account.User.EventHandlers;

public class UserCreatedEventHandler : INotificationHandler<DomainEventNotification<UserCreatedEvent>>
{
    private readonly ISPADbContext _context;
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(
        ISPADbContext context,
        ILogger<UserCreatedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<UserCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}