using Domain.Entities.OrderAggregate.Enums;

namespace Domain.UseCases.Orders.GetOrdersByStatus.Requests;

public class GetOrdersByStatusRequest
{
    public OrderStatus? Status { get; set; }
}
