using FluentResults;

namespace Application.UseCases.Orders.Common.Errors;

public class OrderNotFoundError : Error
{
    public OrderNotFoundError(Guid id)
        : base($"O pedido com o identificador '{id}' não foi encontrado")
    {
    }
}