using FluentResults;

namespace Domain.Errors.ProductCategories
{
    public class ProductCategoryNotFoundError : Error
    {
        public ProductCategoryNotFoundError(Guid id)
            : base($"A categoria de produto com o identificador '{id}' não foi encontrada")
        {
        }
    }
}
