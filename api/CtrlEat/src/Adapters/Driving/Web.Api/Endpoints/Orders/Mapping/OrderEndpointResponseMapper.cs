using Domain.UseCases.Orders.Common.Responses;
using Web.Api.Endpoints.Orders.Responses;

namespace Web.Api.Endpoints.Orders.Mapping;

public static class OrderEndpointResponseMapper
{
    public static OrderEndpointResponse MapToResponse(this OrderResponse order)
    {
        return new OrderEndpointResponse
        {
            Id = order.Id,
            Status = order.Status,
            Items = order.Items.MapToResponse(),
        };
    }

    public static OrderItemEndpointResponse MapToResponse(this OrderItemResponse order)
    {
        return new OrderItemEndpointResponse
        {
            Id = order.Id,
            Quantity = order.Quantity,
            Description = order.Description,
            Observation = order.Observation,
            Price = order.Price,
        };
    }

    public static List<OrderItemEndpointResponse> MapToResponse(this List<OrderItemResponse> orderItems)
    {
        if (orderItems is null)
        {
            return new List<OrderItemEndpointResponse>();
        }

        var items = new List<OrderItemEndpointResponse>();

        foreach (var itemResponse in orderItems)
        {
            items.Add(itemResponse.MapToResponse());
        }

        return items;
    }
}