using FluentResults;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Orders.CheckoutHookOrder.Errors;

public class OrderStatusInvalidToBePaidError : Error
{
    public OrderStatusInvalidToBePaidError()
        : base("O pedido já foi pago, finalizado ou cancelado.")
    {
        Metadata.Add("status_code", StatusCodes.Status400BadRequest);
    }
}