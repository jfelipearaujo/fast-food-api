using Domain.Entities.OrderAggregate.Enums;

using FluentResults;

namespace Domain.Entities.OrderAggregate.Errors;

public class OrderInvalidStatusTransitionError : Error
{
    public OrderInvalidStatusTransitionError(OrderStatus from, OrderStatus to)
        : base($"Não é possível transicionar do status '{from}' para '{to}'")
    {
    }
}
