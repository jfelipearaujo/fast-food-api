using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

namespace Application.UseCases.ProductCategories
{
    public class DeleteProductCategoryUseCase : IDeleteProductCategoryUseCase
    {
        private readonly IProductCategoryRepository repository;

        public DeleteProductCategoryUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int?> ExecuteAsync(DeleteProductCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var exists = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (exists is null)
                return null;

            return await repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}