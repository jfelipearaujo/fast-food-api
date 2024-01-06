using Web.Api.Extensions;

namespace Web.Api.Endpoints.Orders;

public static class OrderEndpoints
{
    private const string EndpointTag = "Order";

    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var orders = app.NewVersionedApi(EndpointTag);

        var group = orders.MapGroup(ApiEndpoints.Orders.BaseRoute)
            .HasApiVersion(ApiEndpoints.Orders.V1.Version)
            .WithOpenApi();

        group.MapGet("{id}", GetOrderById.HandleAsync)
            .WithName(ApiEndpoints.Orders.V1.GetById)
            .WithDescription("Searches and returns a order's data based on their identifier")
            .Produces<OrderEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/tracking", OrderTrackingByStatus.HandleAsync)
            .WithName(ApiEndpoints.Orders.V1.GetByStatus)
            .WithDescription("Searches and returns a order's data based on their status. If not provided, then all orders will be returned.")
            .Produces<List<OrderTrackingEndpointResponse>>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("/", CreateOrder.HandleAsync)
            .WithName(ApiEndpoints.Orders.V1.Create)
            .WithDescription("Register an order")
            .Produces<CreateOrderEndpointResponse>(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapPatch("{id}/status", UpdateOrderStatus.HandleAsync)
            .WithName(ApiEndpoints.Orders.V1.UpdateStatus)
            .WithDescription("Update the order status")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("{id}/items", AddOrderItem.HandleAsync)
            .WithName(ApiEndpoints.Orders.V1.AddOrderItem)
            .WithDescription("Add an item to the order")
            .Produces<OrderItemEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("{id}/checkout", CheckoutOrder.HandleAsync)
            .WithName(ApiEndpoints.Orders.V1.Checkout)
            .WithDescription("Checkout the order")
            .Produces(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapPost("/checkout/hook", CheckoutHookOrder.HandleAsync)
            .WithName(ApiEndpoints.Orders.V1.CheckoutHook)
            .WithDescription("Checkout Hook the order")
            .Produces(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest)
            .Produces<ApiError>(StatusCodes.Status404NotFound);
    }
}
