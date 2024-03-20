using Domain.Adapters.Repositories;
using Domain.UseCases.Products.Common.Responses;
using Domain.UseCases.Products.GetAllProducts;

using FluentResults;

using Microsoft.Extensions.Logging;

namespace Application.UseCases.Products.GetAllProducts;

public class GetAllProductsUseCase : IGetAllProductsUseCase
{
    private readonly ILogger<GetAllProductsUseCase> logger;
    private readonly IProductRepository repository;

    public GetAllProductsUseCase(ILogger<GetAllProductsUseCase> logger, IProductRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task<Result<List<ProductResponse>>> ExecuteAsync(
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all products");

        var products = await repository.GetAllAsync(cancellationToken);

        var response = new List<ProductResponse>();

        logger.LogInformation("Retrieved {productsCount} products", products.Count());

        foreach (var product in products)
        {
            response.Add(ProductResponse.MapFromDomain(product));
        }

        return Result.Ok(response);
    }
}
