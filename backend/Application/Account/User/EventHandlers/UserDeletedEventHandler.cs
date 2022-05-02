using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Account.User.EventHandlers;

public class UserDeletedEventHandler : INotificationHandler<DomainEventNotification<UserDeletedEvent>>
{
    private readonly ISPADbContext _context;
    private readonly ILogger<UserDeletedEventHandler> _logger;

    public UserDeletedEventHandler(
        ISPADbContext context,
        ILogger<UserDeletedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task Handle(DomainEventNotification<UserDeletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        _logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}