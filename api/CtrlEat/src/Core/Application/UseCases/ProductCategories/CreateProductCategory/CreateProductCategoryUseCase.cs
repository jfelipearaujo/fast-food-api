using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate;
using Domain.UseCases.ProductCategories.Common.Responses;
using Domain.UseCases.ProductCategories.CreateProductCategory;
using Domain.UseCases.ProductCategories.CreateProductCategory.Request;

using FluentResults;

namespace Application.UseCases.ProductCategories.CreateProductCategory;

public class CreateProductCategoryUseCase : ICreateProductCategoryUseCase
{
    private readonly IProductCategoryRepository repository;

    public CreateProductCategoryUseCase(IProductCategoryRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ProductCategoryResponse>> ExecuteAsync(
        CreateProductCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var productsCategoryValidation = ProductCategory.Create(request.Description);

        if (productsCategoryValidation.IsFailed)
        {
            return Result.Fail(productsCategoryValidation.Errors);
        }

        ProductCategory productCategory = productsCategoryValidation.Value;

        await repository.CreateAsync(productCategory, cancellationToken);

        return ProductCategoryResponse.MapFromDomain(productCategory);
    }
}