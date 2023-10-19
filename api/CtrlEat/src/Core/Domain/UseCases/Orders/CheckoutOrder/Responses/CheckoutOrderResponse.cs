using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;

namespace Domain.UseCases.Orders.CheckoutOrder.Responses;

public class CheckoutOrderResponse
{
    public PaymentStatus Status { get; set; }

    // ---

    public static CheckoutOrderResponse MapFromDomain(Payment payment)
    {
        return new CheckoutOrderResponse
        {
            Status = payment.Status
        };
    }
}