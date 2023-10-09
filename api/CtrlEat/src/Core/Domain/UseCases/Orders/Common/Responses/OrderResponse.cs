using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;

namespace Domain.UseCases.Orders.Common.Responses;

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

    public static List<OrderResponse> MapFromDomain(List<Order> orders)
    {
        var response = new List<OrderResponse>();

        foreach (var item in orders)
        {
            response.Add(MapFromDomain(item));
        }

        return response;
    }
}
