using FluentResults;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Orders.CheckoutHookOrder.Errors;

public class NoWaitingApprovalPaymentError : Error
{
    public NoWaitingApprovalPaymentError()
        : base("Nenhum pagamento pendente foi encontrado")
    {
        Metadata.Add("status_code", StatusCodes.Status404NotFound);
    }
}
