using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

using FluentResults;

namespace Application.UseCases.ProductCategories.DeleteProductCategory;

public class DeleteProductCategoryUseCase : IDeleteProductCategoryUseCase
{
    private readonly IProductCategoryRepository repository;

    public DeleteProductCategoryUseCase(IProductCategoryRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<int>> ExecuteAsync(
        DeleteProductCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var productCategory = await repository.GetByIdAsync(
            ProductCategoryId.Create(request.Id),
            cancellationToken);

        if (productCategory is null)
        {
            return Result.Fail(new ProductCategoryNotFoundError(request.Id));
        }

        return await repository.DeleteAsync(productCategory, cancellationToken);
    }
}