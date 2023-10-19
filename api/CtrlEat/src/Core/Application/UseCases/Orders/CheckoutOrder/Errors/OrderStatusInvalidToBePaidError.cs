using Domain.Entities.OrderAggregate.Enums;

using FluentResults;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Orders.CheckoutOrder.Errors;

public class OrderStatusInvalidToBePaidError : Error
{
    public OrderStatusInvalidToBePaidError(OrderStatus status)
        : base($"O status do pedido ({status}) é inválido para realizar o pagamento")
    {
        Metadata.Add("status_code", StatusCodes.Status400BadRequest);
    }
}