using Domain.Adapters;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Application.UseCases.ProductCategories
{
    public class GetAllProductCategoriesUseCase : IGetAllProductCategoriesUseCase
    {
        private readonly IProductCategoryRepository repository;

        public GetAllProductCategoriesUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<List<ProductCategoryResponse>>> ExecuteAsync(
            CancellationToken cancellationToken)
        {
            var productCategories = await repository.GetAllAsync(cancellationToken);

            var response = new List<ProductCategoryResponse>();

            foreach (var productCategory in productCategories)
            {
                response.Add(ProductCategoryResponse.MapFromDomain(productCategory));
            }

            return Result.Ok(response);
        }
    }
}