using FluentResults;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Orders.CreateOrder.Errors;

public class OrderOnGoingForClientError : Error
{
    public OrderOnGoingForClientError()
        : base("Já existe um pedido em andamento")
    {
        Metadata.Add("status_code", StatusCodes.Status400BadRequest);
    }
}
