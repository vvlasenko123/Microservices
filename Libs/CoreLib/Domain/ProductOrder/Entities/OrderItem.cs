using Core.Domain.Common;
using Core.Domain.ProductOrder.ValueObjects;

namespace Core.Domain.ProductOrder.Entities;

public class OrderItem
{
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

    public OrderItem(ProductId productId, int quantity, Money price)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Количество должно быть больше нуля");
        }

        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}