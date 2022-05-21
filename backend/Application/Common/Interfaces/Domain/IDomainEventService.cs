namespace Application.Common.Interfaces;

/// <summary>
/// Domain event service for publishing
/// </summary>
public interface IDomainEventService
{
    /// <summary>
    /// Publish domain event
    /// </summary>
    /// <param name="domainEvent">Domain event</param>
    Task Publish(DomainEvent domainEvent);
}