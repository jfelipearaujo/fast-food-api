using Domain.Entities.OrderAggregate.Enums;

namespace Web.Api.Endpoints.Orders;

public class CheckoutOrderEndpointResponse
{
    public string TrackId { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
}