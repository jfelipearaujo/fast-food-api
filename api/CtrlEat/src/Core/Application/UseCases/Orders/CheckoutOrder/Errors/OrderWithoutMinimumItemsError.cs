using FluentResults;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Orders.CheckoutOrder.Errors;

public class OrderWithoutMinimumItemsError : Error
{
    public OrderWithoutMinimumItemsError(int minimumItems)
        : base($"O pedido precisa ter pelo menos {minimumItems} item adicionado")
    {
        Metadata.Add("status_code", StatusCodes.Status400BadRequest);
    }
}