using FluentResults;

namespace Application.UseCases.Orders.GetOrderById.Errors;

public class OrderNotFoundError : Error
{
    public OrderNotFoundError(Guid id)
        : base($"O pedido com o identificador '{id}' não foi encontrado")
    {
    }
}