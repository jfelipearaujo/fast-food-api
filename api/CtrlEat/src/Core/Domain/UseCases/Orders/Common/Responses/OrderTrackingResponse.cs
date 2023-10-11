using Domain.Entities.OrderAggregate.Enums;

namespace Domain.UseCases.Orders.Common.Responses;

public class OrderTrackingResponse
{
    public OrderStatus Status { get; set; }

    public List<OrderTrackingDataResponse> Orders { get; set; } = new();
}
