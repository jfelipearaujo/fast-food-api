using Domain.Adapters.Repositories;
using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.GetAllProducts;
using FluentResults;

namespace Application.UseCases.Products.GetAllProducts;

public class GetAllProductsUseCase : IGetAllProductsUseCase
{
    private readonly IProductRepository repository;

    public GetAllProductsUseCase(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<List<ProductResponse>>> ExecuteAsync(
        CancellationToken cancellationToken)
    {
        var products = await repository.GetAllAsync(cancellationToken);

        var response = new List<ProductResponse>();

        foreach (var product in products)
        {
            response.Add(ProductResponse.MapFromDomain(product));
        }

        return Result.Ok(response);
    }
}
