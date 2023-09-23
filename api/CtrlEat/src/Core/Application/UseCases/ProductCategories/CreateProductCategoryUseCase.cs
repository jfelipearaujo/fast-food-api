using Domain.Adapters;
using Domain.Entities;
using Domain.Entities.StrongIds;
using Domain.UseCases.ProductCategories;
using Domain.UseCases.ProductCategories.Requests;
using Domain.UseCases.ProductCategories.Responses;

using FluentResults;

namespace Application.UseCases.ProductCategories
{
    public class CreateProductCategoryUseCase : ICreateProductCategoryUseCase
    {
        private readonly IProductCategoryRepository repository;

        public CreateProductCategoryUseCase(IProductCategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<ProductCategoryResponse>> ExecuteAsync(
            CreateProductCategoryRequest request,
            CancellationToken cancellationToken)
        {
            var productsCategory = new ProductCategory
            {
                Id = ProductCategoryId.Create(Guid.NewGuid()),
                Description = request.Description
            };

            await repository.CreateAsync(productsCategory, cancellationToken);

            var response = ProductCategoryResponse.MapFromDomain(productsCategory);

            return Result.Ok(response);
        }
    }
}