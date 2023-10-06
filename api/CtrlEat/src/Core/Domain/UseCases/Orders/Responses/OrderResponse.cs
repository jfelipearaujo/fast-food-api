using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;

namespace Domain.UseCases.Orders.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderItemResponse> Items { get; set; }

    // --

    public static OrderResponse MapFromDomain(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id.Value,
            Status = order.Status,
            Items = OrderItemResponse.MapFromDomain(order.Items),
        };
    }
}
