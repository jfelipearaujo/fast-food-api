using FluentResults;

namespace Application.UseCases.Orders.AddOrderItem.Errors;

public class OrderItemAlreadyExistsError : Error
{
    public OrderItemAlreadyExistsError(string itemDescription)
        : base($"O produto '{itemDescription}' já foi adicionado no pedido")
    {
    }
}
