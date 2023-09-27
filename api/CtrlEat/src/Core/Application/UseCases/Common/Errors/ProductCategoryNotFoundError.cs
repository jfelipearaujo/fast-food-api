using FluentResults;

namespace Application.UseCases.Common.Errors;

public class ProductCategoryNotFoundError : Error
{
    public ProductCategoryNotFoundError(Guid id)
        : base($"A categoria de produto com o identificador '{id}' não foi encontrada")
    {
    }
}