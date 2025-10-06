namespace Application.UseCases.DTO;

public sealed class CreateOrderDto
{
    public Guid UserId { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public List<CreateOrderItemDto> Items { get; set; } = new();
}