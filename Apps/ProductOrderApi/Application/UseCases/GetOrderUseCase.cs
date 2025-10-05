using Core.Domain.ProductOrder.Entities;
using Core.Domain.ProductOrder.Entities.Interfaces;

namespace Application.UseCases;

public class GetOrderUseCase
{
    private readonly IOrderRepository _repo;

    public GetOrderUseCase(IOrderRepository repo)
    {
        _repo = repo;
    }

    public Task<Order?> HandleAsync(Guid orderId, CancellationToken ct)
    {
        return _repo.GetAsync(orderId, ct);
    }
}