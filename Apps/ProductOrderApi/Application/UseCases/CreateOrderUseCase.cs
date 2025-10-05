using Application.UseCases.DTO;
using Core.Domain.Common;
using Core.Domain.ProductOrder.Builders;
using Core.Domain.ProductOrder.Entities.Interfaces;
using Core.Domain.ProductOrder.ValueObjects;

namespace Application.UseCases;

public class CreateOrderUseCase
{
    private readonly IOrderRepository _repo;

    public CreateOrderUseCase(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> HandleAsync(CreateOrderDto dto, CancellationToken ct)
    {
        var order = new OrderBuilder()
            .WithUser(dto.UserId)
            .WithAddress(new Address(dto.Country, dto.Region, dto.City, dto.Street))
            .Build();

        foreach (var i in dto.Items)
        {
            order.AddItem(new ProductId(i.ProductId), i.Quantity, new Money(i.Price));
        }

        await _repo.AddAsync(order, ct);
        return order.Id;
    }
}