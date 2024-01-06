using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;
using Domain.Entities.OrderAggregate.ValueObjects;

namespace Domain.UseCases.Orders.CheckoutOrder.Responses;

public class CheckoutOrderResponse
{
    public string TrackId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    // ---

    public static CheckoutOrderResponse MapFromDomain(TrackId trackId, Payment payment)
    {
        return new CheckoutOrderResponse
        {
            TrackId = trackId.Value,
            PaymentStatus = payment.Status
        };
    }
}