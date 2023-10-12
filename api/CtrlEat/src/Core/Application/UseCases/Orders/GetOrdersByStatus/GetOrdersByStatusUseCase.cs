using Application.UseCases.Orders.Common.Errors;

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

    public async Task<Result<List<OrderTrackingResponse>>> ExecuteAsync(
        GetOrdersByStatusRequest request,
        CancellationToken cancellationToken)
    {
        var statusToSearch = OrderStatus.None;

        if (!string.IsNullOrEmpty(request.Status) && !Enum.TryParse(request.Status, true, out statusToSearch))
        {
            return Result.Fail(new InvalidOrderStatusError(request.Status));
        }

        var orders = await repository.GetAllByStatusAsync(statusToSearch, cancellationToken);

        var response = new List<OrderTrackingResponse>();

        foreach (var order in orders)
        {
            var orderByStatus = response.Find(x => x.Status == order.Status);

            if (orderByStatus is null)
            {
                response.Add(new OrderTrackingResponse
                {
                    Status = order.Status,
                    Orders = new List<OrderTrackingDataResponse>
                    {
                        OrderTrackingDataResponse.MapFromDomain(order)
                    }
                });
            }
            else
            {
                orderByStatus.Orders.Add(OrderTrackingDataResponse.MapFromDomain(order));
            }
        }

        return response;
    }
}
