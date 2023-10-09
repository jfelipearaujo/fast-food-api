using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products.DeleteProduct;
using Domain.UseCases.Products.DeleteProduct.Requests;

using FluentResults;

namespace Application.UseCases.Products.DeleteProduct;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IProductRepository repository;

    public DeleteProductUseCase(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<int>> ExecuteAsync(
        DeleteProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(ProductId.Create(request.Id), cancellationToken);

        if (product is null)
        {
            return Result.Fail(new ProductNotFoundError(request.Id));
        }

        var deletedEntities = await repository.DeleteAsync(product, cancellationToken);

        return Result.Ok(deletedEntities);
    }
}
