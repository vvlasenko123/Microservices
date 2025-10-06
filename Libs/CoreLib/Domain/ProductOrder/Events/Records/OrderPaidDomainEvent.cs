using Core.Domain.Common.Interfaces;

namespace Core.Domain.ProductOrder.Events.Records;

public record OrderPaidDomainEvent(Guid OrderId, Guid PaymentId) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}