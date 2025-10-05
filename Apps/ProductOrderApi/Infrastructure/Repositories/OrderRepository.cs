using System.Collections.Concurrent;
using Core.Domain.ProductOrder.Entities;
using Core.Domain.ProductOrder.Entities.Interfaces;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private static readonly ConcurrentDictionary<Guid, Order> _store = new();

    public Task<Order?> GetAsync(Guid id, CancellationToken ct)
    {
        if (_store.TryGetValue(id, out var o))
        {
            return Task.FromResult<Order?>(o);
        }
        return Task.FromResult<Order?>(null);
    }

    public Task AddAsync(Order order, CancellationToken ct)
    {
        _store[order.Id] = order;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Order order, CancellationToken ct)
    {
        _store[order.Id] = order;
        return Task.CompletedTask;
    }
}