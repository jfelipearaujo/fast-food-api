using Application.UseCases.Common.Constants;
using Application.UseCases.Common.Errors;
using Application.UseCases.Products.UploadProductImage.Errors;

using Domain.Adapters.Repositories;
using Domain.Adapters.Storage;
using Domain.Adapters.Storage.Requests;
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
    private readonly IStorageService storageService;

    public UploadProductImageUseCase(
        IProductRepository repository,
        IFileSystem fileSystem,
        IStorageService storageService)
    {
        this.repository = repository;
        this.fileSystem = fileSystem;
        this.storageService = storageService;
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

        await using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream, cancellationToken);

        var filePath = $"app/images/{product.Id.Value}{extension}";

        var uploadRequest = new UploadObjectRequest
        {
            InputStream = memoryStream,
            Name = filePath,
            BucketName = "jsfelipearaujo"
        };

        await storageService.UploadFileAsync(uploadRequest, cancellationToken);

        product.Update(imageUrl: filePath);

        await repository.UpdateAsync(product, cancellationToken);

        return product.Id.Value.ToString();
    }
}
