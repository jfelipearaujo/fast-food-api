using Domain.Entities.OrderAggregate.Enums;

namespace Web.Api.Endpoints.Orders;

public class OrderTrackingEndpointResponse
{
    public OrderStatus Status { get; set; }

    public List<OrderTrackingEndpointDataResponse> Orders { get; set; }
}
