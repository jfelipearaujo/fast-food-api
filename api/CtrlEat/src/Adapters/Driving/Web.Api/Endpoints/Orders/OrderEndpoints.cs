using Domain.UseCases.Orders.AddOrderItem;
using Domain.UseCases.Orders.CheckoutOrder;
using Domain.UseCases.Orders.CheckoutOrder.Requests;
using Domain.UseCases.Orders.CreateOrder;
using Domain.UseCases.Orders.GetOrderById;
using Domain.UseCases.Orders.GetOrderById.Requests;
using Domain.UseCases.Orders.GetOrdersByStatus;
using Domain.UseCases.Orders.GetOrdersByStatus.Requests;
using Domain.UseCases.Orders.UpdateOrderStatus;

using Web.Api.Endpoints.Orders.Requests;
using Web.Api.Endpoints.Orders.Requests.Mapping;
using Web.Api.Endpoints.Orders.Responses;
using Web.Api.Endpoints.Orders.Responses.Mapping;
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

        group.MapGet("{id}", GetOrderById)
            .WithName(ApiEndpoints.Orders.V1.GetById)
            .WithDescription("Searches and returns a order's data based on their identifier")
            .Produces<OrderEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapGet("/tracking", GetOrdersByStatus)
            .WithName(ApiEndpoints.Orders.V1.GetByStatus)
            .WithDescription("Searches and returns a order's data based on their status. If not provided, then all orders will be returned.")
            .Produces<List<OrderTrackingEndpointResponse>>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("/", CreateOrder)
            .WithName(ApiEndpoints.Orders.V1.Create)
            .WithDescription("Register an order")
            .Produces<CreateOrderEndpointResponse>(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status404NotFound);

        group.MapPatch("{id}/status", UpdateOrderStatus)
            .WithName(ApiEndpoints.Orders.V1.UpdateStatus)
            .WithDescription("Update the order status")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("{id}/items", AddOrderItem)
            .WithName(ApiEndpoints.Orders.V1.AddOrderItem)
            .WithDescription("Add an item to the order")
            .Produces<OrderItemEndpointResponse>(StatusCodes.Status200OK)
            .Produces<ApiError>(StatusCodes.Status400BadRequest);

        group.MapPost("{id}/checkout", CheckoutOrder)
            .WithName(ApiEndpoints.Orders.V1.Checkout)
            .WithDescription("Checkout the order")
            .Produces(StatusCodes.Status201Created)
            .Produces<ApiError>(StatusCodes.Status400BadRequest)
            .Produces<ApiError>(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> GetOrderById(
        Guid id,
        IGetOrderByIdUseCase useCase,
        CancellationToken cancellation)
    {
        var request = new GetOrderByIdRequest
        {
            Id = id,
        };

        var result = await useCase.ExecuteAsync(request, cancellation);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> GetOrdersByStatus(
        string? status,
        IGetOrdersByStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new GetOrdersByStatusRequest
        {
            Status = status
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> CreateOrder(
        CreateOrderEndpointRequest endpointRequest,
        ICreateOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToCreatedResponse();

        return Results.CreatedAtRoute(
            ApiEndpoints.Orders.V1.GetById,
            response,
            new
            {
                id = response.Id
            });
    }

    public static async Task<IResult> UpdateOrderStatus(
        Guid id,
        UpdateOrderStatusEndpointRequest endpointRequest,
        IUpdateOrderStatusUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        return Results.Ok();
    }

    public static async Task<IResult> AddOrderItem(
        Guid id,
        AddOrderItemEndpointRequest endpointRequest,
        IAddOrderItemUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }

    public static async Task<IResult> CheckoutOrder(
        Guid id,
        ICheckoutOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = new CheckoutOrderRequest
        {
            OrderId = id
        };

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            if (result.HasNotFound())
            {
                return Results.NotFound(result.ToApiError());
            }

            return Results.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return Results.Ok(response);
    }
}
