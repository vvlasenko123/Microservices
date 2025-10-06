namespace Core.Domain.ProductOrder.ValueObjects;

public readonly struct PaymentId
{
    public Guid Value { get; }

    public PaymentId(Guid value)
    {
        Value = value;
    }
}