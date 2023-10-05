using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Application.UseCases.ProductCategories.UpdateProductCategory;

public class UpdateProductCategoryUseCase : IUpdateProductCategoryUseCase
{
    private readonly IProductCategoryRepository repository;

    public UpdateProductCategoryUseCase(IProductCategoryRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ProductCategoryResponse>> ExecuteAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var productCategory = await repository.GetByIdAsync(
            ProductCategoryId.Create(request.Id),
            cancellationToken);

        if (productCategory is null)
        {
            return Result.Fail(new ProductCategoryNotFoundError(request.Id));
        }

        productCategory.Update(request.Description);

        await repository.UpdateAsync(productCategory, cancellationToken);

        return ProductCategoryResponse.MapFromDomain(productCategory);
    }
}