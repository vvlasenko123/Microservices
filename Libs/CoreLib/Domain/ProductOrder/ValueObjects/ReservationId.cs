namespace Core.Domain.ProductOrder.ValueObjects;

public readonly struct ReservationId
{
    public Guid Value { get; }

    public ReservationId(Guid value)
    {
        Value = value;
    }
}