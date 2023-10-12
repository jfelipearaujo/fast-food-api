using Domain.Entities.OrderAggregate.Enums;

using FluentResults;

namespace Domain.Entities.OrderAggregate.Errors;

public class OrderAlreadyWithStatusError : Error
{
    public OrderAlreadyWithStatusError(OrderStatus to)
        : base($"O pedido já encontra-se no status solicitado: '{to}'")
    {
    }
}