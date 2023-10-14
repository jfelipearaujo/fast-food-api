using Domain.UseCases.Products.GetProductImage.Requests;

using FluentResults;

namespace Domain.UseCases.Products.GetProductImage;

public interface IGetProductImageUseCase
{
    Task<Result<string>> Execute(
        GetProductImageRequest request,
        CancellationToken cancellationToken);
}
