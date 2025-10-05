namespace Api.Contracts.Requests;

public class CreateOrderRequest
{
    public Guid UserId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}