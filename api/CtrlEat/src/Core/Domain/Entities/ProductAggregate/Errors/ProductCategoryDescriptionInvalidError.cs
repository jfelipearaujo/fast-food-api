using FluentResults;

namespace Domain.Entities.ProductAggregate.Errors;

public class ProductCategoryDescriptionInvalidError : Error
{
    public ProductCategoryDescriptionInvalidError()
        : base("A descrição da categoria de produtos deve ser informada")
    {
    }
}
