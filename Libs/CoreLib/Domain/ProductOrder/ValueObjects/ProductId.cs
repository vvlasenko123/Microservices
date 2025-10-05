namespace Core.Domain.ProductOrder.ValueObjects;

public readonly struct ProductId
{
    public Guid Value { get; }

    public ProductId(Guid value)
    {
        Value = value;
    }
}