using Domain.Entities.OrderAggregate;

namespace Domain.UseCases.Orders.Common.Responses;

public class OrderItemResponse
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public string Observation { get; set; }

    public string Description { get; set; }

    public string Price { get; set; }

    // --

    public static OrderItemResponse MapFromDomain(OrderItem orderItem)
    {
        return new OrderItemResponse
        {
            Id = orderItem.Id.Value,
            Quantity = orderItem.Quantity,
            Observation = orderItem.Observation,
            Description = orderItem.Description,
            Price = orderItem.FormatPrice(),
        };
    }

    public static List<OrderItemResponse> MapFromDomain(ICollection<OrderItem> orderItems)
    {
        if (orderItems is null)
        {
            return new List<OrderItemResponse>();
        }

        var items = new List<OrderItemResponse>();

        foreach (var itemResponse in orderItems)
        {
            items.Add(MapFromDomain(itemResponse));
        }

        return items;
    }
}