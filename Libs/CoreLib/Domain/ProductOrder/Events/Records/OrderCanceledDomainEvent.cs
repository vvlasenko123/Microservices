using Core.Domain.Common.Interfaces;

namespace Core.Domain.ProductOrder.Events.Records;

public record OrderCanceledDomainEvent(Guid OrderId, string Reason) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}