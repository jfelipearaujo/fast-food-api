using Domain.Adapters;
using Domain.Entities.OrderAggregate.Enums;
using Domain.UseCases.Orders.Common.Responses;
using Domain.UseCases.Orders.GetOrdersByStatus;
using Domain.UseCases.Orders.GetOrdersByStatus.Requests;

using FluentResults;

namespace Application.UseCases.Orders.GetOrdersByStatus;

public class GetOrdersByStatusUseCase : IGetOrdersByStatusUseCase
{
    private readonly IOrderRepository repository;

    public GetOrdersByStatusUseCase(IOrderRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<Dictionary<OrderStatus, List<OrderResponse>>>> GetOrdersByStatus(
        GetOrdersByStatusRequest request,
        CancellationToken cancellationToken)
    {
        var searchForStatus = request.Status ?? OrderStatus.None;

        var ordersByStatus = await repository.GetAllByStatusAsync(searchForStatus, cancellationToken);

        var response = new Dictionary<OrderStatus, List<OrderResponse>>();

        foreach (var orderStatus in ordersByStatus)
        {
            response.Add(orderStatus.Key, OrderResponse.MapFromDomain(orderStatus.Value));
        }

        return response;
    }
}
