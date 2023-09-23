using Domain.Adapters;
using Domain.Entities;
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
            var productsCategoryValidation = ProductCategory.ValidateAndCreate(request.Description);

            if (productsCategoryValidation.IsFailed)
            {
                return Result.Fail(productsCategoryValidation.Errors);
            }

            ProductCategory productCategory = productsCategoryValidation.Value;

            await repository.CreateAsync(productCategory, cancellationToken);

            return ProductCategoryResponse.MapFromDomain(productCategory);
        }
    }
}