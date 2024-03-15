using Domain.UseCases.Products.GetProductImage.Requests;

using FluentResults;

namespace Domain.UseCases.Products.GetProductImage;

public interface IGetProductImageUseCase
{
    Task<Result<MemoryStream>> Execute(
        GetProductImageRequest request,
        CancellationToken cancellationToken);
}
