using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using Mapster;

namespace Application.UseCases.ProductCategories
{
    public class GetProductCategoryByIdUseCase : IGetProductCategoryByIdUseCase
    {
        private readonly IProductCategoryRepository repository;

        public GetProductCategoryByIdUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProductCategoryResponse?> ExecuteAsync(GetProductCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var productCategory = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (productCategory is null)
                return null;

            return productCategory.Adapt<ProductCategoryResponse>();
        }
    }
}