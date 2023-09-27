using FluentResults;

namespace Domain.Entities.ProductCategoryAggregate.Errors;

public class ProductCategoryDescriptionInvalidError : Error
{
    public ProductCategoryDescriptionInvalidError()
        : base("A descrição da categoria de produtos deve ser informada")
    {
    }
}
