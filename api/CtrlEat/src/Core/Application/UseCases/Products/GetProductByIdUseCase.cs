using Domain.Adapters;
using Domain.Errors.Products;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

namespace Application.UseCases.Products
{
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
            var product = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                return Result.Fail(new ProductNotFoundError(request.Id));
            }

            var response = ProductResponse.MapFromDomain(product);

            return Result.Ok(response);
        }
    }
}
