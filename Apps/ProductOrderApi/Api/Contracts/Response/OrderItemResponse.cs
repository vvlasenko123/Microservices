﻿namespace Api.Contracts.Response;

public class OrderItemResponse
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
