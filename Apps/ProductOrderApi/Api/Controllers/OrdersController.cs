using Api.Contracts.Requests;
using Api.Contracts.Response;
using Application.UseCases;
using Application.UseCases.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _create;
    private readonly ReserveOrderUseCase _reserve;
    private readonly GetOrderUseCase _get;

    public OrdersController(
        CreateOrderUseCase create,
        ReserveOrderUseCase reserve,
        GetOrderUseCase get)
    {
        _create = create;
        _reserve = reserve;
        _get = get;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest req, CancellationToken ct)
    {
        if (req.Items.Count == 0)
        {
            return BadRequest(new
            {
                message = "Нет позиций заказа"
            });
        }

        var dto = new CreateOrderDto
        {
            UserId = req.UserId,
            Country = req.Country,
            Region = req.Region,
            City = req.City,
            Street = req.Street,
            Items = req.Items.Select(i => new CreateOrderItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price,
                Currency = i.Currency
            }).ToList()
        };

        var id = await _create.HandleAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { orderId = id }, new { id });
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid orderId, CancellationToken ct)
    {
        var order = await _get.HandleAsync(orderId, ct);
        if (order == null)
        {
            return NotFound(new
            {
                message = "Заказ не найден"
            });
        }

        var response = new OrderResponse
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalAmount = order.TotalAmount.Amount,
            Currency = order.Currency.ToString(),
            Address = new AddressResponse
            {
                Country = order.ShippingAddress.Country,
                Region  = order.ShippingAddress.Region,
                City    = order.ShippingAddress.City,
                Street  = order.ShippingAddress.Street
            },
            Status = order.Status.ToString(),
            Items = order.Items.Select(i => new OrderItemResponse
            {
                ProductId = i.ProductId.Value,
                Quantity  = i.Quantity,
                Price     = i.Price.Amount
            }).ToList(),
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt
        };

        return Ok(response);
    }

    [HttpPost("{orderId:guid}/reserve")]
    public async Task<IActionResult> Reserve([FromRoute] Guid orderId, CancellationToken ct)
    {
        await _reserve.HandleAsync(orderId, ct);
        return NoContent();
    }
}