using Domain.Entities.OrderAggregate.Enums;
using Domain.UseCases.Orders.CheckoutOrder.Responses;
using Domain.UseCases.Orders.Common.Responses;

namespace Web.Api.Endpoints.Orders.Responses.Mapping;

public static class OrderEndpointResponseMapper
{
    public static OrderEndpointResponse MapToResponse(this OrderResponse order)
    {
        return new OrderEndpointResponse
        {
            Id = order.Id,
            TrackId = order.TrackId,
            Status = order.OrderStatus,
            TotalAmount = order.TotalAmount,
            Payments = order.Payments,
            Items = order.Items.MapToResponse(),
        };
    }

    public static CreateOrderEndpointResponse MapToCreatedResponse(this OrderResponse order)
    {
        return new CreateOrderEndpointResponse
        {
            Id = order.Id,
            TrackId = order.TrackId,
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

    public static OrderTrackingEndpointDataResponse MapToResponse(this OrderTrackingDataResponse order)
    {
        return new OrderTrackingEndpointDataResponse
        {
            Id = order.Id,
            TrackId = order.TrackId,
            StatusUpdatedAt = order.StatusUpdatedAt,
            StatusUpdatedAtFormatted = order.StatusUpdatedAtFormatted,
        };
    }

    public static List<OrderTrackingEndpointDataResponse> MapToResponse(this List<OrderTrackingDataResponse> orders)
    {
        var response = new List<OrderTrackingEndpointDataResponse>();

        foreach (var order in orders)
        {
            response.Add(order.MapToResponse());
        }

        return response;
    }

    public static OrderTrackingEndpointResponse MapToResponse(this OrderTrackingResponse order)
    {
        return new OrderTrackingEndpointResponse
        {
            Status = order.Status,
            Orders = order.Orders.MapToResponse()
        };
    }

    public static List<OrderTrackingEndpointResponse> MapToResponse(this List<OrderTrackingResponse> orders)
    {
        var response = new List<OrderTrackingEndpointResponse>();

        foreach (var order in orders)
        {
            response.Add(order.MapToResponse());
        }

        return response;
    }

    public static Dictionary<OrderStatus, List<OrderTrackingEndpointResponse>> MapToResponse(this Dictionary<OrderStatus, List<OrderTrackingResponse>> orders)
    {
        var response = new Dictionary<OrderStatus, List<OrderTrackingEndpointResponse>>();

        foreach (var order in orders)
        {
            response.Add(order.Key, order.Value.MapToResponse());
        }

        return response;
    }

    public static OrderCheckoutEndpointResponse MapToResponse(this CheckoutOrderResponse response)
    {
        return new OrderCheckoutEndpointResponse
        {
            TrackId = response.TrackId,
            PaymentStatus = response.PaymentStatus
        };
    }
}