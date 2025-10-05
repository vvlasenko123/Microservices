namespace Core.Domain.ProductOrder.Enums;

public enum OrderStatus
{
    Created = 0,
    Reserved = 1,
    Paid = 2,
    Confirmed = 3,
    Canceled = 4
}