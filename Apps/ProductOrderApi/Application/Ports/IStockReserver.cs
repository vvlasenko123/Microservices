using Core.Domain.ProductOrder.Entities;
using Core.Domain.ProductOrder.ValueObjects;

namespace Application.Interfaces;

public interface IStockReserver
{
    Task<ReservationId> CreateReservationAsync(Guid orderId, IEnumerable<OrderItem> items, CancellationToken ct);
    Task ConfirmReservationAsync(ReservationId reservationId, CancellationToken ct);
    Task ReleaseReservationAsync(ReservationId reservationId, CancellationToken ct);
}