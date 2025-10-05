using Core.Domain.Common;
using Core.Domain.ProductOrder.Entities;
using Core.Domain.ProductOrder.ValueObjects;

namespace Core.Domain.ProductOrder.Builders;

public class OrderBuilder
{
    private Guid _userId;
    private Address _address;
    private Currency _currency = Currency.RUB;
    private bool _userSet;
    private bool _addressSet;

    public OrderBuilder WithUser(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор пользователя не может быть пустым");
        }

        _userId = userId;
        _userSet = true;
        return this;
    }

    public OrderBuilder WithAddress(Address address)
    {
        _address = address;
        _addressSet = true;
        return this;
    }

    public OrderBuilder WithCurrency(Currency currency)
    {
        _currency = currency;
        return this;
    }

    public Order Build()
    {
        if (!_userSet)
        {
            throw new InvalidOperationException("Пользователь не задан");
        }

        if (!_addressSet)
        {
            throw new InvalidOperationException("Адрес не задан");
        }

        var order = new Order(_userId, _address, _currency);
        return order;
    }
}