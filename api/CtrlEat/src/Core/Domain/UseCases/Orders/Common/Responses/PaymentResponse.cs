using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;

namespace Domain.UseCases.Orders.Common.Responses;

public class PaymentResponse
{
    public Guid Id { get; set; }

    public PaymentStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public static PaymentResponse MapFromDomain(Payment payment)
    {
        return new PaymentResponse
        {
            Id = payment.Id.Value,
            Status = payment.Status,
            CreatedAt = payment.CreatedAtUtc,
            UpdatedAt = payment.UpdatedAtUtc,
        };
    }

    public static IEnumerable<PaymentResponse> MapFromDomain(ICollection<Payment> payments)
    {
        foreach (var payment in payments)
        {
            yield return MapFromDomain(payment);
        }
    }
}
