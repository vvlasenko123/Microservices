namespace Core.Domain.Common.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync(IEnumerable<IDomainEvent> events, CancellationToken ct);
}