using Application.UseCases.Common.Constants;
using Application.UseCases.Common.Errors;
using Application.UseCases.Products.UploadProductImage.Errors;
using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.UploadProductImage;
using Domain.UseCases.Products.UploadProductImage.Requests;

using FluentResults;

namespace Application.UseCases.Products.UploadProductImage;

public class UploadProductImageUseCase : IUploadProductImageUseCase
{

    private readonly IProductRepository repository;

    public UploadProductImageUseCase(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<string>> ExecuteAsync(
        UploadProductImageRequest request,
        CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.File.FileName);

        if (extension != Images.ValidImageExtension)
        {
            return Result.Fail(new ProductImageInvalidExtensionError());
        }

        var product = await repository.GetByIdAsync(ProductId.Create(request.Id), cancellationToken);

        if (product is null)
        {
            return Result.Fail(new ProductNotFoundError(request.Id));
        }

        var baseDirectory = Directory.GetCurrentDirectory();
        var directory = Path.Join(baseDirectory, "images");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var filePath = Path.Combine(directory, $"{product.Id.Value}{extension}");

        using var stream = File.OpenWrite(filePath);
        await request.File.CopyToAsync(stream, cancellationToken);

        product.Update(imageUrl: request.ImageUrl);

        await repository.UpdateAsync(product, cancellationToken);

        return product.Id.Value.ToString();
    }
}
