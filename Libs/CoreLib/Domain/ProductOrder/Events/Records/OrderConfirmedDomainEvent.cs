using Core.Domain.Common.Interfaces;

namespace Core.Domain.ProductOrder.Events.Records;

public record OrderConfirmedDomainEvent(Guid OrderId) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}