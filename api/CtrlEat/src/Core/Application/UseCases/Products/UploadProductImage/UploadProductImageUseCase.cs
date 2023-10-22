using Application.UseCases.Common.Constants;
using Application.UseCases.Common.Errors;
using Application.UseCases.Products.UploadProductImage.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.UploadProductImage;
using Domain.UseCases.Products.UploadProductImage.Requests;

using FluentResults;

using System.IO.Abstractions;

namespace Application.UseCases.Products.UploadProductImage;

public class UploadProductImageUseCase : IUploadProductImageUseCase
{
    private readonly IProductRepository repository;
    private readonly IFileSystem fileSystem;

    public UploadProductImageUseCase(IProductRepository repository, IFileSystem fileSystem)
    {
        this.repository = repository;
        this.fileSystem = fileSystem;
    }

    public async Task<Result<string>> ExecuteAsync(
        UploadProductImageRequest request,
        CancellationToken cancellationToken)
    {
        var extension = fileSystem.Path.GetExtension(request.File.FileName);

        if (extension != Images.FILE_EXTENSION_JPG)
        {
            return Result.Fail(new ProductImageInvalidExtensionError());
        }

        var product = await repository.GetByIdAsync(ProductId.Create(request.Id), cancellationToken);

        if (product is null)
        {
            return Result.Fail(new ProductNotFoundError(request.Id));
        }

        var baseDirectory = fileSystem.Directory.GetCurrentDirectory();
        var directory = fileSystem.Path.Join(baseDirectory, "images");

        if (!fileSystem.Directory.Exists(directory))
        {
            fileSystem.Directory.CreateDirectory(directory);
        }

        var filePath = fileSystem.Path.Combine(directory, $"{product.Id.Value}{extension}");

        using var stream = fileSystem.File.OpenWrite(filePath);
        await request.File.CopyToAsync(stream, cancellationToken);

        product.Update(imageUrl: request.ImageUrl);

        await repository.UpdateAsync(product, cancellationToken);

        return product.Id.Value.ToString();
    }
}
