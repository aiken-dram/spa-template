using Application.Common.Interfaces;
using Shared.Domain.Models;

namespace WorkService.Services;

public class DomainEventService : IDomainEventService
{
    public async Task Publish(DomainEvent domainEvent)
    {
        //domain events are not supported in background service
        //well they could be, just replace signalr with messagequery to signalr in services for background worker project
    }
}
