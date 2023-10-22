using Application.UseCases.Common.Constants;
using Application.UseCases.Common.Errors;
using Application.UseCases.Products.GetProductImage.Errors;

using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.GetProductImage;
using Domain.UseCases.Products.GetProductImage.Requests;

using FluentResults;

using System.IO.Abstractions;

namespace Application.UseCases.Products.GetProductImage;

public class GetProductImageUseCase : IGetProductImageUseCase
{
    private readonly IProductRepository repository;
    private readonly IFileSystem fileSystem;

    public GetProductImageUseCase(IProductRepository repository, IFileSystem fileSystem)
    {
        this.repository = repository;
        this.fileSystem = fileSystem;
    }

    public async Task<Result<string>> Execute(
        GetProductImageRequest request,
        CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(
            ProductId.Create(request.Id),
            cancellationToken);

        if (product is null)
        {
            return Result.Fail(new ProductNotFoundError(request.Id));
        }

        var baseDirectory = fileSystem.Directory.GetCurrentDirectory();
        var directory = fileSystem.Path.Join(baseDirectory, "images");
        var filePath = fileSystem.Path.Combine(directory, $"{product.Id.Value}{Images.FILE_EXTENSION_JPG}");

        if (!fileSystem.File.Exists(filePath))
        {
            return Result.Fail(new ProductImageNotFountError(request.Id));
        }

        return filePath;
    }
}
