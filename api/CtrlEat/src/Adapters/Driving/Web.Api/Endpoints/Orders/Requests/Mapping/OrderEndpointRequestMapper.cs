using Domain.UseCases.Orders.AddOrderItem.Requests;
using Domain.UseCases.Orders.CreateOrder.Requests;

namespace Web.Api.Endpoints.Orders.Requests.Mapping;

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
