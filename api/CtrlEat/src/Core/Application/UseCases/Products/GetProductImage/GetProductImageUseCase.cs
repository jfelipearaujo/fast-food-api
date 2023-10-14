using Application.UseCases.Common.Constants;
using Application.UseCases.Common.Errors;
using Application.UseCases.Products.GetProductImage.Errors;

using Domain.Adapters;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.GetProductImage;
using Domain.UseCases.Products.GetProductImage.Requests;

using FluentResults;

namespace Application.UseCases.Products.GetProductImage;

public class GetProductImageUseCase : IGetProductImageUseCase
{
    private readonly IProductRepository repository;

    public GetProductImageUseCase(IProductRepository repository)
    {
        this.repository = repository;
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

        var baseDirectory = Directory.GetCurrentDirectory();
        var directory = Path.Join(baseDirectory, "images");
        var filePath = Path.Combine(directory, $"{product.Id.Value}{Images.ValidImageExtension}");

        if (!File.Exists(filePath))
        {
            return Result.Fail(new ProductImageNotFountError(request.Id));
        }

        return filePath;
    }
}
