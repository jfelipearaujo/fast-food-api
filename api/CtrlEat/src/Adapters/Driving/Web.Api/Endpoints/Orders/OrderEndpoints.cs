using Domain.UseCases.Orders.AddOrderItem;
using Domain.UseCases.Orders.CreateOrder;
using Domain.UseCases.Orders.GetOrderById;
using Domain.UseCases.Orders.GetOrderById.Requests;
using Domain.UseCases.Orders.GetOrdersByStatus;
using Domain.UseCases.Orders.GetOrdersByStatus.Requests;

using Microsoft.AspNetCore.Http.HttpResults;

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
        var group = app.MapGroup("/orders")
            .WithTags(EndpointTag);

        group.MapGet("{id}", GetOrderById)
            .WithName(nameof(GetOrderById))
            .WithOpenApi();

        group.MapGet("/tracking", GetOrdersByStatus)
            .WithName(nameof(GetOrdersByStatus))
            .WithOpenApi();

        group.MapPost("/", CreateOrder)
            .WithName(nameof(CreateOrder))
            .WithOpenApi();

        group.MapPost("{id}/items", AddOrderItem)
            .WithName(nameof(AddOrderItem))
            .WithOpenApi();
    }

    public static async Task<Results<Ok<OrderEndpointResponse>, NotFound<ApiError>>> GetOrderById(
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
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<List<OrderTrackingEndpointResponse>>, BadRequest<ApiError>>> GetOrdersByStatus(
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
            return TypedResults.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }

    public static async Task<Results<CreatedAtRoute<OrderEndpointResponse>, NotFound<ApiError>>> CreateOrder(
        CreateOrderEndpointRequest endpointRequest,
        ICreateOrderUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest();

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.NotFound(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.CreatedAtRoute(
            response,
            nameof(GetOrderById),
            new
            {
                id = response.Id
            });
    }

    public static async Task<Results<Ok<OrderItemEndpointResponse>, BadRequest<ApiError>>> AddOrderItem(
        Guid id,
        AddOrderItemEndpointRequest endpointRequest,
        IAddOrderItemUseCase useCase,
        CancellationToken cancellationToken)
    {
        var request = endpointRequest.MapToRequest(id);

        var result = await useCase.ExecuteAsync(request, cancellationToken);

        if (result.IsFailed)
        {
            return TypedResults.BadRequest(result.ToApiError());
        }

        var response = result.Value.MapToResponse();

        return TypedResults.Ok(response);
    }
}
