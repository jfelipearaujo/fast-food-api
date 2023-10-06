using FluentResults;

namespace Domain.Entities.OrderAggregate.Errors;

public class OrderItemInvalidQuantityError : Error
{
    public OrderItemInvalidQuantityError()
        : base("É esperado que seja informado pelo menos 1 item")
    {
    }
}
