using FluentResults;

namespace Application.UseCases.Orders.GetOrdersByStatus.Errors;

public class InvalidOrderStatusError : Error
{
    public InvalidOrderStatusError(string status)
        : base($"O status informado '{status}' não é válido, tente novamente")
    {
    }
}
