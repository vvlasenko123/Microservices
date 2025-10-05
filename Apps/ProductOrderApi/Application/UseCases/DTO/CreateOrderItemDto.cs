namespace Application.UseCases.DTO;

public sealed class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "RUB";
}