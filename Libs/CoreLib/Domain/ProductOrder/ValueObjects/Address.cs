namespace Core.Domain.ProductOrder.ValueObjects;

public readonly struct Address
{
    public string Country { get; }
    public string Region { get; }
    public string City { get; }
    public string Street { get; }

    public Address(string country, string region, string city, string street)
    {
        Country = country ?? string.Empty;
        Region = region ?? string.Empty;
        City = city ?? string.Empty;
        Street = street ?? string.Empty;
    }
}