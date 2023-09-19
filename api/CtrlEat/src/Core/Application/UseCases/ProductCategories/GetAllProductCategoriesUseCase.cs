using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Responses;

using Mapster;

namespace Application.UseCases.ProductCategories
{
    public class GetAllProductCategoriesUseCase : IGetAllProductCategoriesUseCase
    {
        private readonly IProductCategoryRepository repository;

        public GetAllProductCategoriesUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ProductCategoryResponse>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var productCategories = await repository.GetAllAsync(cancellationToken);

            return productCategories.Adapt<IEnumerable<ProductCategoryResponse>>();
        }
    }
}