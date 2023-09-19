using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using Mapster;

namespace Application.UseCases.ProductCategories
{
    public class UpdateProductCategoryUseCase : IUpdateProductCategoryUseCase
    {
        private readonly IProductCategoryRepository repository;

        public UpdateProductCategoryUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProductCategoryResponse?> ExecuteAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
        {
            var productCategory = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (productCategory is null)
                return null;

            productCategory.Description = request.Description;

            await repository.UpdateAsync(productCategory, cancellationToken);

            return productCategory.Adapt<ProductCategoryResponse>();
        }
    }
}