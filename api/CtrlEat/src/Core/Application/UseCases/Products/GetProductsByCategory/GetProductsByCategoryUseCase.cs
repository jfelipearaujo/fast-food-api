using Domain.Adapters;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Application.UseCases.Products.GetProductsByCategory;

public class GetProductsByCategoryUseCase : IGetProductsByCategoryUseCase
{
    private readonly IProductRepository repository;

    public GetProductsByCategoryUseCase(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<List<ProductResponse>>> ExecuteAsync(
        GetProductsByCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var products = await repository.GetAllByCategoryAsync(request.Category, cancellationToken);

        var response = new List<ProductResponse>();

        foreach (var product in products)
        {
            response.Add(ProductResponse.MapFromDomain(product));
        }

        return Result.Ok(response);
    }
}
