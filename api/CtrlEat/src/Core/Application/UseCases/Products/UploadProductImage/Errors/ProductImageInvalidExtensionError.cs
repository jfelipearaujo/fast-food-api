using FluentResults;

namespace Application.UseCases.Products.UploadProductImage.Errors;

public class ProductImageInvalidExtensionError : Error
{
    public ProductImageInvalidExtensionError()
        : base("A imagem precisa ser com a extensão '.jpg'")
    {

    }
}
