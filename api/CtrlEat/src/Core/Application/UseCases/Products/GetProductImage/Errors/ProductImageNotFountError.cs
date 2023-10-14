using FluentResults;

namespace Application.UseCases.Products.GetProductImage.Errors;

public class ProductImageNotFountError : Error
{
    public ProductImageNotFountError(Guid id)
        : base($"A imagem do produto '{id}' não foi encontrada")
    {

    }
}
