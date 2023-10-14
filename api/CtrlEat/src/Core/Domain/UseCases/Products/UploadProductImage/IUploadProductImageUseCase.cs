using Domain.UseCases.Products.UploadProductImage.Requests;

using FluentResults;

namespace Domain.UseCases.Products.UploadProductImage;

public interface IUploadProductImageUseCase
{
    Task<Result<string>> ExecuteAsync(
        UploadProductImageRequest request,
        CancellationToken cancellationToken);
}
