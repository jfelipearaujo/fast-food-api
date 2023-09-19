using FluentResults;

namespace Domain.Errors.Products
{
    public class ProductNotFoundError : Error
    {
        public ProductNotFoundError(Guid id)
            : base($"O produto com o identificador '{id}' não foi encontrado")
        {
        }
    }
}
