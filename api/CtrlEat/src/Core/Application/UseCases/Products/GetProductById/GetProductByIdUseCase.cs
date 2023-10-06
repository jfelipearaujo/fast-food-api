using Application.UseCases.Common.Errors;

using Domain.Adapters;
using Domain.Entities.ProductAggregate.ValueObjects;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Application.UseCases.Products.GetProductById;

public class GetProductByIdUseCase : IGetProductByIdUseCase
{
    private readonly IProductRepository repository;

    public GetProductByIdUseCase(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<ProductResponse>> ExecuteAsync(
        GetProductByIdRequest request,
        CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(ProductId.Create(request.Id), cancellationToken);

        if (product is null)
        {
            return Result.Fail(new ProductNotFoundError(request.Id));
        }

        var response = ProductResponse.MapFromDomain(product);

        return Result.Ok(response);
    }
}
