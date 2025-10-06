using Core.Domain.ProductOrder.ValueObjects;

namespace Application.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentId> CreatePaymentAsync(Guid orderId, decimal amount, string currency, string? method, string? cardToken, string? phone, CancellationToken ct);
}