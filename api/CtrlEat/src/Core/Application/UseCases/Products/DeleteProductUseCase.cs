using Domain.Adapters;
using Domain.Entities.TypedIds;
using Domain.Errors.Products;
using Domain.UseCases.Products;
using Domain.UseCases.Products.Requests;

using FluentResults;

namespace Application.UseCases.Products
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository repository;

        public DeleteProductUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<int>> ExecuteAsync(
            DeleteProductRequest request,
            CancellationToken cancellationToken)
        {
            var product = await repository.GetByIdAsync(new ProductId(request.Id), cancellationToken);

            if (product is null)
            {
                return Result.Fail(new ProductNotFoundError(request.Id));
            }

            var deletedEntities = await repository.DeleteAsync(product, cancellationToken);

            return Result.Ok(deletedEntities);
        }
    }
}
