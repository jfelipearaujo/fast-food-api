using Domain.Adapters;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using FluentResults;

using Mapster;

namespace Application.UseCases.Products
{
    public class GetProductsByCategoryUseCase : IGetProductsByCategoryUseCase
    {
        private readonly IProductRepository repository;

        public GetProductsByCategoryUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<IEnumerable<ProductResponse>>> ExecuteAsync(
            GetProductsByCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var products = await repository.GetAllByCategoryAsync(request.Category, cancellationToken);

            var response = products.Adapt<IEnumerable<ProductResponse>>();

            return Result.Ok(response);
        }
    }
}
