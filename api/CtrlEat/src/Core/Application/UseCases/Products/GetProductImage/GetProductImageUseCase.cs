using Application.UseCases.Common.Constants;
using Application.UseCases.Common.Errors;
using Application.UseCases.Products.GetProductImage.Errors;

using Domain.Adapters.Repositories;
using Domain.Adapters.Storage;
using Domain.Adapters.Storage.Requests;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.GetProductImage;
using Domain.UseCases.Products.GetProductImage.Requests;

using FluentResults;

using System.Net;

namespace Application.UseCases.Products.GetProductImage;

public class GetProductImageUseCase : IGetProductImageUseCase
{
    private readonly IProductRepository repository;
    private readonly IStorageService storageService;

    public GetProductImageUseCase(
        IProductRepository repository,
        IStorageService storageService)
    {
        this.repository = repository;
        this.storageService = storageService;
    }

    public async Task<Result<MemoryStream>> Execute(
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

        var filePath = $"app/images/{product.Id.Value}{Images.FILE_EXTENSION_JPG}";

        var fileRequest = new DownloadObjectRequest
        {
            Name = filePath,
            BucketName = "jsfelipearaujo"
        };

        var fileResponse = await storageService.DownloadFileAsync(fileRequest, cancellationToken);

        if (fileResponse.StatusCode == (int)HttpStatusCode.NotFound)
        {
            return Result.Fail(new ProductImageNotFountError(request.Id));
        }

        return fileResponse.FileStream;
    }
}
