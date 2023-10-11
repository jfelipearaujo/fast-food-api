using Domain.Adapters;
using Domain.UseCases.ProductCategories.Common.Responses;
using Domain.UseCases.ProductCategories.GetAllProductCategories;
using FluentResults;

namespace Application.UseCases.ProductCategories.GetAllProductCategories;

public class GetAllProductCategoriesUseCase : IGetAllProductCategoriesUseCase
{
    private readonly IProductCategoryRepository repository;

    public GetAllProductCategoriesUseCase(IProductCategoryRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<List<ProductCategoryResponse>>> ExecuteAsync(
        CancellationToken cancellationToken)
    {
        var productCategories = await repository.GetAllAsync(cancellationToken);

        var response = new List<ProductCategoryResponse>();

        foreach (var productCategory in productCategories)
        {
            response.Add(ProductCategoryResponse.MapFromDomain(productCategory));
        }

        return response;
    }
}