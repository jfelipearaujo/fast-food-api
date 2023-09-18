using Domain.Adapters;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;
using Domain.UseCases.Products.Responses;

using Mapster;

namespace Application.UseCases.Products
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductResponse?> ExecuteAsync(
            GetProductByIdRequest request,
            CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return null;

            return product.Adapt<ProductResponse?>();
        }
    }
}
