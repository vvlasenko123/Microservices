namespace Core.Domain.Common;

public readonly struct Money
{
    public decimal Amount { get; }

    public Money(decimal amount)
    {
        Amount = amount;
    }

    public override string ToString()
    {
        return Amount.ToString("0.##");
    }
}