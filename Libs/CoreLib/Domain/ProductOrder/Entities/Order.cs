using Core.Domain.Common;
using Core.Domain.ProductOrder.Enums;
using Core.Domain.ProductOrder.Events.Records;
using Core.Domain.ProductOrder.ValueObjects;

namespace Core.Domain.ProductOrder.Entities;

public class Order : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public Money TotalAmount { get; private set; }
    public Currency Currency { get; private set; }

    public Address ShippingAddress { get; private set; }
    public OrderStatus Status { get; set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public ReservationId? ReservationId { get; private set; }
    public PaymentId? PaymentId { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    internal Order(Guid userId, Address address, Currency currency)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        ShippingAddress = address;
        Currency = currency;
        TotalAmount = new Money(0m);
        Status = OrderStatus.Created;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public void AddItem(ProductId productId, int quantity, Money price)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Количество должно быть больше нуля");
        }

        _items.Add(new OrderItem(productId, quantity, price));
        RecalculateTotal();
    }

    public void MarkReserved(ReservationId reservationId)
    {
        if (Status != OrderStatus.Created)
        {
            throw new InvalidOperationException("Резервировать можно только новый заказ");
        }

        ReservationId = reservationId;
        Status = OrderStatus.Reserved;

        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new OrderReservedDomainEvent(Id, reservationId.Value));
    }

    public void MarkPaid(PaymentId paymentId)
    {
        if (Status != OrderStatus.Reserved)
        {
            throw new InvalidOperationException("Оплатить можно только зарезервированный заказ");
        }

        PaymentId = paymentId;
        Status = OrderStatus.Paid;

        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new OrderPaidDomainEvent(Id, paymentId.Value));
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Paid)
        {
            throw new InvalidOperationException("Подтвердить можно только оплаченный заказ");
        }

        Status = OrderStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new OrderConfirmedDomainEvent(Id));
    }

    public void Cancel(string reason)
    {
        if (Status == OrderStatus.Canceled || Status == OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Нельзя отменить подтверждённый или уже отменённый заказ");
        }

        Status = OrderStatus.Canceled;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new OrderCanceledDomainEvent(Id, reason));
    }

    private void RecalculateTotal()
    {
        var sum = 0m;
        foreach (var i in _items)
        {
            sum += i.Price.Amount * i.Quantity;
        }

        TotalAmount = new Money(sum);
    }
}