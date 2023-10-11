using FluentResults;

namespace Application.UseCases.Orders.Common.Errors;

public class InvalidOrderStatusForUpdateError : Error
{
    public InvalidOrderStatusForUpdateError(string status)
        : base($"O status informado '{status}' não é válido para ser utilizado em uma transação")
    {
    }
}