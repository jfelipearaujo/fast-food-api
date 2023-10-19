using Application.UseCases.Common.Errors;
using Domain.Adapters.Repositories;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.ProductCategories.Common.Responses;
using Domain.UseCases.ProductCategories.GetProductCategoryById;
using Domain.UseCases.ProductCategories.GetProductCategoryById.Request;

using FluentResults;

namespace Application.UseCases.ProductCategories.GetProductCategoryById;

public class GetProductCategoryByIdUseCase : IGetProductCategoryByIdUseCase
{
    private readonly IProductCategoryRepository repository;

    public GetProductCategoryByIdUseCase(IProductCategoryRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ProductCategoryResponse>> ExecuteAsync(GetProductCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var productCategory = await repository.GetByIdAsync(
            ProductCategoryId.Create(request.Id),
            cancellationToken);

        if (productCategory is null)
        {
            return Result.Fail(new ProductCategoryNotFoundError(request.Id));
        }

        return ProductCategoryResponse.MapFromDomain(productCategory);
    }
}