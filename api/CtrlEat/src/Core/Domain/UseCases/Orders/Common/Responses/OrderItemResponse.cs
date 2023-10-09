using Domain.Entities.OrderAggregate;

using System.Globalization;

namespace Domain.UseCases.Orders.Common.Responses;

public class OrderItemResponse
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public string Observation { get; set; }

    public string Description { get; set; }

    public string Price { get; private set; }

    // --

    public static OrderItemResponse MapFromDomain(OrderItem orderItem)
    {
        var currencyCultureName = orderItem.Price.Currency == "BRL" ? "pt-BR" : "en-US";

        return new OrderItemResponse
        {
            Id = orderItem.Id.Value,
            Quantity = orderItem.Quantity,
            Observation = orderItem.Observation,
            Description = orderItem.Description,
            Price = orderItem.Price.Amount.ToString("C", CultureInfo.CreateSpecificCulture(currencyCultureName)),
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