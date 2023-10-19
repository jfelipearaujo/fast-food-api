using Domain.Entities.OrderAggregate.Enums;

namespace Web.Api.Endpoints.Orders.Responses;

public class OrderCheckoutEndpointResponse
{
    public PaymentStatus Status { get; set; }
}