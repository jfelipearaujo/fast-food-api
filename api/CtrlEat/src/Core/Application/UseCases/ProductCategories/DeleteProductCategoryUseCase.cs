using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;

namespace Application.UseCases.ProductCategories
{
    public class DeleteProductCategoryUseCase : IDeleteProductCategoryUseCase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public DeleteProductCategoryUseCase(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<int?> ExecuteAsync(DeleteProductCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var exists = await _productCategoryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (exists is null)
                return null;

            return await _productCategoryRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}