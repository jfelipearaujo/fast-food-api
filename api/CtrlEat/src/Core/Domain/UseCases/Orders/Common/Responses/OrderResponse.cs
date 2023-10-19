using Domain.Entities.OrderAggregate;
using Domain.Entities.OrderAggregate.Enums;

namespace Domain.UseCases.Orders.Common.Responses;

public class OrderResponse
{
    public Guid Id { get; set; }

    public string TrackId { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public IEnumerable<PaymentResponse> Payments { get; set; }

    public List<OrderItemResponse> Items { get; set; }

    // --

    public static OrderResponse MapFromDomain(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id.Value,
            TrackId = order.TrackId.Value,
            OrderStatus = order.Status,
            Payments = PaymentResponse.MapFromDomain(order.Payments),
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
