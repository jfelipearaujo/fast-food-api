using Domain.UseCases.Orders.Requests;

using Web.Api.Endpoints.Orders.Requests;

namespace Web.Api.Endpoints.Orders.Mapping;

public static class OrderEndpointRequestMapper
{
    public static CreateOrderRequest MapToRequest(this CreateOrderEndpointRequest request)
    {
        return new CreateOrderRequest
        {
            ClientId = request.ClientId,
        };
    }

    public static AddOrderItemRequest MapToRequest(this AddOrderItemEndpointRequest request, Guid orderId)
    {
        return new AddOrderItemRequest
        {
            OrderId = orderId,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Observation = request.Observation,
        };
    }
}
