using Application.Interfaces;
using Core.Domain.ProductOrder.Entities.Interfaces;

namespace Application.UseCases;

public class ReserveOrderUseCase
{
    private readonly IOrderRepository _repo;
    private readonly IStockReserver _reserver;

    public ReserveOrderUseCase(IOrderRepository repo, IStockReserver reserver)
    {
        _repo = repo;
        _reserver = reserver;
    }

    public async Task HandleAsync(Guid orderId, CancellationToken ct)
    {
        var order = await _repo.GetAsync(orderId, ct);
        if (order == null)
        {
            throw new InvalidOperationException("Заказ не найден");
        }

        var reservationId = await _reserver.CreateReservationAsync(order.Id, order.Items, ct);
        order.MarkReserved(reservationId);
        await _repo.UpdateAsync(order, ct);
    }
}