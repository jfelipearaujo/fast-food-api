using Domain.Entities.OrderAggregate.Enums;

namespace Web.Api.Endpoints.Orders.Responses;

public class OrderCheckoutEndpointResponse
{
    public string TrackId { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
}