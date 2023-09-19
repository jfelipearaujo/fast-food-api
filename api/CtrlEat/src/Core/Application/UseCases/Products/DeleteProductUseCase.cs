using Domain.Adapters;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;

namespace Application.UseCases.Products
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository repository;

        public DeleteProductUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int?> ExecuteAsync(
            DeleteProductRequest request,
            CancellationToken cancellationToken)
        {
            var product = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return null;

            return await repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
