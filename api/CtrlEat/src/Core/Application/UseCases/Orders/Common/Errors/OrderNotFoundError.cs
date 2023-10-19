using FluentResults;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Orders.Common.Errors;

public class OrderNotFoundError : Error
{
    public OrderNotFoundError(Guid id)
        : base($"O pedido com o identificador '{id}' não foi encontrado")
    {
        Metadata.Add("status_code", StatusCodes.Status404NotFound);
    }
}