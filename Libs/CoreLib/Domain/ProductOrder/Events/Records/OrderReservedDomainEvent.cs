using Core.Domain.Common.Interfaces;

namespace Core.Domain.ProductOrder.Events.Records;

public record OrderReservedDomainEvent(Guid OrderId, Guid ReservationId) : IDomainEvent
{
    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}