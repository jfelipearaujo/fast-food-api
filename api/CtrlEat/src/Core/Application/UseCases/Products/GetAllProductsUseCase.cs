using Domain.Adapters;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Responses;

using Mapster;

namespace Application.UseCases.Products
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductRepository repository;

        public GetAllProductsUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ProductResponse>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var products = await repository.GetAllAsync(cancellationToken);

            return products.Adapt<IEnumerable<ProductResponse>>();
        }
    }
}
