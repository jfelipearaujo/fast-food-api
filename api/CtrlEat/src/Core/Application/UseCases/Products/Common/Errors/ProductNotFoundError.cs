using FluentResults;

namespace Application.UseCases.Products.Common.Errors;

public class ProductNotFoundError : Error
{
    public ProductNotFoundError(Guid id)
        : base($"O produto com o identificador '{id}' não foi encontrado")
    {
    }
}
