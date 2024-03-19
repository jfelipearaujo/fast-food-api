using Domain.UseCases.Products.GetProductImage.Requests;

using FluentResults;

namespace Domain.UseCases.Products.GetProductImage;

public interface IGetProductImageUseCase
{
    Task<Result<byte[]>> Execute(
        GetProductImageRequest request,
        CancellationToken cancellationToken);
}
