using FluentResults;

namespace Domain.Errors.ProductCategories
{
    public class ProductCategoryDescriptionInvalidError : Error
    {
        public ProductCategoryDescriptionInvalidError()
            : base("A descrição da categoria de produtos deve ser informada")
        {
        }
    }
}
