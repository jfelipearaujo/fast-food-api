using FluentResults;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Orders.CheckoutOrder.Errors;

public class PaymentAlreadyExistsForOrderError : Error
{
    public PaymentAlreadyExistsForOrderError()
        : base("Já existe uma order de pagamento em andamento para o pedido")
    {
        Metadata.Add("status_code", StatusCodes.Status400BadRequest);
    }
}
